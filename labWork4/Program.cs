using labWork4.Core;
using labWork4.Models;

ContactRepository repository;
var builder = WebApplication.CreateBuilder(args);


while (true)
{
    string path;
    RepositoryType repositoryType;

    PrintRepositoryChoose();
    try
    {
        var ch = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Input the source file path, or press enter to use default path.");
        path = Console.ReadLine() ?? "";

        switch (ch)
        {
            case 1:
                repositoryType = RepositoryType.JSON;
                break;
            case 2:
                repositoryType = RepositoryType.XML;
                break;
            case 3:
                repositoryType = RepositoryType.DATABASE;
                break;
            case 4:
                return;
            default:
                throw new FormatException();
        }
    }
    catch
    {
        Console.WriteLine("Incorrect input. Please, try again.");
        continue;

    }

    repository = ContactRepository.CreateRepository(repositoryType, path);
    break;
}

void PrintRepositoryChoose()
{
    Console.WriteLine("Enter the number of contact repository type and press [Enter]");
    Console.WriteLine("1.JSON");
    Console.WriteLine("2.XML");
    Console.WriteLine("3.DataBase");
    Console.WriteLine("4.Exit");
}

// Add services to the container.;

builder.Services.AddControllers();
builder.Services.AddSingleton<ContactRepository>(repository);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
