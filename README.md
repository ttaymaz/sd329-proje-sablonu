<!--
  BU DOSYA BİR TASLAKTIR. Köşeli parantez < > içindeki her yeri kendi
  projenize göre doldurun, bu yorum bloğunu silin.
  Rozetteki <kullanici> ve <repo> kısımlarını kendi GitHub adresinizle
  değiştirin; rozet testlerinizin son durumunu gösterir.
-->

# <Proje adı>

![Testler](https://github.com/<kullanici>/<repo>/actions/workflows/test.yml/badge.svg)

<İki üç cümle: bu uygulama ne yapar, kimin işine yarar?>

**Alan:** <seçtiğiniz alan> · **Geliştiren:** <adınız ya da GitHub kullanıcı adınız>

İleri Programlama I (C# · .NET 10) dersinin dönem projesi olarak geliştirildi.

## Çalıştırma

.NET 10 SDK gerekir (`dotnet --version`).

```
dotnet run --project src/Uygulama
```

Testler:

```
dotnet test
```

## Örnek kullanım

<Uygulamanın bir çalıştırmasının konsol çıktısını buraya yapıştırın.>

```
<konsol çıktısı>
```

## Mimari

| Proje | Sorumluluk |
| ----- | ---------- |
| `src/Cekirdek` | <Kayıtlarınız, servisleriniz, olaylarınız> |
| `src/Altyapi` | JSON dosya deposu <ve eklediğiniz başka şeyler> |
| `src/Uygulama` | Konsol menüsü, DI kayıtları, `appsettings.json` |
| `tests/Testler` | xUnit testleri |

<Bir iki paragraf: hangi servis neyi yapıyor, başarısızlıkları nasıl
ele aldınız (istisna, Try kalıbı, Sonuc<T>), neden?>

## Yapay zekâ kullanımı

Bu projede yapay zekâ araçlarını nasıl kullandığım [`YAPAY-ZEKA.md`](YAPAY-ZEKA.md) dosyasında.
