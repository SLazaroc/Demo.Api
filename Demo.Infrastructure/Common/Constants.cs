using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Common
{
    public static class Constants
    {
        public static class ApiSettings
        {
            internal const string TraceLog = "TraceLog";
        }
        public static class Message
        {
            public static readonly string[] M1000 = { "1000", "Success." };
            public static readonly string[] M1001 = { "1001", "Something went wrong. Please try again later."};
            public static readonly string[] M1002 = { "1002", "Unable process. The service has shut down." };
            public static readonly string[] M1003 = { "1003", "No data found." };

            public static readonly string[] M5000 = { "5000", "The service was successfully shut down."  };
            public static readonly string[] M5001 = { "5001", "The service started successfully." };
            public static readonly string[] M5002 = { "5002", "Process running in background." };


        }
    }
}
