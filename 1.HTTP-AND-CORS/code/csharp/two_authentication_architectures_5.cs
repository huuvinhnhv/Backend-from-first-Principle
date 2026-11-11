// C# .NET 8:
// 1. Thiết lập session cookie bảo mật sau đăng nhập (stateful)
app.MapPost("/login", (HttpContext ctx) => {
    var sid = Guid.NewGuid().ToString("N");
    sessions[sid] = userId; // Session store phía server (Redis/Database)

    ctx.Response.Cookies.Append("session", sid, new CookieOptions {
        HttpOnly = true,                         // JS không thể đọc (chống XSS)
        Secure = true,                           // Chỉ truyền qua HTTPS
        SameSite = SameSiteMode.Strict,          // Phòng chống CSRF
        MaxAge = TimeSpan.FromHours(1),
        Path = "/"
    });
    return Results.Ok();
});

// 2. Endpoint yêu cầu xác thực Bearer Token JWT (stateless)
app.MapGet("/me", (ClaimsPrincipal user) => Results.Ok(new { ok = true }))
   .RequireAuthorization(); // Tự động kiểm tra Bearer JWT, trả về 401 nếu thiếu/hết hạn
