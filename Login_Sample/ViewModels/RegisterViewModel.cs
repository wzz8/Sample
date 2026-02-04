using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Login_Sample.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Login_Sample.ViewModels
{
    public class RegisterViewModel : ObservableObject
    {
        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Collapsed;
        private ImageSource _icon;
        private bool _isLoading = false;
        private readonly ApplicationDbContext _dbContext;

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
        /// 邮箱地址
        /// </summary>
        public string Email
        {
            get { return _email; }
            set 
            { 
                if (_email != value)
                {
                    _email = value;
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
        /// 确认密码
        /// </summary>
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set 
            { 
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
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
        /// 窗口图标
        /// </summary>
        public ImageSource Icon
        {
            get { return _icon; }
            set 
            { 
                if (_icon != value)
                {
                    _icon = value;
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
        /// 注册命令
        /// </summary>
        public ICommand RegisterCommand { get; private set; }

        /// <summary>
        /// 返回登录命令
        /// </summary>
        public ICommand BackToLoginCommand { get; private set; }

        /// <summary>
        /// 密码变更命令
        /// </summary>
        public ICommand PasswordChangedCommand { get; private set; }

        /// <summary>
        /// 确认密码变更命令
        /// </summary>
        public ICommand ConfirmPasswordChangedCommand { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RegisterViewModel()
        {
            // 初始化数据库上下文
            _dbContext = new ApplicationDbContext();
            
            RegisterCommand = new RelayCommand<object>(Register);
            BackToLoginCommand = new RelayCommand<object>(BackToLogin);
            PasswordChangedCommand = new RelayCommand<object>(HandlePasswordChanged);
            ConfirmPasswordChangedCommand = new RelayCommand<object>(HandleConfirmPasswordChanged);
            
            // 创建图标
            Icon = CreateRegisterIcon();
        }

        /// <summary>
        /// 创建注册图标
        /// </summary>
        private DrawingImage CreateRegisterIcon()
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
            
            // 添加注册图标（用户加加号）
            GeometryDrawing registerIcon = new GeometryDrawing
            {
                Brush = Brushes.White,
                Pen = new Pen(Brushes.White, 4),
                Geometry = Geometry.Parse("M32,12 A12,12 0 1,1 32,36 A12,12 0 1,1 32,12 Z M32,36 L32,56 M22,46 L42,46 M42,24 L52,24 M47,19 L47,29")
            };
            drawingGroup.Children.Add(registerIcon);
            
            return new DrawingImage(drawingGroup);
        }

        /// <summary>
        /// 处理密码变更
        /// </summary>
        private void HandlePasswordChanged(object? parameter)
        {
            if (parameter is string password)
            {
                Password = password;
            }
        }

        /// <summary>
        /// 处理确认密码变更
        /// </summary>
        private void HandleConfirmPasswordChanged(object? parameter)
        {
            if (parameter is string password)
            {
                ConfirmPassword = password;
            }
        }

        /// <summary>
        /// 注册方法
        /// </summary>
        private async void Register(object? parameter)
        {
            // 验证输入
            if (!ValidateInput())
            {
                return;
            }

            // 执行注册逻辑
            try
            {
                IsLoading = true;
                
                // 检查用户名是否已存在
                if (await _dbContext.Users.AnyAsync(u => u.Username == Username))
                {
                    ErrorMessage = "用户名已存在";
                    ErrorVisibility = Visibility.Visible;
                    return;
                }
                
                // 检查邮箱是否已存在
                if (await _dbContext.Users.AnyAsync(u => u.Email == Email))
                {
                    ErrorMessage = "邮箱已被注册";
                    ErrorVisibility = Visibility.Visible;
                    return;
                }
                
                // 哈希密码
                string hashedPassword = PasswordHasher.HashPassword(Password);
                
                // 创建新用户
                var newUser = new User
                {
                    Username = Username,
                    Email = Email,
                    PasswordHash = hashedPassword,
                    CreatedAt = DateTime.UtcNow
                };
                
                // 保存到数据库
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();
                
                // 注册成功
                MessageBox.Show("注册成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // 注册成功后返回登录窗口
                BackToLogin(parameter);
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "注册失败：" + ex.Message;
                ErrorVisibility = Visibility.Visible;
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 返回登录窗口
        /// </summary>
        private void BackToLogin(object? parameter)
        {
            // 关闭当前窗口并打开登录窗口
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginWindow)
                {
                    window.Show();
                }

                if (window is RegisterWindow)
                {
                    window.Close();
                }
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            // 验证用户名
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "请输入用户名";
                ErrorVisibility = Visibility.Visible;
                return false;
            }

            // 验证邮箱格式
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "请输入邮箱地址";
                ErrorVisibility = Visibility.Visible;
                return false;
            }
            
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                ErrorMessage = "请输入有效的邮箱地址";
                ErrorVisibility = Visibility.Visible;
                return false;
            }

            // 验证密码
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "请输入密码";
                ErrorVisibility = Visibility.Visible;
                return false;
            }

            // 验证密码强度（示例：至少6个字符）
            if (Password.Length < 6)
            {
                ErrorMessage = "密码长度不能少于6个字符";
                ErrorVisibility = Visibility.Visible;
                return false;
            }

            // 验证确认密码
            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "请确认密码";
                ErrorVisibility = Visibility.Visible;
                return false;
            }

            // 验证两次输入的密码是否一致
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "两次输入的密码不一致";
                ErrorVisibility = Visibility.Visible;
                return false;
            }

            // 验证通过
            ErrorVisibility = Visibility.Collapsed;
            return true;
        }
    }
}