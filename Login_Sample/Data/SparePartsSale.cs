using System;

namespace Login_Sample.Data
{
    /// <summary>
    /// 配件销售模型类
    /// </summary>
    public class SparePartsSale
    {
        /// <summary>
        /// 销售记录ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 配件ID
        /// </summary>
        public int InventoryItemId { get; set; }
        
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }
        
        /// <summary>
        /// 销售数量
        /// </summary>
        public int QuantitySold { get; set; }
        
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// 销售总额
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// 销售日期
        /// </summary>
        public DateTime SaleDate { get; set; }
        
        /// <summary>
        /// 销售人
        /// </summary>
        public string SalesPerson { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 关联的配件
        /// </summary>
        public InventoryItem InventoryItem { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public SparePartsSale()
        {
            CustomerName = string.Empty;
            CustomerPhone = string.Empty;
            SalesPerson = string.Empty;
            Remarks = string.Empty;
            SaleDate = DateTime.Now;
        }
    }
}