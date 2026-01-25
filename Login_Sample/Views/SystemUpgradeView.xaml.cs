using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Login_Sample.Views
{
    public partial class SystemUpgradeView : UserControl
    {
        public SystemUpgradeView()
        {
            InitializeComponent();
        }

        // 数据网格项类
        public class DataGridItem
        {
            public string Version { get; set; }
            public string UpgradeDate { get; set; }
            public string Status { get; set; }
            public string Upgrader { get; set; }
            public string Description { get; set; }
        }
    }
}