// C# .NET 8 (Kestrel): Phục vụ TLS trực tiếp với cặp cert + private key
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.ListenAnyIP(443, listenOptions => {
        // Tải chứng chỉ X.509 từ server.crt và server.key
        listenOptions.UseHttps(X509Certificate2.CreateFromPemFile("server.crt", "server.key"));
    });
});

var app = builder.Build();
app.Run();
// Lưu ý production: thông thường bạn sẽ terminate TLS tại Reverse Proxy/LB và chạy HTTP phía sau.
