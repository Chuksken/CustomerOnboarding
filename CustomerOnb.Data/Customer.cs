﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CustomerOnb.Data
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Password { get; set; }
        public int LgaId { get; set; }
        public int StateId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Lga Lga { get; set; }
        public virtual State State { get; set; }
    }
}