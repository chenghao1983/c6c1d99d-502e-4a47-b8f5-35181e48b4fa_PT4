using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ActiveLearning.Business.SignalRHub
{
    public class StatisticsHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }
}