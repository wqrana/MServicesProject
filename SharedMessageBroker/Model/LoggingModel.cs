using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMessageBroker.Model
{
    public class LoggingModel
    {
        public DateTime LoggingTime { get; set; }=DateTime.Now;
        public string LoggingProgram { get; set; }
        public string LoggingClass { get; set; }
        public string LoggingMethod { get; set; }
        public string LogType { get; set; } = "Info";
        public string Message { get; set; }
               
    }
}
