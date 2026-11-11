// C# .NET 8 (Minimal APIs): Ánh xạ chính xác các mã trạng thái
app.MapGet("/users/{id:int}", (int id, HttpContext ctx) => {
    if (!ctx.Request.Headers.ContainsKey("Authorization")) {
        return Results.Unauthorized();                    // 401: Bạn là ai? (chưa xác thực)
    }

    var user = db.Find(id);
    if (user is null) {
        return Results.NotFound(new { error = "no such user" }); // 404: Không tìm thấy user
    }

    if (!user.IsVisibleTo(ctx.User)) {
        return Results.Forbid();                          // 403: Đã biết bạn là ai, nhưng cấm truy cập
    }

    return Results.Ok(user);                              // 200: Thành công
});
