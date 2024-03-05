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

        public RainfallException(string message)
            : base(message)
        {
            Errors = new List<Error>();
        }

        public RainfallException(string stationdId, string message)
         : base(message)
        {
            this.stationId = stationdId;
        }


        public string stationId { get; protected set; }

        public IList<Error> Errors { get; }

    }
}
