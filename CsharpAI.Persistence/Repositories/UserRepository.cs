using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Persistence.Entities;
using CsharpAI.Persistence.Interfaces;
using Dapper;
//using Microsoft.Extensions.Logging;

namespace CsharpAI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDBContext _context;
        //private readonly ILogger<UserRepository> _logger;

        public UserRepository(DapperDBContext context)
        {
            _context = context;
        }
        public async Task<User> AuthenticateUser(string email, string password)
        {
            User user = null; 
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var procedure = "[AuthenticateUser]";
                    var values = new
                    {
                        Email = email,
                        Password = password

                    };
                    user = await connection.QueryFirstOrDefaultAsync<User>(procedure, values, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
               // _loggerRepo?.LogInfo(ex.Message.ToString());
            }
            return user;
        }

        
    }
}
