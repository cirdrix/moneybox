using System;
using System.Collections.Generic;
using MoneyBox.DataAccess;
using MoneyBox.Domain;

namespace MoneyBox.Services
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> LoadAllUsers();
        Guid GetGuid();
    }
    public class UserService : IUserService
    {
      
        private readonly IRepository<ApplicationUser> _repositoryBox;

        public UserService(IRepository<ApplicationUser> repositoryBox)
        {
            _repositoryBox = repositoryBox;
      
        }

        public IEnumerable<ApplicationUser> LoadAllUsers()
        {
            return _repositoryBox.Get();
        }

        public Guid GetGuid()
        {
            return _repositoryBox.GetMyGuid();
        }
    }
}
