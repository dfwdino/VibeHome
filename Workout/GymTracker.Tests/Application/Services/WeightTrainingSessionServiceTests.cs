using GymTracker.Application.Services;
using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using Moq;
using Xunit;

namespace GymTracker.Tests.Application.Services;

public class WeightTrainingSessionServiceTests
{
    private readonly Mock<IWeightTrainingSessionRepository> _mockRepository;
    private readonly WeightTrainingSessionService _service;

    public WeightTrainingSessionServiceTests()
    {
        _mockRepository = new Mock<IWeightTrainingSessionRepository>();
        _service = new WeightTrainingSessionService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSessions()
    {
        // Arrange
        var expectedSessions = new List<WeightTrainingSession>
        {
            new() { Id = 1, WorkoutTypeId = 1, LocationId = 1, DateTime = DateTime.Now },
            new() { Id = 2, WorkoutTypeId = 2, LocationId = 2, DateTime = DateTime.Now }
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
        var expectedSession = new WeightTrainingSession
        {
            Id = 1,
            WorkoutTypeId = 1,
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
            .ReturnsAsync((WeightTrainingSession)null);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddSession()
    {
        // Arrange
        var session = new WeightTrainingSession
        {
            WorkoutTypeId = 1,
            LocationId = 1,
            DateTime = DateTime.Now
        };

        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<WeightTrainingSession>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.AddAsync(session);

        // Assert
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<WeightTrainingSession>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateSession()
    {
        // Arrange
        var session = new WeightTrainingSession
        {
            Id = 1,
            WorkoutTypeId = 1,
            LocationId = 1,
            DateTime = DateTime.Now
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<WeightTrainingSession>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(session);

        // Assert
        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<WeightTrainingSession>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteSession()
    {
        // Arrange
        var session = new WeightTrainingSession
        {
            Id = 1,
            WorkoutTypeId = 1,
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