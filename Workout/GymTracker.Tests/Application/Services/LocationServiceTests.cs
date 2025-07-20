using GymTracker.Application.Services;
using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using Moq;
using Xunit;

namespace GymTracker.Tests.Application.Services;

public class LocationServiceTests
{
    private readonly Mock<ILocationRepository> _mockRepository;
    private readonly LocationService _service;

    public LocationServiceTests()
    {
        _mockRepository = new Mock<ILocationRepository>();
        _service = new LocationService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllLocations()
    {
        // Arrange
        var expectedLocations = new List<Location>
        {
            new() { Id = 1, Name = "Location 1" },
            new() { Id = 2, Name = "Location 2" }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(expectedLocations);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(expectedLocations.Count, result.Count());
        Assert.Equal(expectedLocations[0].Id, result.First().Id);
        Assert.Equal(expectedLocations[1].Id, result.Last().Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnLocation_WhenExists()
    {
        // Arrange
        var expectedLocation = new Location
        {
            Id = 1,
            Name = "Location 1"
        };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(expectedLocation);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLocation.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((Location)null);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddLocation()
    {
        // Arrange
        var location = new Location
        {
            Name = "Location 1"
        };

        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Location>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.AddAsync(location);

        // Assert
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateLocation()
    {
        // Arrange
        var location = new Location
        {
            Id = 1,
            Name = "Location 1"
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Location>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(location);

        // Assert
        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteLocation()
    {
        // Arrange
        var location = new Location
        {
            Id = 1,
            Name = "Location 1"
        };

        _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(location.Id);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
    }
} 