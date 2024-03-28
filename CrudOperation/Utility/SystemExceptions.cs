using Microsoft.AspNetCore.Http;
using System;

namespace CrudOperation.Utility
{
    public class SystemExceptions : Exception
    {
        public SystemExceptions(string message, object inner) : base(message)
        {
            base.Data.Add("ErrorCode", inner);
        }
        public static Int16 InternalExcep(Int16 errorCode)
        {
            return (errorCode > 0 ? errorCode : Convert.ToInt16(StatusCodes.Status500InternalServerError));
        }

    }
}
