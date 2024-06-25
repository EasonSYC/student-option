// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;

namespace StudentOption;

internal class Program
{
    static async Task Main()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string connectionString = config["ConnectionStrings:studentDb"] ?? string.Empty;
        DbConsoleInterface consoleInterface = new(connectionString);

        int choice = -1;
        while (choice != 0)
        {
            Console.Clear();
            Console.WriteLine(DbConsoleInterface.mainPromptText);

            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out choice) && choice >= 0 && choice <= 6)
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            await consoleInterface.ClassFromCourseInterfaceAsync();
                            break;
                        case 2:
                            await consoleInterface.StudentFromClassInterfaceAsync();
                            break;
                        case 3:
                            await consoleInterface.ClassFromStudentInterfaceAsync();
                            break;
                        case 4:
                            consoleInterface.EditInterface();
                            break;
                        case 5:
                            await consoleInterface.ValidateClassInterfaceAsync();
                            break;
                        case 6:
                            consoleInterface.ValidateStudentInterface();
                            break;
                        default:
                            break;
                    }
                }
                catch (NotImplementedException e)
                {
                    Console.Clear();
                    Console.WriteLine($"Not Implemented: {e.Source}, {e.Message}");
                    Console.WriteLine(DbConsoleInterface.waitToContinueText);
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine($"Exception: {e.Source}, {e.Message}");
                    Console.WriteLine(DbConsoleInterface.waitToContinueText);
                    Console.ReadLine();
                }

            }
            else
            {
                choice = -1;
            }
        }
    }
}