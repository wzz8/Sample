using System;

namespace Login_Sample.Data
{
    /// <summary>
    /// 库存配件模型类
    /// </summary>
    public class InventoryItem
    {
        /// <summary>
        /// 配件ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 配件名称
        /// </summary>
        public string ItemName { get; set; }
        
        /// <summary>
        /// 配件编号
        /// </summary>
        public string ItemNumber { get; set; }
        
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        
        /// <summary>
        /// 仓库位置
        /// </summary>
        public string WarehouseLocation { get; set; }
        
        /// <summary>
        /// 库存状态
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// 网点名称
        /// </summary>
        public string BranchName { get; set; }
        
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdatedTime { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public InventoryItem()
        {
            ItemName = string.Empty;
            ItemNumber = string.Empty;
            Supplier = string.Empty;
            WarehouseLocation = string.Empty;
            Status = string.Empty;
            BranchName = string.Empty;
            LastUpdatedTime = DateTime.Now;
        }
    }
}
