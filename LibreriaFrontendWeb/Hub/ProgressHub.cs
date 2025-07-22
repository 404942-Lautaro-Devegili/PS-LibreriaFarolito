using LibreriaBack.Datos.Entidades.Logistica;
using Microsoft.AspNetCore.SignalR;
namespace LibreriaFrontendWeb.Hub
{
    public class ProgressHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendProgress(int progress, int processed, int remaining)
        {
            await Clients.All.SendAsync("ReceiveProgress", progress, processed, remaining);
        }
    }
}