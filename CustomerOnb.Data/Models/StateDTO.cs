using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnb.Data.Models
{
   public class StateReq
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Name must be character only")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 character in length.")]
        public string Name { get; set; }
    }

    //public class StateResp
    //{
    //    public string Code { get; set; }
    //    public string Meassage { get; set; }
    //}
}
