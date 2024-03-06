using Rainfall.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rainfall.Application.Models
{

    public class ValidationResponseError
    {
        public ValidationResponseError(string message, string stationdId,  IList<Error> errors)
        {
            StationId = stationdId;
            Message = message;
            if (errors != null)
                Errors = errors.Select(x => new BadRequestOutcome(x.propertyName, x.message)).ToList();
        }

        [JsonPropertyName("detail")]
        public IList<BadRequestOutcome> Errors { get; protected set; }

        [JsonPropertyName("message")]
        public string Message { get; protected set; }

        [JsonIgnore]
        public string StationId { get; protected set; }

    }

    public abstract class Outcome
    {
        public Outcome(string field, string message)
        {
            PropertyName = field;
            Message = message;
        }

        public virtual string PropertyName { get; protected set; }

        public virtual string Message { get; protected set; }
    }


    public class BadRequestOutcome : Outcome
    {
        public BadRequestOutcome(string field, string message)
            : base(field, message)
        {

        }

        [JsonPropertyName("propertyName")]
        public override string PropertyName { get => base.PropertyName; protected set => base.PropertyName = value; }


        [JsonPropertyName("message")]
        public override string Message { get => base.Message; protected set => base.Message = value; }
    }
}
