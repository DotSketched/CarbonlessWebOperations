using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonlessWebOperations.Classes.HelperObjects
{
    public class WebOperationFile 
    {
        /// <summary>
        /// The file name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The file sizr in bytes
        /// </summary>
        public long SizeInBytes { get; set; }
        /// <summary>
        /// The file extension
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// The file directory on disk
        /// </summary>
        public string Path { get; set; }
        
    }
}
