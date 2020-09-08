using System.Collections.Generic;
using TestProject.Domain.Models;

namespace TestProject.Tests
{
    public abstract class TestBase
    {
        protected readonly User john;
        protected readonly User sue;
        protected readonly User newUser;
        protected readonly List<User> users;
        protected readonly List<Account> accounts;
        protected readonly Account newAccount;

        public TestBase()
        {
            john = new User
            {
                Id = 1,
                Email = "john@company.com",
                Name = "John",
                MonthlySalary = 10000,
                MonthlyExpense = 5000
            };
            sue = new User
            {
                Id = 2,
                Email = "sue@company.com",
                Name = "Sue",
                MonthlySalary = 5000,
                MonthlyExpense = 4500
            };
            users = new List<User>() { john, sue };
            newUser = new User
            {
                Id = 3,
                Email = "bob@company.com",
                Name = "Bob",
                MonthlySalary = 7000.45m,
                MonthlyExpense = 2500.21m
            };

            var account1 = new Account
            {
                Id = 1,
                UserId = 1,
                User = john,
                Credit = 0
            };
            var account2 = new Account
            {
                Id = 2,
                UserId = 1,
                User = john,
                Credit = 100.98m
            };
            accounts = new List<Account>() { account1, account2 };

            newAccount = new Account
            {
                Id = 3,
                UserId = 1,
                User = john,
                Credit = 500.44m
            };
         }
    }
}
