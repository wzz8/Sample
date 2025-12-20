using System;
using System.Collections.Generic;

namespace Login_Sample.Data
{
    /// <summary>
    /// 备件入库记录模型类
    /// </summary>
    public class InventoryInRecord
    {
        /// <summary>
        /// 入库记录ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 入库单号
        /// </summary>
        public string InboundNumber { get; set; }
        
        /// <summary>
        /// 入库人
        /// </summary>
        public string InboundPerson { get; set; }
        
        /// <summary>
        /// 含税总金额
        /// </summary>
        public decimal TotalAmountWithTax { get; set; }
        
        /// <summary>
        /// 不含税总金额
        /// </summary>
        public decimal TotalAmountWithoutTax { get; set; }
        
        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime InboundDate { get; set; }
        
        /// <summary>
        /// 入库备注
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 入库的备件项
        /// </summary>
        public List<InventoryInItem> InboundItems { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public InventoryInRecord()
        {
            InboundNumber = string.Empty;
            InboundPerson = string.Empty;
            InboundDate = DateTime.Now;
            Remarks = string.Empty;
            InboundItems = new List<InventoryInItem>();
        }
    }
    
    /// <summary>
    /// 入库备件项模型类
    /// </summary>
    public class InventoryInItem
    {
        /// <summary>
        /// 入库项ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 入库记录ID
        /// </summary>
        public int InventoryInRecordId { get; set; }
        
        /// <summary>
        /// 库存配件ID
        /// </summary>
        public int InventoryItemId { get; set; }
        
        /// <summary>
        /// 库存配件
        /// </summary>
        public InventoryItem InventoryItem { get; set; }
        
        /// <summary>
        /// 入库数量
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal UnitPriceWithTax { get; set; }
        
        /// <summary>
        /// 不含税单价
        /// </summary>
        public decimal UnitPriceWithoutTax { get; set; }
        
        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }
        
        /// <summary>
        /// 税额
        /// </summary>
        public decimal TaxAmount { get; set; }
        
        /// <summary>
        /// 含税小计
        /// </summary>
        public decimal SubtotalWithTax { get; set; }
        
        /// <summary>
        /// 不含税小计
        /// </summary>
        public decimal SubtotalWithoutTax { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public InventoryInItem()
        {
            InventoryItem = new InventoryItem();
            TaxRate = 0.13m; // 默认税率13%
        }
    }
}