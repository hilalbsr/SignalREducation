using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.Api.Models;

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
    public class MyHub : Hub
    {
        public static List<string> Names { get; set; } = new List<string>();
        private static int ClientCount { get; set; } = 0;
        public static int TeamCount { get; set; } = 0;

        private readonly AppDbContext _context;

        public MyHub(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Tüm client'lara mesaj gönderir.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task SendName(string name)
        {
            if (Names.Count >= TeamCount)
            {
                //Clients.Caller --bu hub'a bağlı olan client'a mesajı gönderir.
                //ReceiveMessage --client tarafında bu isimde bir metot olmalıdır. bu metot olmazsa mesaj alınamaz.Subrscribe olmalılar buna.
                //name --client tarafına gönderilecek olan mesaj. tipi önemli değil. object olabilir.class olabilir.
                //await Clients.Caller.SendAsync("ReceiveMessage", "Takım dolu.");


                await Clients.Caller.SendAsync("Error", $"Takım en fazla {TeamCount} kişi olabilir.");
            }
            else
            {
                Names.Add(name);

                //Clients.All --bu hub'a bağlı olan tüm client'lara mesajı gönderir.
                //ReceiveMessage --client tarafında bu isimde bir metot olmalıdır. bu metot olmazsa mesaj alınamaz.Subrscribe olmalılar buna.
                //name --client tarafına gönderilecek olan mesaj. tipi önemli değil. object olabilir.class olabilir.
                await Clients.All.SendAsync("ReceiveName", name);
            }
        }

        //Kullanıcı ilk girdiği anda olan dataların gösterilmesi
        // Veritaından gelen dataların gösterilmesi
        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }

        //Client'ın bağlantı sağladığı zaman çalışır.
        //Client'ın bağlantı sağladığı zaman çalışacak kodlar buraya yazılır.
        public override async Task OnConnectedAsync()
        {
            ClientCount++;

            //Clients.All.SendAsync --bu hub'a bağlı olan tüm client'lara mesajı gönderir.
            //await Clients.All.SendAsync("ReceiveClientCount", $"ConnectionId : {Context.ConnectionId} bağlandı. Toplam bağlı client sayısı: {ClientCount}");      

            //await Clients.All.SendAsync("ReceiveClientCount", $"Toplam bağlı client sayısı: {ClientCount}");
            await Clients.All.SendAsync("ReceiveClientCount", $"{ClientCount}");

            await base.OnConnectedAsync();
        }

        //Client'ın bağlantısını kestiği zaman çalışır.
        //Client'ın bağlantısını kestiği zaman çalışacak kodlar buraya yazılır.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", $"ConnectionId : {Context.ConnectionId} bağlantısı kesildi. Toplam bağlı client sayısı: {ClientCount}");

            await base.OnDisconnectedAsync(exception);
        }

        #region Groups

        //Bir client'ın bir gruba eklenmesi.oda takım vs
        public async Task AddToGroup(string teamName)
        {
            //Groups.AddToGroupAsync --client'ı bir gruba eklemek için kullanılır.
            //Context.ConnectionId --client'ın bağlantı id'si.
            //teamName --teamName adı.
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);


            //await Clients.Group(teamName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined {teamName}");
        }

        //Bir client'ın bir gruba eklenmesi.oda takım vs
        public async Task RemoveToGroup(string teamName)
        {
            //Groups.RemoveFromGroupAsync --client'ı bir gruptan çıkarmak için kullanılır.
            //Context.ConnectionId --client'ın bağlantı id'si.
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);

            //await Clients.Group(teamName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined {teamName}");
        }

        //Bir gruba mesaj gönderilmesi.
        public async Task SendNameByGroup(string name, string teamName)
        {
            var team = _context.Teams.FirstOrDefault(x => x.Name == teamName);
            if (team is not null)
            {
                team.Users.Add(new User() { Name = teamName });
            }
            else
            {
                var newTeam = new Team() { Name = teamName };
                newTeam.Users.Add(new User() { Name = name });
                _context.Teams.Add(newTeam);
            }

            await _context.SaveChangesAsync();

            //Clients.Group --belirtilen gruba bağlı olan tüm client'lara mesajı gönderir.
            await Clients.Group(teamName).SendAsync("ReceiveMessageByGroup", name, team.Id);
        }

        //Bir gruptaki client'ların listesini almak.
        public async Task GetNamesByGroup()
        {
            var teams = _context.Teams.Include(x => x.Users).Select(x => new
            {
                teamId = x.Id,
                users = x.Users.ToList()
            });

            await Clients.All.SendAsync("ReceiveNamesByGroup", teams);
        }


        #endregion


        #region SignalR Server Hub Complex Type parametre

        public async Task SendProduct(Product product)
        {
            await Clients.All.SendAsync("ReceiveProduct", product);
        }

        #endregion
    }
}
