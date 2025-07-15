using FileUploadReader.DataHelper;
using FileUploadReader.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddSingleton(sp =>
    builder.Configuration.GetSection("AzureCognitive").Get<AzureCognitiveSettings>());


var settings = builder.Configuration.GetSection("AzureCognitive").Get<AzureCognitiveSettings>();
DataHelper.Initialize(settings);

var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
