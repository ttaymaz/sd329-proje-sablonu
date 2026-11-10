using System.Text.Encodings.Web;
using System.Text.Json;
using Cekirdek;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Altyapi;

// IDepo<T>'nin dosya sürümü: her tip kendi JSON dosyasında durur
// (Rezervasyon → veri/Rezervasyon.json). Kayıt tipleriniz record olursa
// System.Text.Json onları kurucularıyla okur.
public sealed class JsonDepo<T> : IDepo<T>
{
    private static readonly JsonSerializerOptions Secenekler = new()
    {
        WriteIndented = true,
        // Türkçe karakterler dosyada ı gibi değil, ı olarak dursun
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    private readonly string _klasor;
    private readonly string _dosya;
    private readonly ILogger<JsonDepo<T>> _gunluk;

    public JsonDepo(IOptions<DepoAyarlari> ayarlar, ILogger<JsonDepo<T>> gunluk)
    {
        ArgumentNullException.ThrowIfNull(ayarlar);
        _klasor = ayarlar.Value.Klasor;
        _dosya = Path.Combine(_klasor, $"{typeof(T).Name}.json");
        _gunluk = gunluk;
    }

    public async Task<IReadOnlyList<T>> OkuAsync(CancellationToken iptal = default)
    {
        if (!File.Exists(_dosya))
        {
            _gunluk.LogInformation("{Dosya} henüz yok; boş liste dönülüyor", _dosya);
            return [];
        }

        await using var akis = File.OpenRead(_dosya);
        var ogeler = await JsonSerializer.DeserializeAsync<List<T>>(akis, Secenekler, iptal);
        return ogeler ?? [];
    }

    public async Task YazAsync(IReadOnlyList<T> ogeler, CancellationToken iptal = default)
    {
        ArgumentNullException.ThrowIfNull(ogeler);
        Directory.CreateDirectory(_klasor);

        await using var akis = File.Create(_dosya);
        await JsonSerializer.SerializeAsync(akis, ogeler, Secenekler, iptal);
        _gunluk.LogInformation("{Sayi} kayıt {Dosya} dosyasına yazıldı", ogeler.Count, _dosya);
    }
}
