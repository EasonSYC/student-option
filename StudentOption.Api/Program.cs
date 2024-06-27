using StudentOption.Data;
using StudentOption.Classes;

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
            try
            {
                Course course = await db.GetCourseByIdAsync(courseId);
                return Results.Ok(course);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetCoruseByCourseId").WithOpenApi();

        app.MapGet("/student", async () =>
        {
            return Results.Ok(await db.GetStudentsAsync());
        }).WithName("GetStudent").WithOpenApi();

        app.MapGet("/student/{studentId}", async (int studentId) =>
        {
            try
            {
                Student student = await db.GetStudentByIdAsync(studentId);
                return Results.Ok(student);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetStudentByStudentId").WithOpenApi();

        app.MapGet("/student/classset/{classSetId}", async (int classSetId) =>
        {
            try
            {
                List<Student> students = await db.GetStudentsFromClassSetAsync(await db.GetClassSetByIdAsync(classSetId));
                return Results.Ok(students);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetStudentByClassSetId").WithOpenApi();

        app.MapGet("/teacher", async () =>
        {
            return Results.Ok(await db.GetTeachersAsync());
        }).WithName("GetTeacher").WithOpenApi();

        app.MapGet("/teacher/{teacherId}", async (int teacherId) =>
        {
            try
            {
                Teacher teacher = await db.GetTeacherByIdAsync(teacherId);
                return Results.Ok(teacher);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetTeacherByTeacherId").WithOpenApi();

        app.MapGet("/classset", async () =>
        {
            return Results.Ok(await db.GetClassSetsAsync());
        }).WithName("GetClassSet").WithOpenApi();

        app.MapGet("/classset/{classSetId}", async (int classSetId) =>
        {
            try
            {
                ClassSet classSet = await db.GetClassSetByIdAsync(classSetId);
                return Results.Ok(classSet);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetClassSetByClassSetId").WithOpenApi();

        app.MapGet("/classset/course/{courseId}", async (int courseId) =>
        {
            try
            {
                List<ClassSet> classSets = await db.GetClassSetsFromCoruseAsync(await db.GetCourseByIdAsync(courseId));
                return Results.Ok(classSets);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetClassSetByCourseId").WithOpenApi();

        app.MapGet("/classset/student/{studentId}", async (int studentId) =>
        {
            try
            {
                List<ClassSet> classSets = await db.GetClassSetsFromStudentAsync(await db.GetStudentByIdAsync(studentId));
                return Results.Ok(classSets);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetClassSetByStudentId").WithOpenApi();

        app.MapGet("/classset/teacher/{teacherId}", async (int teacherId) =>
        {
            try
            {
                List<ClassSet> classSets = await db.GetClassSetsFromTeacherAsync(await db.GetTeacherByIdAsync(teacherId));
                return Results.Ok(classSets);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Results.NotFound();
            }
        }).WithName("GetClassSetByTeacherId").WithOpenApi();

        app.Run();
    }
}