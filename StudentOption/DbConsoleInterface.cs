namespace StudentOption;

using StudentOption.Data;
using StudentOption.Classes;
using System.Text;

internal class DbConsoleInterface(string connectionString)
{
    #region ConstStrings // TO-DO: Output formatting

    private const string _coursesText = "There are the following courses:";
    private const string _courseHeadersText = "Id\tTitle\tCategory\tExam Board";
    private const string _coursePromptText = "Please input the desired Course Id:";
    private const string _classSetText = "There are the following class sets for Course Id @1, Name @2, Category @3, Exam Board @4:";
    private const string _classSetWithStudentText = "There are the following class sets for Student Id @1, Name @2, DoB @3:";
    private const string _classSetHeadersText = "Id\tTeacher";
    private const string _classSetHeadersWithCourseText = "Id\tCourse\tTeacher";
    private const string _classSetPromptText = "Please input the desired Class Set Id:";
    private const string _studentText = "There are the following students for Class Set Id @1 in Subject @2 with Teacher @3:";
    private const string _studentsText = "There are the following students:";
    private const string _studentHeadersText = "Id\tName\tDate of Birth";
    private const string _studentPromptText = "Please input the desired Student Id:";
    private const string _studentValidateText = "There are @0 students for Class Set Id @1 in Subject @2 with Teacher @3.";
    private const string _validText = "It is valid.";
    private const string _invalidText = "It is invalid.";
    internal const string waitToContinueText = "Press enter to continue ...";
    internal const string mainPromptText = @"Please input the relavant number to execute the relavant function.
1. Display classes for a course subject.
2. Display students enrolled in a class.
3. Display the classes a student is enrolled in.
4. Add/Remove/Update data. *** NOT IMPLEMENTED ***
5. Validate class for number of students enrolled.
6. Validate student subject choices. *** NOT IMPLEMENTED ***

0. Exit

Please input your choice:";

    #endregion
    
    private readonly StudentOptionDb _dataBase = new(connectionString);

    #region DisplayMethods

    private async Task<string> DisplayCoursesAsync()
    {
        StringBuilder sb = new();
        sb.AppendLine(_coursesText);
        sb.AppendLine(_courseHeadersText);

        List<Course> courses = await _dataBase.GetCoursesAsync();
        courses.ForEach(course => sb.AppendLine($"{course.Id}\t{course.Title}\t{course.Category}\t{course.ExamBoard}"));

        return sb.ToString();
    }
    private async Task<string> DisplayClassSetsFromCourseAsync(Course course)
    {
        StringBuilder sb = new();

        sb.AppendLine(_classSetText.Replace("@1", course.Id.ToString()).Replace("@2", course.Title).Replace("@3", course.Category).Replace("@4", course.ExamBoard));
        sb.AppendLine(_classSetHeadersText);

        List<ClassSet> classSets = await _dataBase.GetClassSetsFromCoruseAsync(course);
        classSets.ForEach(classSet => sb.AppendLine($"{classSet.Id}\t{classSet.Teacher.Title} {classSet.Teacher.FirstName} {classSet.Teacher.LastName}"));

        return sb.ToString();
    }
    private async Task<string> DisplayStudentsFromClassSetAsync(ClassSet classSet)
    {
        StringBuilder sb = new();

        sb.AppendLine(_studentText.Replace("@1", classSet.Id.ToString()).Replace("@2", classSet.Course.Title).Replace("@3", $"{classSet.Teacher.Title} {classSet.Teacher.FirstName} {classSet.Teacher.LastName}"));
        sb.AppendLine(_studentHeadersText);

        List<Student> students = await _dataBase.GetStudentsFromClassSetAsync(classSet);
        students.ForEach(student => sb.AppendLine($"{student.Id}\t{student.FirstName} {student.LastName}\t{student.DateOfBirth}"));

        return sb.ToString();
    }
    private async Task<string> DisplayStudentsAsync()
    {
        StringBuilder sb = new();

        sb.AppendLine(_studentsText);
        sb.AppendLine(_studentHeadersText);

        List<Student> students = await _dataBase.GetStudentsAsync();
        students.ForEach(student => sb.AppendLine($"{student.Id}\t{student.FirstName} {student.LastName}\t{student.DateOfBirth}"));

        return sb.ToString();
    }
    private async Task<string> DisplayClassSetsFromStudentAsync(Student student)
    {
        StringBuilder sb = new();

        sb.AppendLine(_classSetWithStudentText.Replace("@1", student.Id.ToString()).Replace("@2", $"{student.FirstName} {student.LastName}").Replace("@3", student.DateOfBirth.ToString()));
        sb.AppendLine(_classSetHeadersWithCourseText);

        List<ClassSet> classSets = await _dataBase.GetClassSetsFromStudentAsync(student);
        classSets.ForEach(classSet => sb.AppendLine($"{classSet.Id}\t{classSet.Course.Title}\t{classSet.Teacher.Title} {classSet.Teacher.FirstName} {classSet.Teacher.LastName}"));

        return sb.ToString();
    }
    
