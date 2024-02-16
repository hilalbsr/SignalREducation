using Microsoft.AspNetCore.SignalR;

namespace SignalR.Api.Hubs
{
    //Client'tan gelecek mesajları almak için kullanılır.
    //Client'a mesaj göndermek için kullanılır.
    //Hub sınıfından türetilir.
    //Hub sınıfı, SignalR'ın temel sınıfıdır.
    //Obje örneği oluşturulmaz. SignalR tarafından yönetilir.
    //Hub her bir client için bir instance oluşturur.
    //Her client için bir hub oluşturulur.
    //Hub'lar, client'lar arasında iletişimi sağlar.
    //Hub'lar, client'lar arasında stateful bir iletişim sağlar.
    public class MyHub :Hub
    {
        public List<string> MessagesList { get; set; } = new List<string>();

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        /// <summary>
        /// Tüm client'lara mesaj gönderir.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string message)
        {
            MessagesList.Add(message);

            //Clients.All --bu hub'a bağlı olan tüm client'lara mesajı gönderir.
            //ReceiveMessage --client tarafında bu isimde bir metot olmalıdır. bu metot olmazsa mesaj alınamaz.Subrscribe olmalılar buna.
            //message --client tarafına gönderilecek olan mesaj. tipi önemli değil. object olabilir.class olabilir.
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
     
        public async Task GetMessages()
        {
            await Clients.All.SendAsync("ReceiveNames", MessagesList);
        }
    }
}
