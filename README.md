## Görevler

Aşağıdaki görevleri sırasıyla tamamlamanız beklenmektedir.

### 1. Exception Handling

Projede şu anda global bir exception handling mekanizması bulunmamaktadır. Aşağıdaki gereksinimleri karşılayan bir çözüm geliştirin:

- Tüm hataları yakalayan bir middleware yazın

Her API isteğinde aşağıdaki işlemleri yapan bir middleware geliştirin:

- Request header'dan customer bilgisini okuyun (örn: X-Customer-Id)
- Bu bilgiyle veritabanından ilgili customer'ı sorgulayın
- Customer bulunamazsa 401 Unauthorized dönün
- Customer bilgisini request pipeline boyunca kullanılabilir hale getirin
- Bu middleware'i sadece GET ve POST isteklerine uygulayın

### 3. POST /api/payments Endpoint Performans Sorunu

UI ekibi, ödeme oluşturma işleminde kullanıcı arayüzünün kitlendiğini bildirdi. Sorunları tespit edip çözün:

- Kodda hangi satırlar bu soruna neden oluyor?
- External servis çağrısı nasıl optimize edilebilir?
- UI'da kitlenmeyi önlemek için ne yapılmalı?

### 4. GET /api/orders Endpoint Performans Sorunu

Bu endpoint'in çok yavaş çalıştığı raporlanmıştır. Performans sorunlarını bulup düzeltin:


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

### 6. Hassas Verilerin Güvenliği

- Post /api/payments endpoint'ine gönderilen cardNumber parametresi hassas bir veridir. Bu veriyi doğrudan tutmamalıyız.
- CardNumber masked halde tutulması sağlanmalıdır.
- İlk 6 hane ve son 4 hane açık olmalıdır.


### 7. İndirim Hesaplama Hatası

`DiscountService.CalculateLoyaltyDiscount` metodu için unit testler yazın

- VIP müşterilere her zaman %10 indirim uygulanmalıdır.
- Diğer indirim kademelerindeki indirimlerin üst üste uygulanması engellenmelidir.
Örneğin ; yüzde 15 indirim kazanmış bir müşteri bir sonraki siparişinde aynı indirimi kazanamaz.
- Tüm indirimleri tamamlayan müşterinin, indirimleri baştan tekrar kazanmaya devam edebilmesi sağlanmalıdır.

### 8. Unit Test Yazımı

`DiscountService.CalculateLoyaltyDiscount` metodu için unit testler yazın:

- Metodun tüm iş kurallarını kapsayan test senaryoları oluşturun
- Edge case'leri (sınır değerler, negatif inputlar) test edin
- Test metodlarınızı anlamlı şekilde isimlendirin






