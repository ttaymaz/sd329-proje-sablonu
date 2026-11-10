using Altyapi;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Testler;

// Dosyaya yazan bir sınıfı sınamak: her test kendi geçici klasörünü kullanır
// ve iş bitince siler. Gerçek "veri" klasörünüze dokunulmaz.
public sealed class JsonDepoTestleri : IDisposable
{
    private readonly string _klasor =
        Path.Combine(Path.GetTempPath(), "sd329-test-" + Guid.NewGuid().ToString("N"));

    private JsonDepo<OrnekKayit> DepoKur()
        => new(Options.Create(new DepoAyarlari { Klasor = _klasor }),
               NullLogger<JsonDepo<OrnekKayit>>.Instance);

    [Fact]
    public async Task Dosya_yokken_bos_liste_doner()
    {
        var depo = DepoKur();

        var kayitlar = await depo.OkuAsync();

        Assert.Empty(kayitlar);
    }

    [Fact]
    public async Task Yazilan_kayitlar_ayni_sirayla_geri_okunur()
    {
        var depo = DepoKur();
        OrnekKayit[] yazilan = [new("Çalıkuşu", 1), new("Huzur", 2)];

        await depo.YazAsync(yazilan);
        var okunan = await depo.OkuAsync();

        Assert.Equal(yazilan, okunan);
    }

    [Fact]
    public async Task Turkce_karakterler_dosyada_oldugu_gibi_durur()
    {
        var depo = DepoKur();

        await depo.YazAsync([new OrnekKayit("Işık ğüşöç", 1)]);
        string icerik = await File.ReadAllTextAsync(Path.Combine(_klasor, "OrnekKayit.json"));

        Assert.Contains("Işık ğüşöç", icerik, StringComparison.Ordinal);
    }

    public void Dispose()
    {
        if (Directory.Exists(_klasor))
            Directory.Delete(_klasor, recursive: true);
    }

    public sealed record OrnekKayit(string Ad, int Sayi);
}
