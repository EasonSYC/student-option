// See https://aka.ms/new-console-template for more information

using StudentOption.Application;
using Microsoft.Extensions.Configuration;

namespace StudentOption;

internal class Program
{
    static void Main()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string connectionString = config["ConnectionStrings:studentDb"] ?? string.Empty;
        new TerminalApplication(connectionString).MainInterface();
    }
}