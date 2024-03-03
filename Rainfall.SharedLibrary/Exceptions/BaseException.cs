using Rainfall.SharedLibrary.Models;
using System;
using System.Collections.Generic;

namespace Rainfall.SharedLibrary.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message)
           : base(message)
        {
            Errors = new List<Error>();
        }

        public IList<Error> Errors { get; }
    }
}
