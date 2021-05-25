using System;
using System.ComponentModel.DataAnnotations;

namespace PickEmLeagueDomain.Models
{
    public abstract class GuidBasedModel
    {
        [Editable(false)]
        public Guid Id { get; set; }

        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
    }
}
