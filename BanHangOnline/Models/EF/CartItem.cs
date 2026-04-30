using System;

namespace BanHangOnline.Models.EF
{
    public class CartItem
    {
        // ID sản phẩm
        public int ProductId { get; set; }

        // Tên sản phẩm (hiển thị nhanh, không cần join DB)
        public string ProductName { get; set; }

        // Ảnh sản phẩm
        public string Image { get; set; }

        // Size (S, M, L, XL...)
        public string Size { get; set; }

        // Số lượng
        public int Quantity { get; set; }

        // Giá (đã xử lý sale nếu có)
        public decimal Price { get; set; }

        // Navigation (optional)
        public Product Product { get; set; }

        // Constructor rỗng
        public CartItem() { }

        // Constructor đầy đủ
        public CartItem(int productId, string productName, string image, int quantity, decimal price, string size, Product product = null)
        {
            ProductId = productId;
            ProductName = productName;
            Image = image;
            Quantity = quantity;
            Price = price;
            Size = size;
            Product = product;
        }

        // Tổng tiền
        public decimal Total
        {
            get { return Quantity * Price; }
        }
    }
}