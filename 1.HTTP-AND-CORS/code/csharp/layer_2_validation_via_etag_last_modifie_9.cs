// C# .NET 8: Thẩm định ETag và phản hồi 304 Not Modified
app.MapGet("/resource", (HttpContext ctx) => {
    byte[] body = LoadResource();
    var hash = SHA256.HashData(body);
    var etag = $"\"{Convert.ToHexString(hash)[..16]}\""; // Dấu vân tay của nội dung

    if (ctx.Request.Headers.IfNoneMatch == etag) {
        return Results.StatusCode(StatusCodes.Status304NotModified); // 304: Tiết kiệm toàn bộ payload
    }

    ctx.Response.Headers.ETag = etag;
    ctx.Response.Headers.CacheControl = "max-age=10";
    return Results.Bytes(body, "application/json");                  // 200 + body
});
