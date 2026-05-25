var builder = WebApplication.CreateBuilder(args);

// Configuração de Serviços
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do App
app.Run();
