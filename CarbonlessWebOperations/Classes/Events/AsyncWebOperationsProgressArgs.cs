using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonlessWebOperations.Classes.Events
{
    public class AsyncWebOperationsProgressArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public long ProgressInBytes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Percentage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public long FileSizeInBytes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AsyncWebOperationsProgressArgs() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="data"></param>
        public AsyncWebOperationsProgressArgs(long fileSize, long data)
        {
            FileSizeInBytes = fileSize;
            Percentage = Convert.ToInt32((Convert.ToDecimal(data) / Convert.ToDecimal(fileSize)) * 100);
            ProgressInBytes = data;
        }
    }
}
