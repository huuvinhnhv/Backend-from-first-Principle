// C# .NET 8: Khóa lạc quan với If-Match chống xung đột ghi đè
app.MapPut("/doc/{id:int}", async (int id, HttpContext ctx) => {
    var doc = db.Get(id);
    var ifMatch = ctx.Request.Headers.IfMatch.ToString(); // ETag mà client nhìn thấy gần nhất

    if (string.IsNullOrEmpty(ifMatch)) {
        return Results.BadRequest("If-Match required");
    }

    if (ifMatch != doc.ETag) {
        return Results.StatusCode(StatusCodes.Status412PreconditionFailed); // 412: Đã có ai đó sửa trước
    }

    await doc.ApplyAsync(ctx.Request.Body);
    doc.ETag = NewETag();                                 // Tăng phiên bản (bump version)
    db.Save(doc);

    ctx.Response.Headers.ETag = doc.ETag;
    return Results.Ok();
});
