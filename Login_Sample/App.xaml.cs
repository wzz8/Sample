using System.Configuration;
using System.Data;
using System.Windows;
using Login_Sample.Data;
using Microsoft.EntityFrameworkCore;

namespace Login_Sample;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // 全局用户状态
    public static User? CurrentUser { get; set; }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // 初始化数据库
        using (var context = new ApplicationDbContext())
        {
            // 确保数据库和表存在
            context.Database.EnsureCreated();
        }
    }
}

