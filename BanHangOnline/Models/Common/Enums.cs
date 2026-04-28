namespace BanHangOnline.Models.Common
{
    /// <summary>
    /// Trạng thái đơn hàng
    /// </summary>
    public enum OrderStatus
    {
        Pending = 0,           // Chờ xử lý
        Processing = 1,        // Đang xử lý
        Shipped = 2,           // Đã gửi hàng
        Delivered = 3,         // Đã nhận hàng
        Cancelled = 4          // Bị hủy
    }

    /// <summary>
    /// Trạng thái thanh toán
    /// </summary>
    public enum PaymentStatus
    {
        Unpaid = 0,            // Chưa thanh toán
        Paid = 1,              // Đã thanh toán
        Failed = 2             // Thanh toán thất bại
    }

    /// <summary>
    /// Trạng thái sản phẩm
    /// </summary>
    public enum ProductStatus
    {
        Active = 1,            // Hoạt động
        Inactive = 0           // Không hoạt động
    }
}