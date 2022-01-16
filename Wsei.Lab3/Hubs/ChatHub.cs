using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsei.Lab3.Entities;
using Wsei.Lab3.Services;

namespace Wsei.Lab3.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatMessagesRepository _repository;

        public ChatHub(IChatMessagesRepository repository)
        {
            _repository = repository;
        }

        public override async Task OnConnectedAsync()
        {
            var user = Context.User.Identity.Name;

            await Clients.Caller.SendAsync("OnMessageSent", "admin", "Hi there, " + user);
        }

        public async Task SendMessage(string message)
        {
            var user = Context.User.Identity.Name;

            await Clients.All.SendAsync("OnMessageSent", user, message);

            var messageEntity = new ChatMessageEntity
            {
                Username = user,
                Content = message,
            };
            _repository.Add(messageEntity);
        }
    }
}
