using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Persistence.Entities;

namespace CsharpAI.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(string email, string password);
    }
}
