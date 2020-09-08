using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestProject.BusinessService.User;
using TestProject.Domain.Models;
using TestProject.Domain.Repositories;
using Xunit;

namespace TestProject.Tests.ServiceTests
{
    public class UserServiceTests : TestBase
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task CheckCredit_Should_Return_False_When_User_Credit_Is_Null()
        {
            // Arrange
            // nothing to arrange

            // Act
            var result = await _userService.CheckCredit(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckCredit_Should_Return_False_When_User_Credit_Is_Bad()
        {
            // Arrange
            // nothing to arrange

            // Act
            var result = await _userService.CheckCredit(sue);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckCredit_Should_Return_True_When_User_Credit_Is_Good()
        {
            // Arrange
            // nothing to arrange

            // Act
            var result = await _userService.CheckCredit(john);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateUser_Should_Not_Create_User_When_There_Is_Existing_User()
        {
            // Arragne
            _mockUserRepository
                .Setup(ur => ur.SearchAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>() { john });

            // Act
            var result = await _userService.CreateUser(john);


            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUser_Should_Succeed()
        {
            // Arragne
            _mockUserRepository
                .Setup(ur => ur.SearchAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>());
            _mockUserRepository
                .Setup(ur => ur.AddAsync(newUser))
                .ReturnsAsync(newUser);

            // Act
            var result = await _userService.CreateUser(newUser);


            // Assert
            Assert.Equal(newUser, result);
        }

        [Fact]
        public async Task GetUserByEmail_Should_Return_Null_When_No_Data()
        {
            // Arragne
            _mockUserRepository
                .Setup(ur => ur.SearchAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>());

            // Act
            var result = await _userService.GetUserByEmail(john.Email);


            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByEmail_Should_Return_User()
        {
            // Arragne
            _mockUserRepository
                .Setup(ur => ur.SearchAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>() { john });

            // Act
            var result = await _userService.GetUserByEmail(john.Email);


            // Assert
            Assert.Equal(john, result);
        }

        [Fact]
        public async Task GetUserById_Should_Return_Null_When_Id_Is_Not_Valid()
        {
            // Arrange
            // nothing to arrange

            // Act
            var result = await _userService.GetUserById(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserById_Should_Return_User()
        {
            // Arrange
            _mockUserRepository
                .Setup(ur => ur.GetAsync(1))
                .ReturnsAsync(john);

            // Act
            var result = await _userService.GetUserById(1);

            // Assert
            Assert.Equal(john, result);
        }
    }
}
