using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Application.DTOs;

namespace CsharpAI.Application.Interfaces
{
    public interface IAuthService
    {       

        /// <summary>
        /// Authenticates a user and returns a JWT token if successful.
        /// </summary>
        /// <param name="loginDto">User login credentials</param>
        /// <returns>Authentication response with JWT token</returns>
        Task<AuthResponseDto> LoginUser(LoginRequestDto loginDto);
    }
}
