using GymTracker.Application.Services;
using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using Moq;
using Xunit;

namespace GymTracker.Tests.Application.Services;

public class CardioSessionServiceTests
{
    private readonly Mock<ICardioSessionRepository> _mockRepository;
    private readonly CardioSessionService _service;

    public CardioSessionServiceTests()
    {
        _mockRepository = new Mock<ICardioSessionRepository>();
        _service = new CardioSessionService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSessions()
    {
        // Arrange
        var expectedSessions = new List<CardioSession>
        {
            new() { Id = 1, CardioTypeId = 1, LocationId = 1, DateTime = DateTime.Now },
            new() { Id = 2, CardioTypeId = 2, LocationId = 2, DateTime = DateTime.Now }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(expectedSessions);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(expectedSessions.Count, result.Count());
        Assert.Equal(expectedSessions[0].Id, result.First().Id);
        Assert.Equal(expectedSessions[1].Id, result.Last().Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnSession_WhenExists()
    {
        // Arrange
        var expectedSession = new CardioSession
        {
            Id = 1,
            CardioTypeId = 1,
            LocationId = 1,
            DateTime = DateTime.Now
        };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(expectedSession);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedSession.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((CardioSession)null);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddSession()
    {
        // Arrange
        var session = new CardioSession
        {
            CardioTypeId = 1,
            LocationId = 1,
            DateTime = DateTime.Now
        };

        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<CardioSession>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.AddAsync(session);

        // Assert
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<CardioSession>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateSession()
    {
        // Arrange
        var session = new CardioSession
        {
            Id = 1,
            CardioTypeId = 1,
            LocationId = 1,
            DateTime = DateTime.Now
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CardioSession>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(session);

        // Assert
        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<CardioSession>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteSession()
    {
        // Arrange
        var session = new CardioSession
        {
            Id = 1,
            CardioTypeId = 1,
            LocationId = 1,
            DateTime = DateTime.Now
        };

        _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(session.Id);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
    }
} 