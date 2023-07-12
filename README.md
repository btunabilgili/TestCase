# TEST CASE WEB API

Proje docker ortamında 8088 portunda çalışmaktadır. Kullanım kolaylığı açısından swagger production ortamında da çalışacak şekilde configure edilmiştir. http://localhost:8088/swagger/index.html bu url üzerinden postman ya da başka bir toola gerek kalmadan API ile etkileşime geçilebilir.


## API Kullanımı

Öncelikle authenticate olabilmek için "Company" oluşturulmalıdır.

#### Creates Company

```http
  POST /api/company
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `password` | `string` | **Gerekli**. Şifre.|
| `companyName` | `string` | **Gerekli**. Şirket adı.|
| `address` | `string` | **Gerekli**. Şirket adresi.|
| `phone` | `string` | **Gerekli**. Şirket telefonu. Login olurken kullanılacak kullanıcı adıdır aynı zamanda ve 05xx-xxx-xx-xx formatında olmalıdır.|
| `email` | `string` | **Gerekli**. Şirket emailı. test@test.com formatında olmalı.|

Sonrasında oluşturduğumuz şirkete ait telefon numarası ve şifre ile authenticate olabiliriz.

#### JWT Auth

```http
  POST /api/Auth/token
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `phone` | `string` | **Gerekli**. Şirket telefonu.|
| `password` | `string` | **Gerekli**. Şifre.|

  ![Uygulama Ekran Görüntüsü](https://i.ibb.co/G3WxXp9/auth.png)

Görseldeki gibi bize kullanabileceğimiz bir token dönecektir. Swagger arayüzünden "Authorize" butonuna tıklayarak API'ye authenticate olabiliriz ve diğer metodları kullanabiliriz.
