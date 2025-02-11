using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Application.DTOs;
using CsharpAI.Application.Interfaces;
using CsharpAI.Infrastructure.Security;
using CsharpAI.Persistence.Repositories;

namespace CsharpAI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> LoginUser(LoginRequestDto loginDto)
        {
            var user = await _userRepository.AuthenticateUser(loginDto.Email , loginDto.Password);

            if (user == null )
            {
                return new AuthResponseDto { Message = "Invalid credentials" };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Role = user.Role,
                Message = "Login successful"
            };
        }
    }
}
