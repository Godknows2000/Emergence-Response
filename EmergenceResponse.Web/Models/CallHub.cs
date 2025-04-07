namespace EmergenceResponse.Web.Models;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class CallHub : Hub
{
    public async Task StartCall(string receiverId)
    {
        await Clients.User(receiverId).SendAsync("ReceiveCall", Context.UserIdentifier);
    }

    public async Task AcceptCall(string callerId)
    {
        await Clients.User(callerId).SendAsync("CallAccepted", Context.UserIdentifier);
    }

    public async Task SendOffer(string receiverId, string offer)
    {
        await Clients.User(receiverId).SendAsync("ReceiveOffer", offer);
    }

    public async Task SendAnswer(string senderId, string answer)
    {
        await Clients.User(senderId).SendAsync("ReceiveAnswer", answer);
    }

    public async Task SendIceCandidate(string userId, string candidate)
    {
        await Clients.User(userId).SendAsync("ReceiveIceCandidate", candidate);
    }

    public async Task EndCall(string userId)
    {
        await Clients.User(userId).SendAsync("CallEnded");
    }
    public async Task MakeCall(string adminId)
    {
        await Clients.User(adminId).SendAsync("ReceiveCall", Context.UserIdentifier);
    }
}
