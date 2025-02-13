using System.Text;
using CsharpAI.Application.Interfaces;
using CsharpAI.Application.Services;
using CsharpAI.Infrastructure.AI;
using CsharpAI.Infrastructure.Jobs;
using CsharpAI.Infrastructure.Security;
using CsharpAI.Persistence;
using CsharpAI.Persistence.Interfaces;
using CsharpAI.Persistence.Repositories;
using CsharpAI.SignalR;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Sentry;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

// Configure Services
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddSingleton<DapperDBContext>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new DapperDBContext(connectionString);
});
//  Configure Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "AIJobPortal_";
});

// Add Sentry configuration
builder.WebHost.UseSentry(options =>
{
    options.Dsn = builder.Configuration["Sentry:Dsn"]; // Get DSN from appsettings.json
    options.Debug = true;  // Enable debugging logs
    options.TracesSampleRate = 1.0; // 100% transactions (adjust as needed)
    options.Environment = builder.Configuration["Sentry:Environment"];
});

// Configure AI
builder.Services.AddSingleton(new AiProcessor(builder.Configuration["OpenAI:ApiKey"]));
// Configure SignalR
builder.Services.AddSignalR();

// Configure Hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// Capture unhandled exceptions automatically
app.UseSentryTracing();

app.UseAuthorization();
// Enable SignalR
app.MapHub<SignalHub>("/SignalHub");

// Enable Hangfire Dashboard
app.UseHangfireDashboard();

// Schedule AI job matching
RecurringJob.AddOrUpdate<JobMatchingService>(
    "job-matching", service => service.MatchJobs(), Cron.Daily);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
