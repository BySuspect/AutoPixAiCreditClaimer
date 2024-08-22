using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPixAiCreditClaimer.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public required string name { get; set; }
        public required string email { get; set; }
        public required string pass { get; set; }
    }
}
