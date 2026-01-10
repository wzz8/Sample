using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Login_Sample;
using Login_Sample.Data;
using Microsoft.EntityFrameworkCore;

namespace Login_Sample.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool _isNoEnabled = true;
        private string _No = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private Visibility _NoVisibility = Visibility.Visible;
        private double _windowHeight = 320;
        private double _windowWidth = 380;
        private bool _isLoading = false;

        /// <summary>
        /// 是否启用店号输入
        /// </summary>
        public bool IsNoEnabled
        {
            get { return _isNoEnabled; }
            set 
            {
                if (_isNoEnabled != value)
                {
                    _isNoEnabled = value;
                    OnPropertyChanged();
                    UpdateUIState();
                }
            }
        }

        /// <summary>
        /// 店号
        /// </summary>
        public string No
        {
            get { return _No; }
            set 
            {
                if (_No != value)
                {
                    _No = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username
        {
            get { return _username; }
            set 
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set 
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 密码变更命令
        /// </summary>
        public ICommand PasswordChangedCommand { get; private set; }

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
        /// 错误信息可见性
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
        /// 分厂号输入框可见性
        /// </summary>
        public Visibility NoVisibility
        {
            get { return _NoVisibility; }
            set 
            {
                if (_NoVisibility != value)
                {
                    _NoVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 窗口高度
        /// </summary>
        public double WindowHeight
        {
            get { return _windowHeight; }
            set 
            {
                if (_windowHeight != value)
                {
                    _windowHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 窗口宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _windowWidth; }
            set 
            {
                if (_windowWidth != value)
                {
                    _windowWidth = value;
                    OnPropertyChanged();
                }
            }
        }

        private ImageSource m_Icon;
        private readonly ApplicationDbContext _dbContext;

        public ImageSource Icon
        {
            get { return m_Icon; }
            set
            {
                if (m_Icon != value)
                {
                    m_Icon = value;
                    OnPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// 是否正在加载
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// 登录命令
        /// </summary>
        public RelayCommand LoginCommand { get; set; }

        /// <summary>
        /// 注册命令
        /// </summary>
        public RelayCommand RegisterCommand { get; set; }

        /// <summary>
        /// 关闭命令
        /// </summary>
        public RelayCommand CloseCommand { get; set; }
        
        /// <summary>
        /// 设置命令
        /// </summary>
        public RelayCommand SettingsCommand { get; set; }

        public LoginViewModel()
        {
            // 初始化数据库上下文
            _dbContext = new ApplicationDbContext();
            
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
            CloseCommand = new RelayCommand(Close);
            PasswordChangedCommand = new RelayCommand(HandlePasswordChanged);
            SettingsCommand = new RelayCommand(Settings);
            IsNoEnabled = true;
            
            UpdateUIState();
        }
        
        /// <summary>
        /// 处理密码变更的方法
        /// </summary>
        /// <param name="password">密码字符串</param>
        private void HandlePasswordChanged(object? password)
        {
            if (password is string passwordStr)
            {
                Password = passwordStr;
            }
        }
        
        /// <summary>
        /// 登录方法（更新为接受object参数）
        /// </summary>
        private async void Login(object? parameter)
        {
            // 原有登录逻辑不变
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "用户名和密码不能为空";
                ErrorVisibility = Visibility.Visible;
                return;
            }
            
            try
            {
                IsLoading = true;
                ErrorVisibility = Visibility.Hidden;
                
                // 从数据库验证用户
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == Username);
                if (user != null)
                {
                    // 验证密码
                    if (PasswordHasher.VerifyPassword(user.PasswordHash, Password))
                    {
                        ErrorVisibility = Visibility.Hidden;
                        MessageBox.Show("登录成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                        // 设置全局用户状态
                        App.CurrentUser = user;

                        // 打开主界面
                        MainWindow mainWindow = new MainWindow();
                        Application.Current.MainWindow = mainWindow;
                        mainWindow.Show();

                        // 关闭登录窗口
                        if (parameter is Window loginWindow)
                        {
                            loginWindow.Close();
                        }
                    }
                    else
                    {
                        ErrorMessage = "用户名或密码错误";
                        ErrorVisibility = Visibility.Visible;
                    }
                }
                else
                {
                    ErrorMessage = "用户名或密码错误";
                    ErrorVisibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "登录失败：" + ex.Message;
                ErrorVisibility = Visibility.Visible;
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        /// <summary>
        /// 注册方法（更新为接受object参数）
        /// </summary>
        private void Register(object? parameter)
        {
            // 显示注册窗口
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            
            // 关闭当前登录窗口
            if (parameter is Window loginWindow)
            {
                loginWindow.Hide();
            }
        }
        
        /// <summary>
        /// 关闭方法（更新为接受object参数）
        /// </summary>
        private void Close(object? parameter)
        {
            // 原有关闭逻辑不变
        }
        
        /// <summary>
        /// 设置方法
        /// </summary>
        private void Settings(object? parameter)
        {
            // 显示设置窗口
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        /// <summary>
        /// 更新UI状态
        /// </summary>
        private void UpdateUIState()
        {
            if (IsNoEnabled)
            {
                NoVisibility = Visibility.Visible;
                WindowHeight = 270;
                WindowWidth = 380;
            }
            else
            {
                NoVisibility = Visibility.Collapsed;
                WindowHeight = 230;
                WindowWidth = 360;
            }
            Icon = CreateLoginIcon();
        }

        /// <summary>
        /// 创建登录图标
        /// </summary>
        private DrawingImage CreateLoginIcon()
        {
            // 创建绘图组
            DrawingGroup drawingGroup = new DrawingGroup();

            // 添加背景矩形
            GeometryDrawing background = new GeometryDrawing
            {
                Brush = new SolidColorBrush(Color.FromRgb(0, 120, 215)),
                Geometry = new RectangleGeometry(new Rect(0, 0, 64, 64), 8, 8)
            };
            drawingGroup.Children.Add(background);

            // 添加用户图标
            GeometryDrawing userIcon = new GeometryDrawing
            {
                Brush = Brushes.White,
                Pen = new Pen(Brushes.White, 4),
                Geometry = Geometry.Parse("M32,12 A12,12 0 1,1 32,36 A12,12 0 1,1 32,12 Z M32,36 L32,56 M22,46 L42,46")
            };
            drawingGroup.Children.Add(userIcon);

            return new DrawingImage(drawingGroup);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}