    #endregion

    #region ChooseInterfaces

    private async Task<Course> ChooseCourseInterfaceAsync()
    {
        bool valid = false;
        Course courseChoice = Course.Default;
        string displayCourses = await DisplayCoursesAsync();

        while (!valid)
        {
            Console.Clear();
            Console.WriteLine(displayCourses);

            Console.WriteLine(_coursePromptText);
            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out int idChoice) && await _dataBase.ExistCourseIdAsync(idChoice))
            {
                courseChoice = await _dataBase.GetCourseByIdAsync(idChoice);
                valid = true;
            }
        }

        return courseChoice;
    }
    private async Task<ClassSet> ChooseClassSetFromCourseInterfaceAsync(Course course)
    {
        bool valid = false;
        ClassSet classSetChoice = ClassSet.Default;
        string displayClassSetsFromCourse = await DisplayClassSetsFromCourseAsync(course);

        while (!valid)
        {
            Console.Clear();
            Console.WriteLine(displayClassSetsFromCourse);

            Console.WriteLine(_classSetPromptText);
            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out int idChoice) && await _dataBase.ExistClassSetIdWithCourseAsync(course, idChoice))
            {
                classSetChoice = await _dataBase.GetClassSetByIdWithCourseAsync(course, idChoice);
                valid = true;
            }
        }

        return classSetChoice;
    }
    private async Task<Student> ChooseStudentInterfaceAsync()
    {
        bool valid = false;
        Student studentChoice = Student.Default;
        string displayStudents = await DisplayStudentsAsync();

        while (!valid)
        {
            Console.Clear();
            Console.WriteLine(displayStudents);

            Console.WriteLine(_studentPromptText);
            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out int idChoice) && await _dataBase.ExistStudentIdAsync(idChoice))
            {
                studentChoice = await _dataBase.GetStudentByIdAsync(idChoice);
                valid = true;
            }
        }

        return studentChoice;
    }

    #endregion

    #region FunctionalInteraces

    internal async Task ClassFromCourseInterfaceAsync()
    {
        Course courseChoice = await ChooseCourseInterfaceAsync();
        string displayClassSets = await DisplayClassSetsFromCourseAsync(courseChoice);

        Console.Clear();
        Console.WriteLine(displayClassSets);

        Console.WriteLine(waitToContinueText);
        Console.ReadLine();

        return;
    }
    internal async Task StudentFromClassInterfaceAsync()
    {
        ClassSet classSetChoice = await ChooseClassSetFromCourseInterfaceAsync(await ChooseCourseInterfaceAsync());
        string displayStudentsFromClassSet = await DisplayStudentsFromClassSetAsync(classSetChoice);

        Console.Clear();
        Console.WriteLine(displayStudentsFromClassSet);

        Console.WriteLine(waitToContinueText);
        Console.ReadLine();

        return;
    }
    internal async Task ClassFromStudentInterfaceAsync()
    {
        Student chosenStudent = await ChooseStudentInterfaceAsync();
        string displayClassSetsFromStudent = await DisplayClassSetsFromStudentAsync(chosenStudent);

        Console.Clear();
        Console.WriteLine(displayClassSetsFromStudent);

        Console.WriteLine(waitToContinueText);
        Console.ReadLine();

        return;
    }
    internal void EditInterface()
    {
        throw new NotImplementedException();
    }
    private const int _minStudentNo = 5;
    private const int _maxStudentNo = 15;
    internal async Task ValidateClassInterfaceAsync()
    {
        ClassSet classSet = await ChooseClassSetFromCourseInterfaceAsync(await ChooseCourseInterfaceAsync());
        int studentNo = await _dataBase.GetStudentNoByClassSetAsync(classSet);

        Console.Clear();
        Console.WriteLine(_studentValidateText.Replace("@0", studentNo.ToString()).Replace("@1", classSet.Id.ToString()).Replace("@2", classSet.Course.Title).Replace("@3", $"{classSet.Teacher.Title} {classSet.Teacher.FirstName} {classSet.Teacher.LastName}"));

        if (studentNo >= _minStudentNo && studentNo <= _maxStudentNo)
        {
            Console.WriteLine(_validText);
        }
        else
        {
            Console.WriteLine(_invalidText);
        }

        Console.WriteLine(waitToContinueText);
        Console.ReadLine();

        return;
    }
    internal void ValidateStudentInterface()
    {
        throw new NotImplementedException();
    }

    #endregion
}