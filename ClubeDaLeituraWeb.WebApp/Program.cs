var builder = WebApplication.CreateBuilder(args);

// Configuração de Serviços
builder.Services.AddControllersWithViews().AddRazorOptions(options =>
{
    // Resetar a configuração padrão do MVC
    options.ViewLocationFormats.Clear();

    // Views dos módulos: /ModuloCaixa/Apresentacao/Views/Listar.cshtml
    options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

    // Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
    options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
});

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do App
app.Run();
