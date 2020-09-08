using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.Database.Repositories
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        private readonly DatabaseContext _databseContext;

        public UserRepository(DatabaseContext databseContext) : base(databseContext)
        {
            _databseContext = databseContext;
        }
    }
}
