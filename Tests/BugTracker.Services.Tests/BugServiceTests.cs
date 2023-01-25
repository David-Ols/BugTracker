using NUnit.Framework;
using Moq;
using AutoFixture;
using BugTracker.Services;
using BugTracker.Mappers.Interfaces;
using BugTracker.Repository.Interfaces;
using BugTracker.Models;
using BugTracker.Dtos;

namespace BugTracker.Services.Tests;


[TestFixture]
public class BugServiceTests
{
    private BugService _bugService;

    private Mock<IBugRepository> _bugRepository;
    private Mock<IUserRepository> _userRepository;
    private Mock<IBugMapper> _bugMapper;

    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();

        _bugRepository = new Mock<IBugRepository>();
        _userRepository = new Mock<IUserRepository>();
        _bugMapper = new Mock<IBugMapper>();

        _bugService = new BugService(_bugRepository.Object, _bugMapper.Object, _userRepository.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _bugMapper.Invocations.Clear();
        _bugRepository.Invocations.Clear();
        _userRepository.Invocations.Clear();
    }

    #region GetByPublicId

    [Test]
    public async Task GetByPublicId_BugNotFound_ResultIsNull()
    {
        //Arrange
        var publicId = _fixture.Create<string>();

        _bugRepository.Setup(x => x.GetByPublicId(It.IsAny<string>())).ReturnsAsync(() => null);

        //Act
        var result = await _bugService.GetByPublicId(publicId);

        //Assert
        Assert.IsNull(result);

        _userRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
        _bugMapper.Verify(x => x.Map(It.IsAny<Bug>(), It.IsAny<User>()), Times.Never);
    }

    [Test]
    public async Task GetByPublicId_BugHasUserId_RequestingUserOnce()
    {
        //Arrange
        var publicId = _fixture.Create<string>();
        var bug = _fixture.Create<Bug>();

        _bugRepository.Setup(x => x.GetByPublicId(It.IsAny<string>())).ReturnsAsync(bug);

        //Act
        var result = await _bugService.GetByPublicId(publicId);

        //Assert
        _userRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
    }

    [Test]
    public async Task GetByPublicId_BugDoesNotHaveUserId_RequestingUserNever()
    {
        //Arrange
        var publicId = _fixture.Create<string>();
        var bug = _fixture.Build<Bug>().Without(b => b.UserId).Create();

        _bugRepository.Setup(x => x.GetByPublicId(It.IsAny<string>())).ReturnsAsync(bug);

        //Act
        var result = await _bugService.GetByPublicId(publicId);

        //Assert
        _userRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
    }

    [Test]
    public async Task GetByPublicId_CallMapper_ResultIsMapperResponse()
    {
        //Arrange
        var publicId = _fixture.Create<string>();
        var bug = _fixture.Create<Bug>();
        var user = _fixture.Create<User>();
        var bugDto = _fixture.Create<BugDto>();

        _bugRepository.Setup(x => x.GetByPublicId(It.IsAny<string>())).ReturnsAsync(bug);
        _userRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
        _bugMapper.Setup(x => x.Map(It.IsAny<Bug>(), It.IsAny<User>())).Returns(bugDto);

        //Act
        var result = await _bugService.GetByPublicId(publicId);

        //Assert
        Assert.Multiple(() => {
            Assert.IsNotNull(result, "IsNotNull");
            Assert.AreEqual(bugDto.AssigneeName, result.AssigneeName, "AssigneeName");
            Assert.AreEqual(bugDto.Description, result.Description, "Description");
            Assert.AreEqual(bugDto.OpenedOn, result.OpenedOn, "OpenedOn");
            Assert.AreEqual(bugDto.PublicId, result.PublicId, "PublicId");
            Assert.AreEqual(bugDto.Status, result.Status, "Status");
            Assert.AreEqual(bugDto.Title, result.Title, "Title");
            Assert.AreEqual(bugDto.Id, result.Id, "Id");
            Assert.AreEqual(bugDto.UserId, result.UserId, "UserId");
        });

        _userRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        _bugMapper.Verify(x => x.Map(It.IsAny<Bug>(), It.IsAny<User>()), Times.Once);
    }

    #endregion

    #region CreateBug

    [Test]
    public async Task CreateBug_RepositoryReturnsNull_ResultIsNull()
    {
        //Arrange
        var request = _fixture.Create<CreateBug>();

        _bugRepository.Setup(x => x.CreateBug(It.IsAny<CreateBug>())).ReturnsAsync(() => null);

        //Act
        var result = await _bugService.CreateBug(request);

        //Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task CreateBug_RepositoryReturnsBugModel_ResultIsBugModel()
    {
        //Arrange
        var request = _fixture.Create<CreateBug>();
        var bugResponse = _fixture.Create<Bug>();

        _bugRepository.Setup(x => x.CreateBug(It.IsAny<CreateBug>())).ReturnsAsync(bugResponse);

        //Act
        var result = await _bugService.CreateBug(request);

        //Assert
        Assert.Multiple(() => {
            Assert.NotNull(result, "NotNull");
            Assert.AreEqual(bugResponse.Id, result.Id, "Id");
            Assert.AreEqual(bugResponse.PublicId, result.PublicId);
            Assert.AreEqual(bugResponse.Title, result.Title);
            Assert.AreEqual(bugResponse.Status, result.Status);
            Assert.AreEqual(bugResponse.Description, result.Description);
            Assert.AreEqual(bugResponse.OpenedOn, result.OpenedOn);
            Assert.AreEqual(bugResponse.UserId, result.UserId);
        });
    }

    #endregion

    #region GetAllBugs

    [Test]
    public async Task GetAllBugs_RepositoryReturnsNull_ResultIsNull()
    {
        //Arrange
        _bugRepository.Setup(x => x.GetAllBugs()).ReturnsAsync(() => null);

        //Act
        var result = await _bugService.GetAllBugs();

        //Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetAllBugs_RepositoryReturnsListOfBugsModel_ResultIsListOfBugsModel()
    {
        //Arrange
        var bugs = _fixture.CreateMany<Bug>();

        _bugRepository.Setup(x => x.GetAllBugs()).ReturnsAsync(bugs);

        //Act
        var result = await _bugService.GetAllBugs();

        //Assert
        Assert.Multiple(() => {
            Assert.NotNull(result, "NotNull");
            Assert.AreEqual(bugs.Count(), result.Count(), "Count");
        });
    }

    #endregion

    #region UpdateBugStatus

    [TestCase(true)]
    [TestCase(false)]
    public async Task UpdateBugStatus_ResultIsResponseFromRepository(bool repositoryResponse)
    {
        //Arrange
        var request = _fixture.Create<BugStatusUpdate>();

        _bugRepository.Setup(x => x.UpdateBugStatus(It.IsAny<BugStatusUpdate>())).ReturnsAsync(repositoryResponse);

        //Act
        var result = await _bugService.UpdateBugStatus(request);

        //Assert
        Assert.AreEqual(repositoryResponse, result);
    }

    #endregion
}
