// C# .NET 8 (Minimal APIs): Gắn kết HTTP methods với handlers
var app = WebApplication.Create(args);

// Đọc an toàn, có thể cache (safe, cacheable). Tự động hỗ trợ HEAD cho route GET
app.MapGet("/notes", (bool? done, int limit = 20) => ListNotes(done, limit));

app.MapGet("/notes/{id:int}", (int id) => {
    var note = db.Find(id);
    return note is not null ? Results.Ok(note) : Results.NotFound(); // 200 hoặc 404
});

app.MapPost("/notes", (Note note) => Results.Created($"/notes/{note.Id}", note));  // Tạo mới (201)
app.MapPut("/notes/{id:int}", (int id, Note note) => Results.Ok(PutNote(id, note))); // Thay thế toàn bộ (idempotent)
app.MapPatch("/notes/{id:int}", (int id, JsonPatch patch) => Results.Ok());          // Cập nhật một phần
app.MapDelete("/notes/{id:int}", (int id) => Results.NoContent());                    // Xóa (204, idempotent)

app.Run();
