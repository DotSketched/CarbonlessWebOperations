using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonlessWebOperations.Classes.Enums
{
    /// <summary>
    /// Holds the enums for the CarbonlessWebOperations library.
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// The state of the operation.
        /// </summary>
        public enum State
        {
            Started,
            Paused,
            InProgress,
            Stopped,
            Completed
        }
    }
}
