var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();
app.MapGet("/", () => "работает");
app.MapGet("/hello", () => "Hello World!");

app.MapGet("/foo", () => "foo");




app.Run();
