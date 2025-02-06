using MyBlog.Components;
using Data;
using Data.Models.Interfaces;
using MyBlog.Client.Pages;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>().Configure(options =>
{
    options.DataPath = @"Data\";
    options.BlogPostsFolder = "Blogposts";
    options.TagsFolder = "Tags";
    options.CategoriesFolder = "Categories";
    options.CommentsFolder = "Comments";
});

builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("BlogOuziConnection"));
});

builder.Services.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly)
    .AddAdditionalAssemblies(typeof(SharedComponents.Pages.Home).Assembly); ;

app.Run();
