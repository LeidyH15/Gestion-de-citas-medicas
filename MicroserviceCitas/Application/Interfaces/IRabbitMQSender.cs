using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceCitas.Application.Interfaces
{
   public interface IRabbitMQSender
    {
        void SendMessage(object message);
    }
}
