using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class HomeStatisticsViewModel
    {
        /// <summary>
        /// Tổng số sản phẩm trong hệ thống
        /// </summary>
        public int TotalProducts { get; set; }

        /// <summary>
        /// Tổng số đơn hàng
        /// </summary>
        public int TotalOrders { get; set; }

        /// <summary>
        /// Tổng doanh thu
        /// </summary>
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Số đơn hàng theo từng tháng (để vẽ biểu đồ)
        /// </summary>
        public Dictionary<string, int> OrdersByMonth { get; set; }

        /// <summary>
        /// Tổng doanh thu theo từng tháng
        /// </summary>
        public Dictionary<string, decimal> RevenueByMonth { get; set; }
    }
}

