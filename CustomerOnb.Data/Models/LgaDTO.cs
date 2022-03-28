using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnb.Data.Models
{
    public class LgaReq
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Name must be character only")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 character in length.")]
        public string Name { get; set; }

        [Required]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "invalid State ID")]
       // [StringLength(9, MinimumLength = 1, ErrorMessage = "State ID must be between 1 and 9 character in length.")]
        public int StateId { get; set; }
    }

}
