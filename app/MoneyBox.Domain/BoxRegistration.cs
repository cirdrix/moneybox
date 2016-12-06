namespace MoneyBox.Domain
{
    using System;

    public class BoxRegistration
    {
        public int Id { get; set; }
        public Box Box { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
