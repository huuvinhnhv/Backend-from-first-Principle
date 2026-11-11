// C# .NET 8: Xử lý Idempotency-Key chống thanh toán trùng lặp
var seen = new ConcurrentDictionary<string, object>(); // Trong production hãy dùng IDistributedCache (Redis) kèm TTL

app.MapPost("/payments", (HttpContext ctx, PaymentRequest req) => {
    if (!ctx.Request.Headers.TryGetValue("Idempotency-Key", out var key) || string.IsNullOrEmpty(key)) {
        return Results.BadRequest("missing Idempotency-Key");
    }

    if (seen.TryGetValue(key!, out var cachedResult)) {
        return Results.Ok(cachedResult); // Replay: trả về kết quả đã lưu trước đó
    }

    var result = Charge(req);            // Xử lý nghiệp vụ thực tế (non-idempotent)
    seen.TryAdd(key!, result);           // Ghi nhớ kết quả TRƯỚC KHI phản hồi
    return Results.Created($"/payments/{key}", result);
});
