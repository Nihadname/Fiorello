using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using WebApplication11.Models;

    public class ChatHub:Hub
    {
        private readonly UserManager<AppUser> userManager;
        public ChatHub(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now.ToString("dd.mm.yyyy"));
    }
    public override Task OnConnectedAsync()
    {
        var user = userManager.FindByNameAsync(Context.User.Identity.Name).Result;
        user.ConnectionId = Context.ConnectionId;
        var updateResult = userManager.UpdateAsync(user).Result;
        Clients.All.SendAsync("UserConnected", user.Id);

        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var user = userManager.FindByNameAsync(Context.User.Identity.Name).Result;
        user.ConnectionId = null;
        var updateResult = userManager.UpdateAsync(user).Result;
        Clients.All.SendAsync("UserDisConnected", user.Id);

        return base.OnDisconnectedAsync(exception);
    }
}
