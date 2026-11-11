// C# .NET 8 (Kestrel): Tinh chỉnh Keep-Alive timeout và chống tấn công Slowloris
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions => {
    // Thời gian duy trì kết nối keep-alive nhàn rỗi (idle)
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(60);
    // Giảm thiểu tấn công Slowloris (giới hạn thời gian nhận header và tốc độ tối thiểu)
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(2);
    serverOptions.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(2));
});

var app = builder.Build();
app.Run();
