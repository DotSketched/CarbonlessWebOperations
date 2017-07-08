using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonlessWebOperations.Classes.HelperObjects
{
    public class WebOperationDownloadObject
    {
        /// <summary>
        /// The file url.
        /// </summary>
        public Uri Url { get; set; }
        /// <summary>
        /// Set the name of the file being downloaded. 
        /// </summary>
        public string Filename { get; set; }
    }
}
