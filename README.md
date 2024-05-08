# Student Option Application

## Database Design

### Courses

1. **CourseID**: Primary Key, Unique, Not Null, Auto-Increment; Int.
2. **Title**: Unique, Not Null; VarChar(20).
3. **CategoryID**: Foreign Key (Categories.CategoryID), Not Null; Int.
4. **ExamBoardID**: Foreign Key (ExamBoards.ExamBoardID), Not Null; Int.

### Students

1. **StudentID**: Primary Key, Unique, Not Null, Auto-Increment; Int.
2. **FirstName**: Not Null; VarChar(20).
3. **LastName**: Not Null; VarChar(20).
4. **DateOfBirth**: Not Null; Date.

Unique Constraint: FirstName, LastName, DateOfBirth.

### Teachers

1. **TeacherID**: Primary Key, Unique, Not Null, Auto-Increment; Int.
2. **Title**: Not Null; VarChar(10).
3. **FirstName**: Not Null; VarChar(20).
4. **LastName**: Not Null; VarChar(20).
5. **Qualification**: Not Null, VarChar(20).

Unique Constraint: FirstName, LastName.

### Classes

1. **ClassID**: Primary Key, Unique, Not Null, Auto-Increment; Int.
2. **TeacherID**: Foreign Key (Teachers.TeacherID), Not Null; Int.
3. **CourseID**: Foreign Key (Courses.CourseID), Not Null; Int.

### ClassEnrollments

1. **ClassEnrollmentID**: Primary Key, Unique, Not Null, Auto-Increment; Int.
2. **StudentID**: Foreign Key (Students.StudentID), Not Null; Int.
3. **ClassID**: Foreign Key (Classes.ClassID), Not Null; Int.

Unique Constraint: StudentID, ClassID.

### Categories

1. **CategoryID**: Primary Key, Unique, Not Null, Auto-Increment; Int.
2. **CategoryName**: Not Null, Unique; VarChar(30).

### ExamBoards

1. **ExamBoardID**: Primary Key, Unique, Not Null, Auto-Increment; Int.
2. **ExamBoardName**: Not Null, Unique; VarChar(20).
