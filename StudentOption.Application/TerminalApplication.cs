namespace StudentOption.Application;

using StudentOption.Data;
using Microsoft.Extensions.Configuration;

public class TerminalApplication
{
    private const string _waitToContinueText = "Press enter to continue ...";

    private const string _mainPromptText = @"Please input the relavant number to execute the relavant function.
1. Display classes for a course subject.
2. Display students enrolled in a class.
3. Display the classes a student is enrolled in.
4. Add/Remove/Update data.
5. Validate class for number of students enrolled.
6. Validate student subject choices.

0. Exit

Please input your choice:";

    private static StudentOptionDB GetDataBase()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string connectionString = config["ConnectionStrings:studentDb"] ?? string.Empty;
        return new(connectionString);
    }
    public static void MainInterface()
    {
        int choice = -1;
        while (choice != 0)
        {
            Console.Clear();
            Console.WriteLine(_mainPromptText);

            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out choice) && choice >= 0 && choice <= 6)
            {
                switch (choice)
                {
                    case 1:
                        ClassFromCourseInterface();
                        break;
                    case 2:
                        StudentFromClassInterface();
                        break;
                    case 3:
                        ClassFromStudentInterface();
                        break;
                    case 4:
                        EditInterface();
                        break;
                    case 5:
                        ValidateClassInterface();
                        break;
                    case 6:
                        ValidateStudentInterface();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                choice = -1;
            }
        }
    }

    private const string _coursesText = "There are the following courses:";
    private const string _courseHeadersText = "ID\tTitle\tCategory\tExam Board"; // TO-DO: Output formatting
    private const string _coursePromptText = "Please input the desired course ID:";
    private const string _classSetText = "There are the following class sets:";

    private const string _classSetHeadersText = "ID\tTeacher";
    private static void ClassFromCourseInterface()
    {   
        StudentOptionDB dataBase = GetDataBase();
        List<(int id, string title, string category, string examBoard)> courses = dataBase.GetCourses();

        bool valid = false;
        int choice = -1;

        while (!valid)
        {
            Console.Clear();
            Console.WriteLine(_coursesText);
            Console.WriteLine(_courseHeadersText);
            foreach ((int id, string title, string category, string examBoard) in courses)
            {
                Console.WriteLine($"{id}\t{title}\t{category}\t{examBoard}");
            }

            Console.WriteLine(_coursePromptText);
            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out choice))
            {
                foreach ((int id, string title, string category, string examBoard) in courses)
                {
                    if (id == choice)
                    {
                        valid = true;
                    }
                }
            }
        }

        List<(int id, string title, string firstName, string lastName, string)> classSets = dataBase.GetClassSetsFromCoruseID(choice);

        Console.WriteLine(_classSetText);
        Console.WriteLine(_classSetHeadersText);
        foreach ((int id, string title, string firstName, string lastName, _) in classSets)
        {
            Console.WriteLine($"{id}\t{title} {firstName} {lastName}");
        }

        Console.WriteLine(_waitToContinueText);
        Console.ReadLine();

        return;
    }

    private static void StudentFromClassInterface()
    {
        throw new NotImplementedException();
    }

    private static void ClassFromStudentInterface()
    {
        throw new NotImplementedException();
    }

    private static void EditInterface()
    {
        throw new NotImplementedException();
    }

    private static void ValidateClassInterface()
    {
        throw new NotImplementedException();
    }

    private static void ValidateStudentInterface()
    {
        throw new NotImplementedException();
    }
}