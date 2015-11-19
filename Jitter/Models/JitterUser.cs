using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //What was added from [Key] 
using System.Linq;
using System.Web;

namespace Jitter.Models
{
    public class JitterUser //nov_19 branch
    {
        [Key] //Needs to always go above the Id. A red squilly will pop up and select the using it provides. It should pop up above.
        public int JitterUserId { get; set; }

        [MaxLength(161)] //Did not put Required because the JitterUser does not have to have a description unless they want one.
        public string Description { get; set; }

        public string FirstName { get; set; }

        [Required] //This means you must have a Handle to use Jitter. It will only do Handle since it is above it and nothing below Handle.
        [MaxLength(20)] //Did not have to specify 'int' because it already knows that 20 is an int.
        [MinLength(3)]
        [RegularExpression(@"^[a-z\d]+[-_a-zA-Z\d]{0,2}[a-zA-Z\d]+")] //Always after RegularExpression( @"" ) always goes int he brackets
        public string Handle { get; set; }

        public string LastName { get; set; }
        public string Picture { get; set; }
    }
}
