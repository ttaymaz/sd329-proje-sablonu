namespace Altyapi;

// appsettings.json dosyasındaki "Depo" bölümü bu sınıfa bağlanır.
public sealed class DepoAyarlari
{
    // Kayıt dosyalarının yazılacağı klasör, çalışma klasörüne göre
    public string Klasor { get; set; } = "veri";
}
