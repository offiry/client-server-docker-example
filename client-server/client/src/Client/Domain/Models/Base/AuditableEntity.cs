using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Models.Base
{
    public abstract class AuditableEntity
    {
        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public DateTime? LastModified { get; set; }
    }
}
