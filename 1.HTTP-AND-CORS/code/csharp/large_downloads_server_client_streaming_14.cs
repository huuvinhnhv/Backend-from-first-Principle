// C# .NET 8:
// 1. Tiếp nhận file tải lên dạng multipart
app.MapPost("/upload", async (IFormFile file) => {
    // IFormFile tự động buffer dữ liệu vào RAM hoặc file tạm ổ đĩa
    var tempPath = Path.GetTempFileName();
    using var stream = File.Create(tempPath);
    await file.CopyToAsync(stream);
    return Results.Ok(new { received = file.FileName, size = file.Length });
});

// 2. Stream response thành từng chunk (Server-Sent Events)
app.MapGet("/stream", async (HttpContext ctx) => {
    ctx.Response.Headers.ContentType = "text/event-stream";
    ctx.Response.Headers.CacheControl = "no-cache";

    for (int i = 0; i < 5; i++) {
        await ctx.Response.WriteAsync($"data: chunk {i}\n\n");
        await ctx.Response.Body.FlushAsync(); // Đẩy ngay chunk về client mà không chờ đệm
        await Task.Delay(1000);
    }
});
