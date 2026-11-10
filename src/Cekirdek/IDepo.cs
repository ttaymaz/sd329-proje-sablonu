namespace Cekirdek;

// Bir tipteki kayıtların tamamını okuyup yazan jenerik depo (3. hafta).
//
// Servisleriniz bu ARAYÜZE bağlı olur, dosyaya değil. Uygulamada arkasında
// JsonDepo<T> (Altyapi) çalışır; testlerde bunun yerine bellekte duran
// sahte bir depo verebilirsiniz. Bağımlılığın dışarıdan verilmesi
// (bağımlılık enjeksiyonu) bunu mümkün kılar.
public interface IDepo<T>
{
    Task<IReadOnlyList<T>> OkuAsync(CancellationToken iptal = default);

    Task YazAsync(IReadOnlyList<T> ogeler, CancellationToken iptal = default);
}
