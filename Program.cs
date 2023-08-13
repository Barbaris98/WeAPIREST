// начальные данные
List<Person> users = new List<Person>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", ()=> users);
app.MapGet("/api/users/{id}", (string id) =>
{
    // получаем пользовател€ по id
    Person? user = users.FirstOrDefault(x => x.Id == id);
    // если не найден то отпр статусный код об ошибке
    if (user == null)
    {
        return Results.NotFound(new { message = "ѕользователь не найден" });
    }
    // если найден, то отправл€ем его
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    // получаем пользовател€ по id
    Person? user = users.FirstOrDefault(x => x.Id == id);
    if (user == null)
    {
        return Results.NotFound(new { message = "ѕользватель не найден"});
    }
    // если найден, то удал€ем его
    users.Remove(user);
    return Results.Json(user);
});





app.Run();

public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
