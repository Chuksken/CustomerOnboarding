// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CustomerOnb.Data.Entities
{
    public partial class Lga
    {
        public Lga()
        {
            Customer = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public int StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
}