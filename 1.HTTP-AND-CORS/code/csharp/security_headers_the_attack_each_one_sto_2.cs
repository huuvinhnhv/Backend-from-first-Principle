// C# .NET 8: Custom Middleware thiết lập Security Headers
var app = WebApplication.Create(args);

app.Use(async (context, next) => {
    var headers = context.Response.Headers;
    headers.StrictTransportSecurity = "max-age=31536000; includeSubDomains";
    headers["Content-Security-Policy"] = "default-src 'self'";
    headers.XFrameOptions = "DENY";
    headers.XContentTypeOptions = "nosniff";

    await next(context); // Headers PHẢI được thiết lập trước khi response bắt đầu ghi body
});

app.Run();
