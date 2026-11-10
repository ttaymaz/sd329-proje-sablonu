using Cekirdek;

namespace Testler;

// Şablonla gelen örnek testler. Kendi testlerinizi bu klasöre, sınadığınız
// sınıfın adıyla ayrı dosyalara yazın (ör. RezervasyonServisiTestleri.cs).
public class SonucTestleri
{
    [Fact]
    public void Ayni_degerli_iki_basarili_sonuc_esittir()
    {
        Sonuc<int> a = new Basarili<int>(5);
        Sonuc<int> b = new Basarili<int>(5);

        Assert.Equal(a, b);   // record: değer eşitliği
    }

    [Fact]
    public void Hatali_sonuc_desen_eslemeyle_okunur()
    {
        Sonuc<int> sonuc = new Hatali<int>("stok yetersiz");

        string mesaj = sonuc switch
        {
            Basarili<int>(var deger) => $"tamam: {deger}",
            Hatali<int>(var neden) => $"olmadı: {neden}",
            _ => "bilinmiyor",
        };

        Assert.Equal("olmadı: stok yetersiz", mesaj);
    }
}
