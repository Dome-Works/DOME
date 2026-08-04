using HomelabDocs.Shared.Api;
using HomelabDocs.Web.Components;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Hardcoded for local development; replace with Compose configuration later.
const string apiBaseUrl = "http://localhost:5100";

builder.Services
    .AddRefitClient<IHomelabDocsApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(apiBaseUrl);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
