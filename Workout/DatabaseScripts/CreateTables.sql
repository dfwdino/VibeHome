USE GymTracker;

CREATE SCHEMA workout;

CREATE TABLE workout.WorkoutTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE workout.CardioTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE workout.Locations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE workout.WeightTrainingSessions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DateTime DATETIME NOT NULL,
    Reps INT NOT NULL,
    Sets INT NOT NULL,
    Weight DECIMAL(10,2) NOT NULL,
    WorkoutTypeId INT NOT NULL,
    LocationId INT NOT NULL,
    FOREIGN KEY (WorkoutTypeId) REFERENCES workout.WorkoutTypes(Id),
    FOREIGN KEY (LocationId) REFERENCES workout.Locations(Id)
);

CREATE TABLE workout.CardioSessions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DateTime DATETIME NOT NULL,
    Duration TIME NOT NULL,
    Distance DECIMAL(10,2) NOT NULL,
    CaloriesBurned INT NOT NULL,
    CardioTypeId INT NOT NULL,
    LocationId INT NOT NULL,
    FOREIGN KEY (CardioTypeId) REFERENCES workout.CardioTypes(Id),
    FOREIGN KEY (LocationId) REFERENCES workout.Locations(Id)
); 