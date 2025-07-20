using GymTracker.Application.Services;
using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using Moq;
using Xunit;

namespace GymTracker.Tests.Application.Services;

public class WorkoutTypeServiceTests
{
    private readonly Mock<IWorkoutTypeRepository> _mockRepository;
    private readonly WorkoutTypeService _service;

    public WorkoutTypeServiceTests()
    {
        _mockRepository = new Mock<IWorkoutTypeRepository>();
        _service = new WorkoutTypeService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTypes()
    {
        // Arrange
        var expectedTypes = new List<WorkoutType>
        {
            new() { Id = 1, Name = "Type 1" },
            new() { Id = 2, Name = "Type 2" }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(expectedTypes);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(expectedTypes.Count, result.Count());
        Assert.Equal(expectedTypes[0].Id, result.First().Id);
        Assert.Equal(expectedTypes[1].Id, result.Last().Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnType_WhenExists()
    {
        // Arrange
        var expectedType = new WorkoutType
        {
            Id = 1,
            Name = "Type 1"
        };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(expectedType);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedType.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((WorkoutType)null);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddType()
    {
        // Arrange
        var type = new WorkoutType
        {
            Name = "Type 1"
        };

        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<WorkoutType>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.AddAsync(type);

        // Assert
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<WorkoutType>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateType()
    {
        // Arrange
        var type = new WorkoutType
        {
            Id = 1,
            Name = "Type 1"
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<WorkoutType>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(type);

        // Assert
        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<WorkoutType>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteType()
    {
        // Arrange
        var type = new WorkoutType
        {
            Id = 1,
            Name = "Type 1"
        };

        _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(type.Id);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
    }
} 