using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Application.Map;
using DiamondAssessmentSystem.Application.Services;
using DiamondAssessmentSystem.Hubs;
using DiamondAssessmentSystem.Infrastructure.Auth;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using DiamondAssessmentSystem.Infrastructure.Repository;
using DiamondAssessmentSystem.Infrastructure.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==================== AutoMapper ====================
builder.Services.AddAutoMapper(typeof(MapProfile));

// ==================== Controllers ====================
builder.Services.AddControllers();

// ==================== DbContext ====================
builder.Services.AddDbContext<DiamondAssessmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==================== HttpContextAccessor ====================
builder.Services.AddHttpContextAccessor();

// ==================== Identity ====================
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DiamondAssessmentDbContext>()
    .AddDefaultTokenProviders();

// Optional cookie paths (not required for JWT, but for future admin UI maybe)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

// ==================== Application Services ====================
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IServicePriceService, ServicePriceService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICerterficateService, CertificateService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();

// ==================== Repositories ====================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IServicePriceRepository, ServicePriceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IChatLogRepository, ChatLogRepository>();

// ==================== SignalR ====================
builder.Services.AddSignalR();

// ==================== JWT Authentication ====================
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    // 👇 VERY IMPORTANT: Allow reading JWT from query string for SignalR
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub/chat"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// ==================== Authorization ====================
builder.Services.AddAuthorization();

// ==================== CORS ====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                // Allow any localhost (HTTP/HTTPS) during development
                if (origin != null && (origin.StartsWith("http://localhost") || origin.StartsWith("https://localhost")))
                    return true;
                return false;
            })
                          .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ==================== Swagger ====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==================== Database seeding ====================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DbInitializer.SeedDefaultAdminAsync(services);
    await RoleSeeder.SeedRolesAsync(roleManager, logger);
}

// ==================== Middleware ====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// OPTIONAL
// Only redirect HTTPS if not localhost
if (!app.Environment.IsDevelopment())
{
app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hub/chat").RequireCors("AllowSpecificOrigin");

app.Run();
