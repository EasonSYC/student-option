CREATE TABLE ExamBoards
(
    ExamBoardId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    ExamBoardName VARCHAR(20) NOT NULL UNIQUE
)
GO

CREATE TABLE Categories
(
    CategoryId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    CategoryName VARCHAR(30) NOT NULL UNIQUE
)
GO

CREATE TABLE Courses
(
    CourseId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Title VARCHAR(30) NOT NULL UNIQUE,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId),
    ExamBoardId INT NOT NULL FOREIGN KEY REFERENCES ExamBoards(ExamBoardId)
)
GO

CREATE TABLE Students
(
    StudentId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    FirstName VARCHAR(20) NOT NULL,
    LastName VARCHAR(20) NOT NULL,
    DateOfBirth DATE NOT NULL,
    UNIQUE(FirstName, LastName, DateOfBirth)
)
GO

CREATE TABLE Teachers
(
    TeacherId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Title VARCHAR(10) NOT NULL,
    FirstName VARCHAR(20) NOT NULL,
    LastName VARCHAR(20) NOT NULL,
    Qualification VARCHAR(50) NOT NULL,
    UNIQUE(FirstName, LastName)
)
GO

CREATE TABLE ClassSets
(
    ClassSetId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    TeacherId INT NOT NULL FOREIGN KEY REFERENCES Teachers(TeacherId),
    CourseId INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseId)
)
GO

CREATE TABLE ClassEnrollments
(
    ClassEnrollmentId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(StudentId),
    ClassSetId INT NOT NULL FOREIGN KEY REFERENCES ClassSets(ClassSetId),
    UNIQUE(StudentId, ClassSetId)
)
GO