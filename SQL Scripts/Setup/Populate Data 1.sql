INSERT INTO ExamBoards (ExamBoardName) VALUES ('OCR')
INSERT INTO ExamBoards (ExamBoardName) VALUES ('AQA')
INSERT INTO ExamBoards (ExamBoardName) VALUES ('Edexcel')
INSERT INTO ExamBoards (ExamBoardName) VALUES ('WJEC')
INSERT INTO ExamBoards (ExamBoardName) VALUES ('CIE')
GO

INSERT INTO Categories (CategoryName) VALUES ('STEM')
INSERT INTO Categories (CategoryName) VALUES ('Arts and Design')
INSERT INTO Categories (CategoryName) VALUES ('Business and Economics')
INSERT INTO Categories (CategoryName) VALUES ('Social Sciences')
INSERT INTO Categories (CategoryName) VALUES ('Languages')
GO

INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Maths', 5, 1)
INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Sciences', 3, 1)
INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Computing', 4, 1)
GO

INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Art and Design', 2, 2)
INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Drama', 2, 2)
GO

INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Economics', 3, 3)
INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Business Studies', 3, 3)
GO

INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Ancient History', 5, 4)
INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Geography', 1, 4)
GO

INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('English', 5, 5)
INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('German', 3, 5)
INSERT INTO Courses (Title, CategoryID, ExamBoardID) VALUES ('Greek', 2, 5)
GO