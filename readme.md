# Self-Waiter 🌟🎉🌟

## Proje Hakkında 🚀🌟🚀

**Self-Waiter**, kafe ve restoranlarda müşterilerin sipariş verme süreçlerini hızlandırmayı ve daha verimli hale getirmeyi amaçlayan bir dijital sipariş yönetim platformudur. Bu sistem sayesinde müşteriler, masalarından kalkmadan akıllı telefonları veya tabletleri aracılığıyla doğrudan sipariş verebilirler. Sipariş hazır olduğunda müşterilere bildirim gönderilir ve siparişi kendileri teslim alabilir ya da garson tarafından masalarına servis edilebilir. Bu sayede hem müşteri memnuniyeti artar hem de işletmelerin operasyonel verimliliği gelişir. 🚀📈💡

## Özellikler 🔧😃📚

- **Kolay Sipariş Verme:** Müşteriler QR kodunu okutarak ya da mobil uygulamaya giriş yaparak kolayca sipariş verebilir.
- **Zaman Tasarrufu:** Müşteriler sipariş sırasında beklemeden doğrudan masalarından işlem yapabilir.
- **Bildirim Sistemi:** Sipariş hazır olduğunda müşteriye anlık bildirim gönderilir.
- **Masaya Servis:** Müşteri dilerse siparişi kendisi alabilir veya garson tarafından masasına servis edilebilir.
- **Sipariş Takibi:** Müşteriler siparişin hangi aşamada olduğunu uygulama üzerinden görebilir.
- **Gelişmiş Raporlama:** İşletme sahipleri için detaylı sipariş analizleri ve müşteri tercihleri hakkında veriler sunar.
- **Çoklu Dil Desteği:** Uygulama farklı dillerde kullanılabilir. 🌐🔍🎨

## Kullanılan Teknolojiler 💻🛠️🛡️

Bu proje, modern ve güçlü teknolojiler kullanılarak geliştirilmiştir. Uygulama, hem frontend hem de backend katmanlarında esnek ve ölçeklenebilir bir mimariye sahiptir. 🛡️📚📈

### Backend 🔎💡🛠️

- **.NET 9** – Performanslı ve güvenli bir backend geliştirme ortamı sağlar.
- **MongoDB** – Esnek ve doküman tabanlı veritabanı yapısı için tercih edilmiştir.
- **MSSQL** – Geleneksel ilişkisel veritabanı ihtiyaçları için kullanılır.
- **Redis** – Yüksek performanslı cache yönetimi için kullanılır.
- **ElasticSearch** – Hızlı ve güçlü arama motoru sağlar.
- **RabbitMQ** – Asenkron mesajlaşma ve iş kuyruğu yönetimi için tercih edilmiştir. 🧱📡🛠️

### Frontend 👨‍💻🌐🔧

- **Vue 2** – Kullanıcı dostu ve dinamik bir frontend arayüzü sağlar.

### Dağıtım ve Konteynerizasyon 🚧🛳️🚀

- **Docker** – Uygulamaların izole bir şekilde çalıştırılmasını sağlar.
- **Kubernetes** – Uygulamaların ölçeklenebilir bir şekilde yönetimini sağlar.

## Kurulum 🛠️🔧🚀

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/tahapek5454/Self-Waiter.git
   ```
2. Backend projesini başlatın:
   ```bash
   cd server
   dotnet run
   ```
3. Frontend projesini başlatın:
   ```bash
   cd client
   npm install
   npm run serve
   ```
4. MongoDB, Redis, RabbitMQ gibi servisleri başlatmak için Kubernetes deployment dosyalarını kullanın ya da kendi ortamlarınızı entegre edin:
   ````bash
   kubectl apply -f k8s/
   ``` 🌟🛠️🔄
   ````

## Kullanım 👨‍🍳🍽️🔄

1. Kafe/restoran masasında bulunan QR kodu mobil cihazınızdan taratın.
2. Menü otomatik olarak mobil cihazınızda açılacaktır.
3. Ürünleri seçin ve siparişinizi onaylayın.
4. Siparişiniz hazır olduğunda mobil cihazınıza bildirim gelecektir.
5. Siparişi kendiniz alabilir ya da masaya servis isteyebilirsiniz. 🚀📢📡

## Ek Dökümanlar 📸📹📊

- **Mimari Diyagram:** [Arch.png](./docs/Arch.png)
- **ER Diyagramı:** [ER\_Diagram.png]()
- **Kullanım Videosu:** [Kullanım Videosu](./docs/usage-video.mp4) 🎥📄📊

## Katkıda Bulunma 🤝💪👨‍🍳

Projeye katkıda bulunmak için lütfen bir "pull request" gönderin. Tüm katkılar değerlidir! 🌟💪👨‍🎓

## Lisans 📜🔒📂

Bu proje MIT lisansı altında sunulmaktadır. 🌐🛠️🔧

---

Bu proje, geleneksel restoran ve kafe deneyimini dijital dünyaya taşıyarak hem işletmelerin hem de müşterilerin ihtiyaçlarını karşılamak için geliştirilmiştir. Modern teknolojilerle güçlendirilmiş yapısıyla hızlı, güvenilir ve kullanıcı dostu bir deneyim sunar. 🌟🚀🎉

