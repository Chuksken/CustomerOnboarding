using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnb.Data.Models
{
   public class CustomerReq
    {
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public int LgaId { get; set; }
        public int StateId { get; set; }
    }

    public class CustomerResp
    {
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Lga { get; set; }
        public string State { get; set; }
    }

    public class PaginationReq
    {
       // public string OrderBy { get; set; }
        public string search { get; set; }
        public int PageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
