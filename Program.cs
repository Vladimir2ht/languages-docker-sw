var builder = WebApplication.CreateBuilder(args);

string CorsPolicy = "local";

builder.Services.AddCors(options =>
{
	options.AddPolicy(
			name: CorsPolicy,
			policy =>
				{
					policy.WithOrigins("http://localhost:3000", "http://[::1]:3000");
				}
		);
});

// builder.Services.AddMemoryCache();
builder.Services.AddRazorPages();

var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
app.UseExceptionHandler("/Error");
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(CorsPolicy);

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Main}/{action=Index}");
app.MapControllerRoute("get", "get", new { controller = "Main", action = "Get" });
app.MapControllerRoute(name: "NotFound", "{*url}", new { controller = "Main", action = "Index" });

app.MapRazorPages();

app.Run();