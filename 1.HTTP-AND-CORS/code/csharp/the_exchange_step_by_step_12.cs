// C# .NET 8: Phục vụ Range Requests (mã 206) với Results.File
app.MapGet("/big.zip", () => {
    // enableRangeProcessing: true kích hoạt hỗ trợ dải byte (Range) và trả về 206 Partial Content
    return Results.File(
        path: Path.Combine(Directory.GetCurrentDirectory(), "big.zip"),
        contentType: "application/zip",
        enableRangeProcessing: true
    );
});
