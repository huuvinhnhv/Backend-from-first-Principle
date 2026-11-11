// C# .NET 8: Cấu hình nén phản hồi (Response Compression)
builder.Services.AddResponseCompression(options => {
    options.EnableForHttps = true; // Bật nén qua HTTPS
});

var app = builder.Build();
app.UseResponseCompression(); // Tự động thương lượng Accept-Encoding (gzip, br) và nén payload

app.MapGet("/greeting", (HttpContext ctx) => {
    var lang = ctx.Request.Headers.AcceptLanguage.ToString().StartsWith("es") ? "es" : "en";
    var msg = lang == "es" ? "Hola" : "Hello";
    return Results.Ok(new { message = msg });
});
