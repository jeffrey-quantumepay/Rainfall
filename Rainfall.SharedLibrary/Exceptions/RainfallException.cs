using Rainfall.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.SharedLibrary.Exceptions
{
    public abstract class RainfallException : Exception
    {

        public IList<Error> Errors { get; }

        public RainfallException(string message)
            : base(message)
        {
            Errors = new List<Error>();
        }

    }
}
