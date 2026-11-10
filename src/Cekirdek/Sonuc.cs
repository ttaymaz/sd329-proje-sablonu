namespace Cekirdek;

// Başarısız olabilecek işlemlerin sonucu (9. hafta: sonuç tipi).
// Kullanımı:
//   Sonuc<Rezervasyon> s = servis.Olustur(...);
//   string mesaj = s switch
//   {
//       Basarili<Rezervasyon>(var r) => $"oluşturuldu: {r}",
//       Hatali<Rezervasyon>(var neden) => $"olmadı: {neden}",
//       _ => throw new InvalidOperationException(),
//   };
public abstract record Sonuc<T>;

public sealed record Basarili<T>(T Deger) : Sonuc<T>;

public sealed record Hatali<T>(string Neden) : Sonuc<T>;
