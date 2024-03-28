using System;

namespace CrudOperation.Utility
{
    public class ResponseHelper
    {
        public Int16 Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } = string.Empty;
    }
}
