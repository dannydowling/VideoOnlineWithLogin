using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace VideoOnlineWithLogin.Server.Pages.Shared
{
    public class NavMenuComponentBase : ComponentBase
    {
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public readonly ChatHub chat;
        public string message { get; set; }

        public async Task OnValid()
        {
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        public void onClick(string message)
        {
            chat.Send(message);
        }

        }


        public class ChatHub : DynamicHub
    {
        public void Send(string message)
        {
            Clients.All.newMessage
                     (
                     Context.User.Identity.Name + "  : " + message + "                            " + DateTime.Now.ToShortTimeString()
                     );
        }
    }
}

