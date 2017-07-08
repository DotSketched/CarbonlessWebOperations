using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonlessWebOperations.Classes.Events
{
    public class EventHandlers
    {
        public delegate void AsyncWebOperationsCompleteEventHandler(object sender, AsyncWebOperationsCompleteArgs e);
        public delegate void AsyncWebOperationsProgressEventHandler(object sender, AsyncWebOperationsProgressArgs e);
    }
}
