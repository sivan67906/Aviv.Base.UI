using Aviv.Base.UI.Components;
using Aviv.Base.UI.Helper;
using Aviv.Base.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Radzen;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// No SSL
builder.Services.AddHttpClient("NoSSL")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });


builder.Services.AddScoped<StateService>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<MenuDataService>();
builder.Services.AddScoped<NavScrollService>();
builder.Services.AddWMBOS();
builder.Services.AddSession();
builder.Services.AddScoped<SessionService>();
builder.Services.AddPageBreadcrumbService();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationCustomService>();


// Add session services
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
        options.Cookie.Name = "YourAppCookieName";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

WebApplication app = builder.Build();

// Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX1cdXRVQmZfWUF/V0Q=");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
