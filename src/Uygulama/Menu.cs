using Microsoft.Extensions.Logging;

namespace Uygulama;

// Konsol menüsü. Yalnızca kullanıcıyla konuşur: girdiyi okur, servisi
// çağırır, sonucu yazar. İş kuralı BURADA DEĞİL, Cekirdek'teki servislerde
// durur; böylece kurallar konsol olmadan test edilebilir.
//
// TODO: Kendi servislerinizi kurucuya parametre olarak ekleyin; DI kabı
// onları kendisi verir. Örnek:
//   public sealed class Menu(RezervasyonServisi rezervasyonlar, ILogger<Menu> gunluk)
public sealed class Menu(ILogger<Menu> gunluk)
{
    public async Task CalistirAsync(CancellationToken iptal)
    {
        gunluk.LogInformation("Uygulama başladı");

        while (!iptal.IsCancellationRequested)
        {
            Console.WriteLine();
            Console.WriteLine("1) (TODO: ilk menü maddeniz)");
            Console.WriteLine("0) Çıkış");
            Console.Write("Seçiminiz: ");

            string? secim = Console.ReadLine();
            if (secim is null or "0")   // null: girdi akışı kapandı
                break;

            switch (secim.Trim())
            {
                // TODO: case "1": await rezervasyonlar.ListeleAsync(iptal); ...
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }

            // Servis çağrılarınız await ile yapılacağı için metot async.
            // Kendi maddelerinizi ekleyince bu satırı silin.
            await Task.CompletedTask;
        }

        gunluk.LogInformation("Uygulama kapanıyor");
    }
}
