using System.IO.Compression;
using Aviv.Base.UI.Components;
using Aviv.Base.UI.Helper;
using Aviv.Base.UI.Services;
using Aviv.Base.UI.Services.Authentication;
using Aviv.Base.UI.Services.SalesTask;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Radzen;
using Syncfusion.Blazor;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure HTTP client for insecure environments (development only)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpClient("NoSSL")
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });
}
else
{
    builder.Services.AddHttpClient("SecureClient")
        .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15) // Connection pooling for better performance
        });
}

// Add services to the container.
builder.Services.AddSyncfusionBlazor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Authentication services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

// Register services with appropriate lifetimes
// Singletons - shared across all users of the application
builder.Services.AddSingleton<AppState>();

// Scoped - one instance per request/connection
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<MenuDataService>();
builder.Services.AddScoped<NavScrollService>();
builder.Services.AddSingleton<ThemePresetService>();
builder.Services.AddWMBOS();
builder.Services.AddPageBreadcrumbService();

// Radzen services
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationCustomService>();

builder.Services.AddScoped<CustomFormService>();
builder.Services.AddScoped<SalesTaskInfoService>();

// Add Memory Cache for performance improvement
builder.Services.AddMemoryCache();

// Add response compression for better performance
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/javascript", "text/css", "application/json", "image/svg+xml" });
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

// Configure HTTP context accessor for access to HttpContext when needed
builder.Services.AddHttpContextAccessor();

// Configure CORS if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        policy.WithOrigins("https://trusted-origin.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

WebApplication app = builder.Build();

// Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc5NjUwOUAzMjM5MmUzMDJlMzAzYjMyMzkzYlNoSVA2dzFrVFdMU2dIbHJhNmlvajYxT2VxZzByOFNSNjQrNXFFeHVDUGM9");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseResponseCompression();
}

// Use response compression
app.UseResponseCompression();

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Serve static files with cache headers
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static resources for a day
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
    }
});

// Configure routing
app.UseRouting();

// Add CORS
app.UseCors("AllowedOrigins");

// Authentication and authorization middleware - Order is important!
app.UseAuthentication();
app.UseAuthorization();

// CSRF protection
app.UseAntiforgery();

// Map static assets and Razor components
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Log application startup
app.Logger.LogInformation("Application starting up at {Time}", DateTime.UtcNow);

app.Run();