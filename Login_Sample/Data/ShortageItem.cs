using System;

namespace Login_Sample.Data
{
    /// <summary>
    /// 缺件信息数据模型
    /// </summary>
    public class ShortageItem
    {
        /// <summary>
        /// 缺件ID
        /// </summary>
        public int ShortageId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string VehicleBrand { get; set; }

        /// <summary>
        /// 车辆型号
        /// </summary>
        public string VehicleModel { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// 配件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 配件编号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// 所需数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 缺件原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisteredBy { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime RegisteredTime { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime? ArrivalDate { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public DateTime? NotificationDate { get; set; }

        /// <summary>
        /// 处理日期
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// 状态（已确认、已订货、已到货、已通知、已处理）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ShortageItem()
        {
            CustomerName = string.Empty;
            PhoneNumber = string.Empty;
            VehicleBrand = string.Empty;
            VehicleModel = string.Empty;
            LicensePlate = string.Empty;
            PartName = string.Empty;
            PartNumber = string.Empty;
            Reason = string.Empty;
            RegisteredBy = string.Empty;
            RegisteredTime = DateTime.Now;
            OrderNumber = string.Empty;
            Status = "已确认";
            Remarks = string.Empty;
        }
    }
}