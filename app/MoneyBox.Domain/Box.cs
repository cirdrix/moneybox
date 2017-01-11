namespace MoneyBox.Domain
{
    public class Box
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
    }
}
