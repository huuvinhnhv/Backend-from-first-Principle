// C# .NET 8: Cấu hình CORS và xử lý Preflight (OPTIONS)
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.WithOrigins("https://example.com") // Origin chính xác khi bật Credentials
              .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
              .WithHeaders("Content-Type", "Authorization")
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromSeconds(86400)); // Cache quyền preflight trong 24h
    });
});

var app = builder.Build();
app.UseCors(); // Middleware tự động xử lý request OPTIONS và các header CORS
