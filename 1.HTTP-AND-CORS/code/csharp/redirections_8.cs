// C# .NET 8: Chuyển hướng giữ nguyên method (308) hoặc ép sang GET (303)
// 1. Chuyển hướng vĩnh viễn bắt buộc giữ nguyên method/body -> 308 Permanent Redirect
app.MapMethods("/user/{id:int}", ["GET", "POST"], (int id) =>
    Results.Redirect($"/person/{id}", permanent: true, preserveMethod: true));

// 2. Post/Redirect/Get -> 303 See Other để trình duyệt refresh không bị re-POST form
app.MapPost("/submit", (SubmitForm form) => {
    var id = Save(form);
    return Results.SeeOther($"/results/{id}"); // 303 (ép chuyển sang GET)
});
