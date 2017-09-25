namespace ShopHierarchy
{
    public class Review
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}