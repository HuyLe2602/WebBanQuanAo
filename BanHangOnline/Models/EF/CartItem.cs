namespace BanHangOnline.Models.EF
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }

        public CartItem() { }

        public CartItem(int productId, int quantity, decimal price, Product product)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            Product = product;
        }

        public decimal Total => Quantity * Price;
    }
}