using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Domain
{
    public class Box
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
    }
}
