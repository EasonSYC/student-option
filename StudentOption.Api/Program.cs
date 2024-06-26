using StudentOption.Classes;
using StudentOption.Data;

namespace StudentOption.Api;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        string connectionString = app.Configuration["ConnectionStrings:studentDb"] ?? string.Empty;
        app.Logger.Log(LogLevel.Information, "Connection string: {connectionString}", connectionString);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        StudentOptionDb db = new(connectionString);

        app.MapGet("/course", async () =>
        {
            return Results.Ok(await db.GetCoursesAsync());
        }).WithName("GetCourse").WithOpenApi();

        app.MapGet("/course/{courseId}", async (int courseId) =>
        {
            return Results.Ok(await db.GetCourseByIdAsync(courseId));
        }).WithName("GetCoruseByCourseId").WithOpenApi();

        app.MapGet("/student", async () =>
        {
            return Results.Ok(await db.GetStudentsAsync());
        }).WithName("GetStudent").WithOpenApi();

        app.MapGet("/student/{studentId}", async (int studentId) =>
        {
            return Results.Ok(await db.GetStudentByIdAsync(studentId));
        }).WithName("GetStudentByStudentId").WithOpenApi();

        app.MapGet("/student/classset/{classSetId}", async (int classSetId) =>
        {
            return Results.Ok(await db.GetStudentsFromClassSetAsync(await db.GetClassSetByIdAsync(classSetId)));
        }).WithName("GetStudentByClassSetId").WithOpenApi();

        app.MapGet("/classset/{classSetId}", async (int classSetId) => 
        {
            return Results.Ok(await db.GetClassSetByIdAsync(classSetId));
        }).WithName("GetClassSetByClassSetId").WithOpenApi();

        app.MapGet("/classset/course/{courseId}", async (int courseId) => 
        {
            return Results.Ok(await db.GetClassSetsFromCoruseAsync(await db.GetCourseByIdAsync(courseId)));
        }).WithName("GetClassSetByCourseId").WithOpenApi();

        app.MapGet("/classset/student/{studentId}", async (int studentId) =>
        {
            return Results.Ok(await db.GetClassSetsFromStudentAsync(await db.GetStudentByIdAsync(studentId)));
        }).WithName("GetClassSetByStudentId").WithOpenApi();

        app.Run();
    }
}