using System.ComponentModel.DataAnnotations.Schema;

namespace MSLoggerAPI.Model
{
    [Table("AppLogger")]
    public class AppLogger
    {
        public int Id { get; set; }
        public string LogType { get; set; } = "Info";
        public DateTime LoggingTime { get; set; } = DateTime.Now;
        public string LoggingProgram { get; set; }
        public string LoggingClass { get; set; }
        public string LoggingMethod { get; set; }       
        public string Message { get; set; }
    }
}
