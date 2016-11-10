namespace MoneyBox.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BoxRegistration
    {
        public int Id { get; set; }
        public Box Box { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
