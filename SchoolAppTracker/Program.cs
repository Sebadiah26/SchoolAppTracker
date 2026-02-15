using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using SchoolAppTracker.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSchoolAppTrackerCoreClasses(builder.Configuration);

// TODO: Re-enable Google auth for production
// var googleConfig = builder.Configuration.GetSection("Authentication:Google");
// var allowedDomain = googleConfig["AllowedDomain"];
//
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
// })
// .AddCookie()
// .AddGoogle(options =>
// {
//     options.ClientId = googleConfig["ClientId"] ?? "";
//     options.ClientSecret = googleConfig["ClientSecret"] ?? "";
//     options.Events.OnCreatingTicket = context =>
//     {
//         var hd = context.User.GetProperty("hd").GetString();
//         if (!string.IsNullOrEmpty(allowedDomain) && hd != allowedDomain)
//         {
//             context.Fail($"Only {allowedDomain} accounts are allowed.");
//         }
//         return Task.CompletedTask;
//     };
// });
//
// builder.Services.AddAuthorization(options =>
// {
//     options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
//         .RequireAuthenticatedUser()
//         .Build();
// });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
