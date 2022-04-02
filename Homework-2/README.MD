# HiraBalci-Homework2
* Middleware yazılacak. İçeriği App version control (bu middleware option parametresi alacak parametre olarakta appsetting.json gelen app-version section değeri alacak)

* request login ve register ise bir sonraki pipeline'a geçsin (bu requestler dahil değildir altaki işlemler kontrol edilmeyecek)

* request header da app-version key olacak, request header da gönderdiğim app-version değeri appsetting.json tuttuğum app-version değerinden büyükse response 401 status kod olacak

* Swagger headerına app-version default key eklenecek. (IOperationFilter interface kullanılarak)

* Swagger 4 endpoint oluşturulacak istediğiniz gibi olabilir. Sadece bir tanesi gösterilmeyecek.

* Microsoft version classı ile version karşılaştırması yapabilirsiniz.

* Middleware ve IOC hakkında 2 dakikalık okunacak, ingilizcesi iyi olanlar ingilizce yazacaklar, yetersiz olanlar ise türkçe makale yayımlayacaklar. Linkedin ve Medium yayınlanacak. Yayınlamadan önce telegram grubunda paylaşın ve birbirinize geri dönüş yapınız.
