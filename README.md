
# Rise Tech Microservice Message App


Bu proje Rise Technology, consulting & academy için geliştirilmiştir. Projenin amacı, microservice ile çalışarak asenkron yapıda mesaj kuyruk sistemi ile rapor talep edebilmektir.

## Projeyi çalıştırmak için

Projeyi klonlayın

```bash
  git clone https://github.com/ahmetkesc/rise-tech-case
```

Proje için veritabanı tanımlaması yapın ya da varolan veritabanı ismini kullanını

```bash
  rise_tech_db
```

RabbitMQ

```bash
  RabbitMQ localhost ile çalıştırılmıştır. Aynı yolu izleyebilirsiniz ya da 
  RabbitMQ için cloud sistem kullanabilirsiniz.
  https://www.cloudamqp.com/
  Gerekli parametreleri projeden değiştirebilirsiniz.
```
## Kullanılan Teknolojiler

**Paketler:** Autofac, Ocelot, Npgsql, Entityframework, RabbitMQ.Client, xunit


  
## API Kullanımı
### Contact
#### Tüm rehberi getir
##### Default Base URL = http://localhost:5122 
```http
  GET /contact/getall
```
#### Rehber kişi kaydı getir

```http
  GET /contact/getbylocation/{contactId}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `contactId`      | `Guid` | **Gerekli**. Çağrılacak öğenin primary key değeri |

#### Rehbere kişi ekle

```http
  POST /contact/add/
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `contact`      | `Contact` | **Gerekli**. Eklenecek kişi bilgisi |

#### Rehberdeki kişiye iletişim bilgisi ekle

```http
  POST /contact/addlocation/{contactId}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `contactId`      | `Contact` | **Gerekli**. İletişim bilgisinin ekleneceği kişinin primary key değeri |
| `location`      | `Location` | **Gerekli**. İletişim bilgisi |

#### Rehberdeki kişiye iletişim bilgisi ekle

```http
  DELETE /contact/deletelocation?contactId={contactId}&locationId={locationId}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `contactId`      | `contactId` | **Gerekli**. İletişim bilgisinin silineceği kişinin primary key değeri |
| `locationId`      | `locationId` | **Gerekli**. Silinecek iletişim bilgisinin primary key değeri |


### Report
#### Tüm raporları getir
##### Default Base URL = http://localhost:5122 
```http
  GET /report/getall
```
#### Rapor isteği oluşturur

```http
  GET /report/requestreport/
```
#### Oluşturulan raporu getirir

```http
  POST /report/getreport/{id}
```
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Gerekli**. Raporun primary key değeri |

## Yazarlar ve Teşekkür

- [@ahmetkesc](https://github.com/ahmetkesc) tasarım ve geliştirme için.

  
