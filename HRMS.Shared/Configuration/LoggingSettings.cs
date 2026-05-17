using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Configuration
{
    public class LoggingSettings
    {
        public bool IsWriteLog { get; set; }

        public bool LogRequestBody { get; set; }

        public bool LogResponseBody { get; set; }
    }
}
