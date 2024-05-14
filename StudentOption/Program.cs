﻿// See https://aka.ms/new-console-template for more information

using StudentOption.Data;
using StudentOption.Application;
using Microsoft.Extensions.Configuration;

TerminalApplication.MainInterface();

var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
string connectionString = config["ConnectionStrings:studentDb"] ?? string.Empty;
Console.WriteLine(connectionString);
StudentOptionDB db = new(connectionString);

List<string> names = db.GetStudentNames();
Console.WriteLine("Students:");
foreach (var name in names)
{
    Console.WriteLine(name);
}