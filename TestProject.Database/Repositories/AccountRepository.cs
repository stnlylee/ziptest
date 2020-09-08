using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.Database.Repositories
{
    public class AccountRepository : RepositoryBase<Account, int>, IAccountRepository
    {
        private readonly DatabaseContext _databseContext;

        public AccountRepository(DatabaseContext databseContext) : base(databseContext)
        {
            _databseContext = databseContext;
        }
    }
}
