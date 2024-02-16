
ASP.NET CORE + SIGNALR

4. SignalR nedir ?

Uygulamarımıza realtime özellik kazandıran bir opensource kütüphanedir.
Websocket teknolojisi.
Websocket --tek bir tcp üzerinden client ve server arasında iki yönlü iletişimi sağlayan protokoldür. Client ve serverin haberleşmesi için belli bir kurallar olmalı.
2011 yılında ortaya çıktı. 2012 de asp.net te çıktı.
RealTime -- client ve server'in anlık olarak haberleşmesi.--browserda chat uygulaması.sayfa yenilenmeden meajlaşılması. --açık artırma siteleri. --monitoring işlemi.chart'ların güncellenmesi.



5. WebSocket Server uygulaması nasıl geliştirebiliriz ? SignalR nasıl çalışır ?

Asp.Net Core : SignalR
Node.js : Socket.IO
Pyhton : WebSockets

SignalR'ın merkezinde HUB bir dediğimiz merkezi yapı var. Client ile server arasındaki haberleşmeyi bu HUB(class) yapar.

Client1 --> IN --> HUB --> OUT --> Client2 Client3 Client4(Bu client'lar HUB 'a subscribe -abone olanlar) --> OUT --> Client1

Anlık olarak data izlemek, anlık olarak client'ları bilgilendirmek.


6. SignalR Server uygulaması inşa etmek

Client           (Server Method)
Client Method	 (Client Method)

						SignalR Server
						Server Method
						Client Method
Client 
Client Method 	 (Client Method)

Client'tan Server'a Server'dan Client'a akış oluyor. Server, Client metodunu çalıştırabiliyor.

Client server'a istek yapıyor. Server içerisindeki Client Metodu kullanarak ilk önce Client'a çağrı yapıyor.Client Server'ı çalıştırdı. Server Client metodlarını çalıştırdı.



9. SignalR Client Log Mesajları

Sorunsuzdan sorunluya daoğru

1.Trace  		--Uygulama hakkında genel bilgiler vermkek.
2.Debug  		--Uygulama hakkında genel bilgiler vermkek.
3.Information  	--Gerekli bilgiler
4.Warning		--Bir problem var. Ama uygulama hala çalışır durumda.
5.Error			--Hata var. lgilen
6.Critical		--Hata var.Uygulamanın genelinde bir sıkıntıya sebep olabilir.


10. SignalR Client withAutomaticReconnect method

withAutomaticReconnect  --client bağlantısını yaptı. Bir nedenden dolayı bağlantı koparsa bu metotla otomatik bağlantı yapmasını sağlar.

0sn,2sn,10sn,30sn arası istekler yapar. 30snden sonra başarısızsa bağlantı yapma denemelerini bırakıyor.
onreconnection 	--bağlantı koptunktan sonra 0sn de bu metot event fırlatır. Tekrar bağlanır. Bağlantı koptu, tekrar bağlanıyorum diyebilirsiniz.
onreconntected 		--bağlantı tekrar sağlanırsa bu event fırlatılır ve bağlantı kuruldu diyebilinebilir.
onclose	-- Bağlantı kurulmazsa bu metot fırlatırlır. Bağlantı tamamen koptu. Sayfanızı yenilebilirsiniz.

Döngüyede sokulabilir.
onreconnection ve onreconntected birer kez çalışır.


