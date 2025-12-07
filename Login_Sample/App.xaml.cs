using System.Configuration;
using System.Data;
using System.Windows;
using Login_Sample.Data;
using Microsoft.EntityFrameworkCore;

namespace test;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
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

