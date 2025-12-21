using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Login_Sample.Data;
using System.Windows;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 备件供货商视图模型
    /// </summary>
    public class SparePartsSupplierViewModel : INotifyPropertyChanged
    {
        #region 属性和字段

        private ApplicationDbContext _dbContext;

        /// <summary>
        /// 供应商ID
        /// </summary>
        private string _supplierId;
        public string SupplierId
        {
            get { return _supplierId; }
            set { SetProperty(ref _supplierId, value); }
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        private string _supplierName;
        public string SupplierName
        {
            get { return _supplierName; }
            set { SetProperty(ref _supplierName, value); }
        }

        /// <summary>
        /// 联系人
        /// </summary>
        private string _contactPerson;
        public string ContactPerson
        {
            get { return _contactPerson; }
            set { SetProperty(ref _contactPerson, value); }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        private string _contactPhone;
        public string ContactPhone
        {
            get { return _contactPhone; }
            set { SetProperty(ref _contactPhone, value); }
        }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        /// <summary>
        /// 地址
        /// </summary>
        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { SetProperty(ref _remarks, value); }
        }

        /// <summary>
        /// 供应商列表
        /// </summary>
        private ObservableCollection<SparePartsSupplier> _suppliers;
        public ObservableCollection<SparePartsSupplier> Suppliers
        {
            get { return _suppliers; }
            set { SetProperty(ref _suppliers, value); }
        }

        /// <summary>
        /// 选中的供应商
        /// </summary>
        private SparePartsSupplier _selectedSupplier;
        public SparePartsSupplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                if (SetProperty(ref _selectedSupplier, value))
                {
                    // 当选中供应商时，填充表单
                    if (value != null)
                    {
                        SupplierId = value.SupplierId;
                        SupplierName = value.SupplierName;
                        ContactPerson = value.ContactPerson;
                        ContactPhone = value.ContactPhone;
                        Email = value.Email;
                        Address = value.Address;
                        Remarks = value.Remarks;
                    }
                }
            }
        }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        private string _searchKeyword;
        public string SearchKeyword
        {
            get { return _searchKeyword; }
            set
            {
                if (SetProperty(ref _searchKeyword, value))
                {
                    // 搜索关键词变化时，过滤供应商列表
                    FilterSuppliers();
                }
            }
        }

        /// <summary>
        /// 所有供应商（用于搜索过滤）
        /// </summary>
        private List<SparePartsSupplier> _allSuppliers;

        #endregion

        #region 命令

        /// <summary>
        /// 新增命令
        /// </summary>
        public ICommand AddCommand { get; private set; }

        /// <summary>
        /// 保存命令
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// 修改命令
        /// </summary>
        public ICommand UpdateCommand { get; private set; }

        /// <summary>
        /// 删除命令
        /// </summary>
        public ICommand DeleteCommand { get; private set; }

        /// <summary>
        /// 搜索命令
        /// </summary>
        public ICommand SearchCommand { get; private set; }

        /// <summary>
        /// 重置命令
        /// </summary>
        public ICommand ResetCommand { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SparePartsSupplierViewModel()
        {
            _dbContext = new ApplicationDbContext();

            // 初始化命令
            AddCommand = new RelayCommand(AddSupplier);
            SaveCommand = new RelayCommand(SaveSupplier, CanSaveSupplier);
            UpdateCommand = new RelayCommand(UpdateSupplier, CanUpdateSupplier);
            DeleteCommand = new RelayCommand(DeleteSupplier, CanDeleteSupplier);
            SearchCommand = new RelayCommand(SearchSuppliers);
            ResetCommand = new RelayCommand(ResetForm);

            // 初始化数据
            InitializeData();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            // 加载供应商数据
            LoadSuppliers();
            
            // 初始化表单
            ResetForm(null);
        }

        /// <summary>
        /// 加载供应商数据
        /// </summary>
        private void LoadSuppliers()
        {
            try
            {
                // 从数据库加载供应商数据
                _allSuppliers = _dbContext.SparePartsSuppliers.ToList();
                
                // 如果没有数据，加载模拟数据
                if (!_allSuppliers.Any())
                {
                    LoadMockData();
                }
                
                // 更新供应商列表
                Suppliers = new ObservableCollection<SparePartsSupplier>(_allSuppliers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载供应商数据失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                
                // 加载模拟数据作为备选
                LoadMockData();
            }
        }

        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadMockData()
        {
            _allSuppliers = new List<SparePartsSupplier>
            {
                new SparePartsSupplier
                {
                    SupplierId = "SPL001",
                    SupplierName = "汽车配件供应商",
                    ContactPerson = "张三",
                    ContactPhone = "13800138001",
                    Email = "zhangsan@example.com",
                    Address = "北京市朝阳区建国路88号",
                    Remarks = "主要供应商之一，合作多年",
                    CreatedAt = DateTime.Now.AddDays(-30),
                    UpdatedAt = DateTime.Now.AddDays(-10)
                },
                new SparePartsSupplier
                {
                    SupplierId = "SPL002",
                    SupplierName = "汽车零部件有限公司",
                    ContactPerson = "李四",
                    ContactPhone = "13900139002",
                    Email = "lisi@example.com",
                    Address = "上海市浦东新区张江高科技园区",
                    Remarks = "提供高质量的发动机零部件",
                    CreatedAt = DateTime.Now.AddDays(-25),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new SparePartsSupplier
                {
                    SupplierId = "SPL003",
                    SupplierName = "汽车电器供应商",
                    ContactPerson = "王五",
                    ContactPhone = "13700137003",
                    Email = "wangwu@example.com",
                    Address = "广州市天河区珠江新城",
                    Remarks = "专注于汽车电器产品",
                    CreatedAt = DateTime.Now.AddDays(-20),
                    UpdatedAt = DateTime.Now.AddDays(-2)
                }
            };
            
            // 更新供应商列表
            Suppliers = new ObservableCollection<SparePartsSupplier>(_allSuppliers);
        }

        /// <summary>
        /// 新增供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        private void AddSupplier(object parameter)
        {
            ResetForm(null);
            
            // 自动生成供应商ID
            SupplierId = GenerateSupplierId();
            
            MessageBox.Show("请填写供应商信息并点击保存按钮", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 保存供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        private void SaveSupplier(object parameter)
        {
            try
            {
                // 创建新的供应商对象
                var newSupplier = new SparePartsSupplier
                {
                    SupplierId = SupplierId,
                    SupplierName = SupplierName,
                    ContactPerson = ContactPerson,
                    ContactPhone = ContactPhone,
                    Email = Email,
                    Address = Address,
                    Remarks = Remarks,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // 添加到数据库
                _dbContext.SparePartsSuppliers.Add(newSupplier);
                _dbContext.SaveChanges();

                // 更新供应商列表
                _allSuppliers.Add(newSupplier);
                Suppliers.Add(newSupplier);

                // 重置表单
                ResetForm(null);

                MessageBox.Show("供应商信息保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存供应商信息失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        private void UpdateSupplier(object parameter)
        {
            try
            {
                if (SelectedSupplier != null)
                {
                    // 更新供应商信息
                    SelectedSupplier.SupplierName = SupplierName;
                    SelectedSupplier.ContactPerson = ContactPerson;
                    SelectedSupplier.ContactPhone = ContactPhone;
                    SelectedSupplier.Email = Email;
                    SelectedSupplier.Address = Address;
                    SelectedSupplier.Remarks = Remarks;
                    SelectedSupplier.UpdatedAt = DateTime.Now;

                    // 更新数据库
                    _dbContext.SaveChanges();

                    // 更新供应商列表
                    var index = _allSuppliers.FindIndex(s => s.Id == SelectedSupplier.Id);
                    if (index >= 0)
                    {
                        _allSuppliers[index] = SelectedSupplier;
                    }

                    // 重置表单
                    ResetForm(null);

                    MessageBox.Show("供应商信息修改成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改供应商信息失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        private void DeleteSupplier(object parameter)
        {
            try
            {
                if (SelectedSupplier != null)
                {
                    // 确认删除
                    var result = MessageBox.Show($"确定要删除供应商 '{SelectedSupplier.SupplierName}' 吗？", "确认删除", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // 从数据库删除
                        _dbContext.SparePartsSuppliers.Remove(SelectedSupplier);
                        _dbContext.SaveChanges();

                        // 从列表中删除
                        _allSuppliers.Remove(SelectedSupplier);
                        Suppliers.Remove(SelectedSupplier);

                        // 重置表单
                        ResetForm(null);

                        MessageBox.Show("供应商信息删除成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除供应商信息失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 搜索供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        private void SearchSuppliers(object parameter)
        {
            FilterSuppliers();
        }

        /// <summary>
        /// 过滤供应商列表
        /// </summary>
        private void FilterSuppliers()
        {
            if (string.IsNullOrWhiteSpace(SearchKeyword))
            {
                Suppliers = new ObservableCollection<SparePartsSupplier>(_allSuppliers);
            }
            else
            {
                var filteredSuppliers = _allSuppliers.Where(s =>
                    s.SupplierId.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) ||
                    s.SupplierName.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) ||
                    s.ContactPerson.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) ||
                    s.ContactPhone.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Suppliers = new ObservableCollection<SparePartsSupplier>(filteredSuppliers);
            }
        }

        /// <summary>
        /// 重置表单
        /// </summary>
        /// <param name="parameter">参数</param>
        private void ResetForm(object parameter)
        {
            SelectedSupplier = null;
            SupplierId = string.Empty;
            SupplierName = string.Empty;
            ContactPerson = string.Empty;
            ContactPhone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            Remarks = string.Empty;
            SearchKeyword = string.Empty;
        }

        /// <summary>
        /// 自动生成供应商ID
        /// </summary>
        /// <returns>供应商ID</returns>
        private string GenerateSupplierId()
        {
            // 获取最大的供应商ID
            var maxId = _allSuppliers.Max(s => int.TryParse(s.SupplierId.Substring(3), out int id) ? id : 0);
            return "SPL" + (maxId + 1).ToString("D3");
        }

        /// <summary>
        /// 是否可以保存供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可以保存</returns>
        private bool CanSaveSupplier(object parameter)
        {
            return !string.IsNullOrWhiteSpace(SupplierId) &&
                   !string.IsNullOrWhiteSpace(SupplierName) &&
                   !string.IsNullOrWhiteSpace(ContactPhone);
        }

        /// <summary>
        /// 是否可以修改供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可以修改</returns>
        private bool CanUpdateSupplier(object parameter)
        {
            return SelectedSupplier != null &&
                   !string.IsNullOrWhiteSpace(SupplierName) &&
                   !string.IsNullOrWhiteSpace(ContactPhone);
        }

        /// <summary>
        /// 是否可以删除供应商
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可以删除</returns>
        private bool CanDeleteSupplier(object parameter)
        {
            return SelectedSupplier != null;
        }

        #endregion

        #region INotifyPropertyChanged 实现

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}