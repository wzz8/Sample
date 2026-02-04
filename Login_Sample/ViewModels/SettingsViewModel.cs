using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private string _serverAddress = "127.0.0.1"; // 默认服务器地址
        private string _serverPort = "8080"; // 默认服务器端口
        private bool _useProxy = false; // 默认不使用代理
        private string _proxyAddress = string.Empty; // 默认代理地址为空
        private string _proxyPort = "8080"; // 默认代理端口
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress
        {
            get { return _serverAddress; }
            set 
            { 
                if (_serverAddress != value)
                {
                    _serverAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public string ServerPort
        {
            get { return _serverPort; }
            set 
            { 
                if (_serverPort != value)
                {
                    _serverPort = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 是否使用代理服务器
        /// </summary>
        public bool UseProxy
        {
            get { return _useProxy; }
            set 
            { 
                if (_useProxy != value)
                {
                    _useProxy = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 代理服务器地址
        /// </summary>
        public string ProxyAddress
        {
            get { return _proxyAddress; }
            set 
            { 
                if (_proxyAddress != value)
                {
                    _proxyAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 代理服务器端口
        /// </summary>
        public string ProxyPort
        {
            get { return _proxyPort; }
            set 
            { 
                if (_proxyPort != value)
                {
                    _proxyPort = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            { 
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 错误提示可见性
        /// </summary>
        public Visibility ErrorVisibility
        {
            get { return _errorVisibility; }
            set 
            { 
                if (_errorVisibility != value)
                {
                    _errorVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 保存命令
        /// </summary>
        public RelayCommand<object> SaveCommand { get; set; }

        /// <summary>
        /// 取消命令
        /// </summary>
        public RelayCommand<object> CancelCommand { get; set; }

        /// <summary>
        /// 窗口引用
        /// </summary>
        private Window? _window;

        public SettingsViewModel(Window window)
        {
            _window = window;
            SaveCommand = new RelayCommand<object>(Save);
            CancelCommand = new RelayCommand<object>(Cancel);
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private void Save(object? parameter)
        {
            // 验证端口号是否为数字
            if (!int.TryParse(ServerPort, out int serverPortNum))
            {
                ShowError("服务器端口必须是有效的数字");
                return;
            }

            if (UseProxy && !string.IsNullOrEmpty(ProxyAddress) && !int.TryParse(ProxyPort, out int proxyPortNum))
            {
                ShowError("代理端口必须是有效的数字");
                return;
            }

            // 这里可以添加保存设置到配置文件的逻辑
            // 例如使用ConfigurationManager或JSON配置文件

            // 保存成功后关闭窗口
            _window?.Close();
        }

        /// <summary>
        /// 取消设置
        /// </summary>
        private void Cancel(object? parameter)
        {
            _window?.Close();
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        private void ShowError(string message)
        {
            ErrorMessage = message;
            ErrorVisibility = Visibility.Visible;
        }
    }
}