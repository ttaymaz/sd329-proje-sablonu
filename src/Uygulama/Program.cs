using Altyapi;
using Cekirdek;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Uygulama;

// Generic Host: yapılandırma (appsettings.json), günlükleme ve DI kabı
// tek yerde kurulur. ContentRootPath, appsettings.json'ın programın
// yanından okunmasını sağlar; komutu hangi klasörden çalıştırırsanız
// çalıştırın ayarlar bulunur.
var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
});

// Kap kurulurken bağımlılık grafını doğrula: kayıtsız bağımlılık ya da
// tekil bir servise verilen kapsamlı servis, program çalışırken değil
// açılırken hata versin. (Host bunu kendiliğinden yalnızca Development
// ortamında yapar.)
builder.ConfigureContainer(new DefaultServiceProviderFactory(new ServiceProviderOptions
{
    ValidateScopes = true,
    ValidateOnBuild = true,
}));

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(o => o.SingleLine = true);

builder.Services.Configure<DepoAyarlari>(builder.Configuration.GetSection("Depo"));
builder.Services.AddSingleton(typeof(IDepo<>), typeof(JsonDepo<>));

// TODO: Kendi servislerinizi burada kaydedin. Örnek:
// builder.Services.AddSingleton<RezervasyonServisi>();

builder.Services.AddSingleton<Menu>();

using var host = builder.Build();

// Ctrl+C programı hemen öldürmesin; iptal isteği olarak menüye iletilsin
using var iptal = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    iptal.Cancel();
};

await host.Services.GetRequiredService<Menu>().CalistirAsync(iptal.Token);
