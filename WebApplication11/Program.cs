using Microsoft.EntityFrameworkCore;
using System;
using WebApplication11;
using WebApplication11.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container
builder.Services.Register(config);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();


app.Run();