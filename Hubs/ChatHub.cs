﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Paul_Andreea_Proiect.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {   
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message) ;
        }

    }
}
