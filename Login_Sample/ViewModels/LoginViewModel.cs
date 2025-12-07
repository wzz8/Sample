using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace test.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool _isFactoryNoEnabled = true;
        private string _factoryNo = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private Visibility _factoryNoVisibility = Visibility.Visible;
        private double _windowHeight = 320;
        private double _windowWidth = 380;

        /// <summary>
        /// 是否启用分厂号输入
        /// </summary>
        public bool IsFactoryNoEnabled
        {
            get { return _isFactoryNoEnabled; }
            set 
            {
                if (_isFactoryNoEnabled != value)
                {
                    _isFactoryNoEnabled = value;
                    OnPropertyChanged();
                    UpdateUIState();
                }
            }
        }

        /// <summary>
        /// 分厂号
        /// </summary>
        public string FactoryNo
        {
            get { return _factoryNo; }
            set 
            {
                if (_factoryNo != value)
                {
                    _factoryNo = value;
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
        public Visibility FactoryNoVisibility
        {
            get { return _factoryNoVisibility; }
            set 
            {
                if (_factoryNoVisibility != value)
                {
                    _factoryNoVisibility = value;
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

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
            CloseCommand = new RelayCommand(Close);
            PasswordChangedCommand = new RelayCommand(HandlePasswordChanged);
            IsFactoryNoEnabled = false;
            
            UpdateUIState();
        }
        
        /// <summary>
        /// 处理密码变更的方法
        /// </summary>
        /// <param name="password">密码字符串</param>
        private void HandlePasswordChanged(object password)
        {
            if (password is string passwordStr)
            {
                Password = passwordStr;
            }
        }
        
        /// <summary>
        /// 登录方法（更新为接受object参数）
        /// </summary>
        private void Login(object parameter)
        {
            // 原有登录逻辑不变
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "用户名和密码不能为空";
                ErrorVisibility = Visibility.Visible;
                return;
            }
            
            // 模拟登录验证
            if (Username == "admin" && Password == "123456")
            {
                ErrorVisibility = Visibility.Hidden;
                // 登录成功后的操作
            }
            else
            {
                ErrorMessage = "用户名或密码错误";
                ErrorVisibility = Visibility.Visible;
            }
        }
        
        /// <summary>
        /// 注册方法（更新为接受object参数）
        /// </summary>
        private void Register(object parameter)
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
        private void Close(object parameter)
        {
            // 原有关闭逻辑不变
        }

        /// <summary>
        /// 更新UI状态
        /// </summary>
        private void UpdateUIState()
        {
            if (IsFactoryNoEnabled)
            {
                FactoryNoVisibility = Visibility.Visible;
                WindowHeight = 320;
                WindowWidth = 380;
            }
            else
            {
                FactoryNoVisibility = Visibility.Collapsed;
                WindowHeight = 270;
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

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}