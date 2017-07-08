using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarbonlessWebOperations.Classes.Enums.Enums;

namespace CarbonlessWebOperations.Classes.Events
{
    public class AsyncWebOperationsCompleteArgs : EventArgs
    {
        /// <summary>
        /// The state of the current operation.
        /// </summary>
        public State DownloadState { get; set; } 

        /// <summary>
        /// The default constructor
        /// </summary>
        public AsyncWebOperationsCompleteArgs() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloadState">The state in which the operation should be.</param>
        public AsyncWebOperationsCompleteArgs(State downloadState)
        {
            DownloadState = downloadState;
        }
    }
}
