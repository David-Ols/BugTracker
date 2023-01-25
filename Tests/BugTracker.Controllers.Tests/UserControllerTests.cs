using NUnit.Framework;
using Moq;
using AutoFixture;
using Microsoft.Extensions.Logging;
using BugTracker.Services.Interfaces;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace BugTracker.Controllers.Tests;

[TestFixture]
public class UserControllerTests
{
    private UserController _userController;

    private Mock<ILogger<UserController>> _logger;
    private Mock<IUserService> _userService;

    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();

        _logger = new Mock<ILogger<UserController>>();
        _userService = new Mock<IUserService>();

        _userController = new UserController(_logger.Object, _userService.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _logger.Invocations.Clear();
        _userService.Invocations.Clear();
    }

    #region Get

    [Test]
    public async Task Get_UserServiceReturnsUsers_ResultIsOk()
    {
        //Arrange
        var users = _fixture.CreateMany<User>();
        var expectedStatusCode = 200;

        _userService.Setup(x => x.GetAll()).ReturnsAsync(users);

        //Act
        var result = await _userController.Get();
        var okResult = result as OkObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode);
            Assert.AreEqual(users.Count(), ((IEnumerable<User>)okResult.Value).Count());
        });

        _userService.Verify(x => x.GetAll(), Times.Once);
    }

    [Test]
    public async Task Get_UserServiceThrowsError_ResultIsBadRequest()
    {
        //Arrange
        var expectedMessage = "Failed to get all users.";
        var expectedStatusCode = 400;

        _userService.Setup(x => x.GetAll()).ThrowsAsync(new Exception(""));

        //Act
        var result = await _userController.Get();
        var badRequestResult = result as BadRequestObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);
            Assert.AreEqual(expectedStatusCode, badRequestResult.StatusCode);
            Assert.AreEqual(expectedMessage, badRequestResult.Value);
        });
    }

    #endregion

    #region Post

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public async Task Post_InvalidName_ResultIsBadRequest(string requestName)
    {
        //Arrange
        var expectedMessage = "Name can not be empty";
        var expectedStatusCode = 400;

        //Act
        var result = await _userController.Post(requestName);
        var badRequestResult = result as BadRequestObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);
            Assert.AreEqual(expectedStatusCode, badRequestResult.StatusCode);
            Assert.AreEqual(expectedMessage, badRequestResult.Value);
        });
    }

    [Test]
    public async Task Post_ServiceReturnsUser_ResultIsOk()
    {
        //Arrange
        var name = _fixture.Create<string>();
        var user = _fixture.Create<User>();
        var expectedStatusCode = 200;

        _userService.Setup(x => x.CreateUser(It.IsAny<string>())).ReturnsAsync(user);

        //Act
        var result = await _userController.Post(name);
        var okResult = result as OkObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode);
            Assert.IsInstanceOf<User>(okResult.Value);
        });

        _userService.Verify(x => x.CreateUser(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task Post_ServiceThrowsException_ResultIsBadRequest()
    {
        //Arrange
        var name = _fixture.Create<string>();
        var expectedStatusCode = 400;
        var expectedMessage = $"Failed to create user for name: {name}";

        _userService.Setup(x => x.CreateUser(It.IsAny<string>())).ThrowsAsync(new Exception(""));

        //Act
        var result = await _userController.Post(name);
        var badRequestResult = result as BadRequestObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);
            Assert.AreEqual(expectedStatusCode, badRequestResult.StatusCode);
            Assert.AreEqual(expectedMessage, badRequestResult.Value);
        });
    }

    #endregion

    #region Put

    [Test]
    public async Task Put_RequestIsNull_ResultIsBadRequest()
    {
        //Arrange
        var expectedStatusCode = 400;
        var expectedMessage = "Request model is not valid!";

        //Act
        var result = await _userController.Put(null);
        var badRequestResult = result as BadRequestObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);
            Assert.AreEqual(expectedStatusCode, badRequestResult.StatusCode);
            Assert.AreEqual(expectedMessage, badRequestResult.Value);
        });

        _userService.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public async Task Put_RequestNameIsNotValid_ResultIsBadRequest(string requestName)
    {
        //Arrange
        var expectedStatusCode = 400;
        var expectedMessage = "New name is not valid!";
        var userRequest = _fixture.Build<User>().With(x => x.Name, requestName).Create();

        //Act
        var result = await _userController.Put(userRequest);
        var badRequestResult = result as BadRequestObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);
            Assert.AreEqual(expectedStatusCode, badRequestResult.StatusCode);
            Assert.AreEqual(expectedMessage, badRequestResult.Value);
        });

        _userService.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public async Task Put_RequestIdIsNotValid_ResultIsBadRequest()
    {
        //Arrange
        var expectedStatusCode = 400;
        var expectedMessage = "User Id is not valid!";
        var userRequest = _fixture.Build<User>().With(x => x.Id, Guid.Empty).Create();

        //Act
        var result = await _userController.Put(userRequest);
        var badRequestResult = result as BadRequestObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);
            Assert.AreEqual(expectedStatusCode, badRequestResult.StatusCode);
            Assert.AreEqual(expectedMessage, badRequestResult.Value);
        });

        _userService.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task Put_UserIsUpdated_ResultIsOk(bool serviceResponse)
    {
        //Arrange
        var expectedStatusCode = 200;
        var userRequest = _fixture.Create<User>();

        _userService.Setup(x => x.UpdateUser(It.IsAny<User>())).ReturnsAsync(serviceResponse);

        //Act
        var result = await _userController.Put(userRequest);
        var okResult = result as OkObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode);
            Assert.AreEqual(serviceResponse, ((bool)okResult.Value));
        });

        _userService.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public async Task Put_ServiceThrowsException_ResultIsBadRequest()
    {
        //Arrange
        var expectedStatusCode = 400;
        var userRequest = _fixture.Create<User>();
        var expectedMessage = $"Failed to update user {userRequest.Name}.";
        
        _userService.Setup(x => x.UpdateUser(It.IsAny<User>())).ThrowsAsync(new Exception(""));

        //Act
        var result = await _userController.Put(userRequest);
        var badRequestResult = result as BadRequestObjectResult;

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);
            Assert.AreEqual(expectedStatusCode, badRequestResult.StatusCode);
            Assert.AreEqual(expectedMessage, badRequestResult.Value);
        });
    }

    #endregion
}
