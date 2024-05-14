namespace StudentOption.Application;

public class TerminalApplication
{
    private const string _invalidInputText = "Invalid input.";
    private const string _mainPromptText = @"
    Please input the relavant number to execute the relavant function.\n
    1. Display classes for a course subject.\n
    2. Display students enrolled in a class.\n
    3. Display the classes a student is enrolled in.\n
    4. Add/Remove/Update data.\n
    5. Validate class for number of students enrolled.\n
    6. Validate student subject choices.\n
    \n
    0. Exit\n
    \n
    Please input your choice:";

    public static void MainInterface()
    {
        int choice = -1;
        while (choice != 0)
        {
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
                Console.WriteLine(_invalidInputText);
            }
        }
    }

    private static void ClassFromCourseInterface()
    {
        throw new NotImplementedException();
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
