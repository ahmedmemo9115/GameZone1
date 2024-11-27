using Microsoft.AspNetCore.Http.Features;


try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseEnvironment("Development");

    var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                        ?? throw new InvalidOperationException(message: "No Connection String Available");

    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(ConnectionString));

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.Configure<FormOptions>(options =>
    {
        options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
    });


    builder.Services.AddScoped<ICategoriesService, CategoriesService>();

    builder.Services.AddScoped<IDevicesService, DevicesService>();

    builder.Services.AddScoped<IGamesService, GamesService>();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseDeveloperExceptionPage();
    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{
    // Log the exception
    Console.WriteLine($"Unhandled Exception: {ex.Message}");
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
    throw;
}