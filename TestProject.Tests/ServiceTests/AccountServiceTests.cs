using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BusinessService.Account;
using TestProject.BusinessService.User;
using TestProject.Domain.Models;
using TestProject.Domain.Repositories;
using Xunit;

namespace TestProject.Tests.ServiceTests
{
    public class AccountServiceTests : TestBase
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<IUserService> _mockUserService;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockUserService = new Mock<IUserService>();
            _accountService = new AccountService(_mockAccountRepository.Object, 
                _mockUserService.Object);
        }

        [Fact]
        public async Task GetAllAccounts_Should_Return_Null()
        {
            // Arrange
            _mockAccountRepository
                .Setup(ar => ar.GetAllAsync())
                .ReturnsAsync((IEnumerable<Account>)null);

            // Act
            var result = await _accountService.GetAllAccounts();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAccounts_Should_Return_All_Accounts()
        {
            // Arrange
            _mockAccountRepository
                .Setup(ar => ar.GetAllAsync())
                .ReturnsAsync(accounts);

            // Act
            IEnumerable<Account> result = await _accountService.GetAllAccounts();

            // Assert
            int i = 0;
            foreach (var account in result)
            {
                Assert.Equal(accounts[i], account);
                i++;
            }
        }

        [Fact]
        public async Task CreateAccount_Should_Fail_When_User_Is_NonExisting()
        {
            // Arrange
            _mockUserService
                .Setup(us => us.GetUserById(It.IsAny<int>()))
                .ReturnsAsync(sue);

            // Act
            var result = await _accountService.CreateAccount(john.Id, 0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAccount_Should_Fail_When_Check_Credit_Failed()
        {
            // Arrange
            _mockUserService
                .Setup(us => us.GetUserById(It.IsAny<int>()))
                .ReturnsAsync(sue);
            _mockUserService
                .Setup(us => us.CheckCredit(sue))
                .ReturnsAsync(false);

            // Act
            var result = await _accountService.CreateAccount(sue.Id, 0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAccount_Should_Succeed()
        {
            // Arrange
            _mockUserService
                .Setup(us => us.GetUserById(It.IsAny<int>()))
                .ReturnsAsync(john);
            _mockUserService
                .Setup(us => us.CheckCredit(john))
                .ReturnsAsync(true);
            _mockAccountRepository
                .Setup(ar => ar.AddAsync(It.IsAny<Account>()))
                .ReturnsAsync(newAccount);

            // Act
            var result = await _accountService.CreateAccount(john.Id, 500);

            // Assert
            Assert.Equal(newAccount, result);
        }
    }
}
