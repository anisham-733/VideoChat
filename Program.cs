using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using VideoChat.Hubs;
using VideoChat.Options;
using VideoChat.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("TwilioSettings"));



builder.Services.AddTransient<IVideoService, VideoService>();
//builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

    endpoints.MapHub<NotificationHub>("/notificationHub");
});


app.MapFallbackToFile("index.html");

app.Run();
