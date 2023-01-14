﻿namespace my_books.Data.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; } //XML prop
        public string LogEvent { get; set; }
    }
}
