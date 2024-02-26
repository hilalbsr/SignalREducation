using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Api.Hubs;

namespace SignalR.Api.Controllers
{
    //SignalR'ı kullanabilmek için Microsoft.AspNetCore.SignalR paketini yüklememiz gerekmektedir.
    //SignalR, gerçek zamanlı web uygulamaları geliştirmemizi sağlayan bir teknolojidir.
    //SignalR, web uygulamalarında gerçek zamanlı iletişim sağlar.
    //SignalR, web uygulamalarında server ve client arasında gerçek zamanlı iletişim sağlar.
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<MyHub> _hubContext;

        public NotificationController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        //Tüm client'lara mesaj gönderir.
        [HttpGet("{teamCount}")]
        public async Task<IActionResult> SetTeamCount(int teamCount)
        {
            MyHub.TeamCount = teamCount;

            //Clients.All.SendAsync --bu hub'a bağlı olan tüm client'lara mesajı gönderir.
            await _hubContext.Clients.All.SendAsync("Notify", $"Arkadaşlar takım  {teamCount} kişi olacaktır.");

            return Ok();
        }
    }
}
