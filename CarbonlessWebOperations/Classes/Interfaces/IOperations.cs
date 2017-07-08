using CarbonlessWebOperations.Classes.HelperObjects;
using CarbonlessWebOperations.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CarbonlessWebOperations.Classes.Events.EventHandlers;

namespace CarbonlessWebOperations.Classes.Interfaces
{
    interface IOperations
    {
        event AsyncWebOperationsCompleteEventHandler WebOperationCompleted;
        event AsyncWebOperationsProgressEventHandler WebOperationProgress;
        WebRequest WebRequest { get; set; }
        WebOperationFile FileDetail { get; set; }
        void PauseDownload();
        void ResumeDownload();
        void DownloadFileAsync(WebOperationDownloadObject file);
        void StopDownload();
    }
}
