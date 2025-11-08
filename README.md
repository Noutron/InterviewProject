## Görevler

Aşağıdaki görevleri sırasıyla tamamlamanız beklenmektedir.

### 1. Exception Handling

Projede şu anda global bir exception handling mekanizması bulunmamaktadır. Aşağıdaki gereksinimleri karşılayan bir çözüm geliştirin:

- Tüm hataları yakalayan bir middleware yazın
- Hata detaylarını loglayın
- Client'a uygun HTTP status code ve hata mesajı dönün
- Production ortamında hassas bilgilerin dönmemesini sağlayın
- Validation hatalarını ayrı handle edin

### 2. Customer Authentication Middleware

Her API isteğinde aşağıdaki işlemleri yapan bir middleware geliştirin:

- Request header'dan customer bilgisini okuyun (örn: X-Customer-Id)
- Bu bilgiyle veritabanından ilgili customer'ı sorgulayın
- Customer bulunamazsa 401 Unauthorized dönün
- Customer bilgisini request pipeline boyunca kullanılabilir hale getirin
- Bu middleware'i sadece GET ve POST isteklerine uygulayın

### 3. POST /api/payments Endpoint Performans Sorunu

UI ekibi, ödeme oluşturma işleminde kullanıcı arayüzünün kitlendiğini bildirdi. Sorunları tespit edip çözün:

- Kodda hangi satırlar bu soruna neden oluyor?
- Async/await kullanımında yapılan hatalar neler?
- External servis çağrısı nasıl optimize edilebilir?
- UI'da kitlenmeyi önlemek için ne yapılmalı?

### 4. GET /api/orders Endpoint Performans Sorunu

Bu endpoint'in çok yavaş çalıştığı raporlanmıştır. Performans sorunlarını bulup düzeltin:

- N+1 query problemi var mı?
- Entity Framework sorguları optimize edilebilir mi?
- Gereksiz veri çekiliyor mu?
- AsNoTracking kullanılmalı mı?
- Include/ThenInclude kullanımı doğru mu?

### 5. TestService - Referans Tip ve Değer Tip

TestService içindeki metotları inceleyip aşağıdaki API çağrılarının döneceği değerleri tahmin edin:

Senaryo 1:
```
GET /api/test/amount?value=100
```
Bu endpoint ne döner? Neden?

Senaryo 2:
```
GET /api/test/order/1
```
Bu endpointte TotalAmount ve customerId değerlerinin güncellenmediği testpit edilmiştir. Sorun nedir ?





