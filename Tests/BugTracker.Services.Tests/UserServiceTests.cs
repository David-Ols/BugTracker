using System;
using NUnit.Framework;
using Moq;
using AutoFixture;
using BugTracker.Mappers.Interfaces;
using BugTracker.Repository.Interfaces;
using BugTracker.Models;
using BugTracker.Mappers;

namespace BugTracker.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
	{
        private UserService _userService;
        private Mock<IUserRepository> _userRepository;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();

            _userRepository = new Mock<IUserRepository>();

            _userService = new UserService(_userRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _userRepository.Invocations.Clear();
        }

        #region CreateUser

        [Test]
        public async Task CreateUser_RepositoryReturnsUser_ResultIsUser()
        {
            //Arrange
            var name = _fixture.Create<string>();
            var user = _fixture.Create<User>();

            _userRepository.Setup(x => x.Create(It.IsAny<string>())).ReturnsAsync(user);

            //Act
            var result = await _userService.CreateUser(name);

            //Assert
            Assert.Multiple(() => {
                Assert.IsNotNull(result, "IsNotNull");
                Assert.IsInstanceOf<User>(result, "IsInstanceOf");
                Assert.AreEqual(user.Id, result.Id, "Id");
                Assert.AreEqual(user.Name, result.Name, "Name");
            });

            _userRepository.Verify(x => x.Create(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task CreateUser_RepositoryReturnsNull_ResultIsNull()
        {
            //Arrange
            var name = _fixture.Create<string>();

            _userRepository.Setup(x => x.Create(It.IsAny<string>())).ReturnsAsync(() => null);

            //Act
            var result = await _userService.CreateUser(name);

            //Assert
            Assert.IsNull(result);

            _userRepository.Verify(x => x.Create(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region GetAll

        [Test]
        public async Task GetAll_RepositoryReturnsNull_ResultIsNull()
        {
            //Arrange
            _userRepository.Setup(x => x.GetAll()).ReturnsAsync(() => null);

            //Act
            var result = await _userService.GetAll();

            //Assert
            Assert.IsNull(result);

            _userRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public async Task GetAll_RepositoryReturnsListOfUsers_ResultIsListOfUsers()
        {
            //Arrange
            var users = _fixture.CreateMany<User>();

            _userRepository.Setup(x => x.GetAll()).ReturnsAsync(users);

            //Act
            var result = await _userService.GetAll();

            //Assert
            Assert.Multiple(() => {
                Assert.IsNotNull(result, "IsNotNull");
                Assert.IsInstanceOf<IEnumerable<User>>(result, "IsInstanceOf");
                Assert.AreEqual(users.Count(), result.Count(), "Count");
            });

            _userRepository.Verify(x => x.GetAll(), Times.Once);
        }

        #endregion

        #region UpdateUser

        [TestCase(true)]
        [TestCase(false)]
        public async Task UpdateUser_ResultIsRepositoryResponse(bool repositoryResponse)
        {
            //Arrange
            var user = _fixture.Create<User>();

            _userRepository.Setup(x => x.UpdateUser(It.IsAny<User>())).ReturnsAsync(repositoryResponse);

            //Act
            var result = await _userService.UpdateUser(user);

            //Assert
            Assert.Multiple(() => {
                Assert.IsNotNull(result, "IsNotNull");
                Assert.IsInstanceOf<bool>(result, "IsInstanceOf");
                Assert.AreEqual(repositoryResponse, result, "Count");
            });

            _userRepository.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
        }

        #endregion
    }
}

