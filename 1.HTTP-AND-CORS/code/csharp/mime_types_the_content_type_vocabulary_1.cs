// C# .NET 8 (ASP.NET Core Minimal APIs)
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/v1/notes", async (HttpContext ctx) => {
    // 1. Đọc và giải mã JSON từ request body
    var note = await ctx.Request.ReadFromJsonAsync<Note>();
    if (note is null) {
        return Results.BadRequest("invalid JSON");
    }

    // 2. Thiết lập Header ĐẦU TIÊN
    ctx.Response.Headers.ContentType = "application/json";
    // 3. Ghi status code
    ctx.Response.StatusCode = StatusCodes.Status201Created;
    // 4. Ghi body CUỐI CÙNG (tuân thủ bất biến: Header -> Status -> Body)
    note = note with { Id = 42 };
    await ctx.Response.WriteAsJsonAsync(note);
    return Results.Empty;
});

app.Run();

record Note(int Id, string Title, bool Done);
