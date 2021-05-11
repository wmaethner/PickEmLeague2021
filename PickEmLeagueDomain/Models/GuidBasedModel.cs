using System;
using System.ComponentModel.DataAnnotations;

namespace PickEmLeagueDomain.Models
{
    public class GuidBasedModel
    {
        [Editable(false)]
        public Guid Id { get; set; }
    }
}
