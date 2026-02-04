using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Login_Sample.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DataCustomer = Login_Sample.Data.Customer;
using DataCustomerVehicle = Login_Sample.Data.CustomerVehicle;
using DataTechnician = Login_Sample.Data.Technician;

namespace Login_Sample.ViewModels
{
    public class AppointmentManagementViewModel : ObservableObject
    {
        private readonly ApplicationDbContext _dbContext;
        
        // 预约列表
        private ObservableCollection<CustomerAppointment> _appointments;
        public ObservableCollection<CustomerAppointment> Appointments
        {
            get { return _appointments; }
            set { _appointments = value; OnPropertyChanged(nameof(Appointments)); }
        }
        
        // 维修技师列表
        private ObservableCollection<DataTechnician> _technicians;
        public ObservableCollection<DataTechnician> Technicians
        {
            get { return _technicians; }
            set { _technicians = value; OnPropertyChanged(nameof(Technicians)); }
        }
        
        // 客户列表
        private ObservableCollection<DataCustomer> _customers;
        public ObservableCollection<DataCustomer> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged(nameof(Customers)); }
        }
        
        // 客户车辆列表
        private ObservableCollection<DataCustomerVehicle> _customerVehicles;
        public ObservableCollection<DataCustomerVehicle> CustomerVehicles
        {
            get { return _customerVehicles; }
            set { _customerVehicles = value; OnPropertyChanged(nameof(CustomerVehicles)); }
        }
        
        // 选中的客户
        private DataCustomer _selectedCustomer;
        public DataCustomer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set 
            { 
                _selectedCustomer = value; 
                OnPropertyChanged(nameof(SelectedCustomer));
                // 当选择客户时，更新可用车辆列表
                if (value != null)
                {
                    CustomerVehicles = new ObservableCollection<DataCustomerVehicle>(_dbContext.CustomerVehicles.Where(v => v.CustomerId == value.Id));
                }
            }
        }
        
        // 选中的车辆
        private DataCustomerVehicle _selectedVehicle;
        public DataCustomerVehicle SelectedVehicle
        {
            get { return _selectedVehicle; }
            set { _selectedVehicle = value; OnPropertyChanged(nameof(SelectedVehicle)); }
        }
        
        // 选中的技师
        private DataTechnician _selectedTechnician;
        public DataTechnician SelectedTechnician
        {
            get { return _selectedTechnician; }
            set { _selectedTechnician = value; OnPropertyChanged(nameof(SelectedTechnician)); }
        }
        
        // 预约类型
        private string _appointmentType;
        public string AppointmentType
        {
            get { return _appointmentType; }
            set { _appointmentType = value; OnPropertyChanged(nameof(AppointmentType)); }
        }
        
        // 预约日期
        private DateTime _appointmentDate;
        public DateTime AppointmentDate
        {
            get { return _appointmentDate; }
            set { _appointmentDate = value; OnPropertyChanged(nameof(AppointmentDate)); }
        }
        
        // 预约时间
        private string _appointmentTime;
        public string AppointmentTime
        {
            get { return _appointmentTime; }
            set { _appointmentTime = value; OnPropertyChanged(nameof(AppointmentTime)); }
        }
        
        // 服务内容
        private string _serviceContent;
        public string ServiceContent
        {
            get { return _serviceContent; }
            set { _serviceContent = value; OnPropertyChanged(nameof(ServiceContent)); }
        }
        
        // 备注
        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; OnPropertyChanged(nameof(Remarks)); }
        }
        
        // 预约时间选项
        public List<string> TimeOptions { get; set; }
        
        // 预约类型选项
        public List<string> AppointmentTypeOptions { get; set; }
        
        // 命令
        public ICommand AddAppointmentCommand { get; set; }
        public ICommand UpdateAppointmentCommand { get; set; }
        public ICommand DeleteAppointmentCommand { get; set; }
        public ICommand LoadDataCommand { get; set; }
        
        public AppointmentManagementViewModel()
        {
            _dbContext = new ApplicationDbContext();
            
            // 初始化日期为今天
            AppointmentDate = DateTime.Today;
            
            // 初始化时间选项（8:00 - 17:00，每小时一个时间段）
            TimeOptions = Enumerable.Range(8, 10).Select(hour => $"{hour}:00-{hour+1}:00").ToList();
            
            // 初始化预约类型选项
            AppointmentTypeOptions = new List<string> { "常规保养", "故障维修", "轮胎更换", "机油更换", "汽车检查", "其他" };
            
            // 初始化命令
            AddAppointmentCommand = new RelayCommand<object>(AddAppointment);
            UpdateAppointmentCommand = new RelayCommand<object>(UpdateAppointment);
            DeleteAppointmentCommand = new RelayCommand<object>(DeleteAppointment);
            LoadDataCommand = new RelayCommand<object>(LoadData);
            
            // 加载数据
            LoadData(null);
        }
        
        // 加载数据
        private void LoadData(object? parameter)
        {
            // 加载所有预约
            Appointments = new ObservableCollection<CustomerAppointment>(_dbContext.CustomerAppointments.Include(a => a.Customer).Include(a => a.Vehicle).Include(a => a.Technician));
            
            // 加载所有技师
            Technicians = new ObservableCollection<DataTechnician>(_dbContext.Technicians);
            
            // 加载所有客户
            Customers = new ObservableCollection<DataCustomer>(_dbContext.Customers);
            
            // 加载所有车辆
            CustomerVehicles = new ObservableCollection<DataCustomerVehicle>(_dbContext.CustomerVehicles);
        }
        
        // 添加预约
        private void AddAppointment(object parameter)
        {
            try
            {
                if (SelectedCustomer == null || SelectedVehicle == null)
                {
                    // 显示错误消息
                    return;
                }
                
                // 检查所选技师在所选时间是否可用
                if (SelectedTechnician != null)
                {
                    var isTechnicianAvailable = !_dbContext.CustomerAppointments.Any(a => 
                        a.TechnicianId == SelectedTechnician.Id && 
                        a.AppointmentDate == AppointmentDate && 
                        a.AppointmentTime == AppointmentTime && 
                        a.Status != "已取消");
                        
                    if (!isTechnicianAvailable)
                    {
                        // 显示错误消息：该技师在所选时间不可用
                        return;
                    }
                }
                
                var newAppointment = new CustomerAppointment
                {
                    CustomerId = SelectedCustomer.Id,
                    VehicleId = SelectedVehicle.Id,
                    TechnicianId = SelectedTechnician?.Id,
                    AppointmentType = AppointmentType,
                    AppointmentDate = AppointmentDate,
                    AppointmentTime = AppointmentTime,
                    ServiceContent = ServiceContent,
                    Status = "已确认",
                    Remarks = Remarks,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                
                _dbContext.CustomerAppointments.Add(newAppointment);
                _dbContext.SaveChanges();
                
                // 更新预约列表
                Appointments.Add(newAppointment);
                
                // 重置表单
                ResetForm();
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.Message);
            }
        }
        
        // 更新预约
        private void UpdateAppointment(object parameter)
        {
            try
            {
                if (parameter is CustomerAppointment appointment)
                {
                    appointment.UpdatedAt = DateTime.UtcNow;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.Message);
            }
        }
        
        // 删除预约
        private void DeleteAppointment(object parameter)
        {
            try
            {
                if (parameter is CustomerAppointment appointment)
                {
                    _dbContext.CustomerAppointments.Remove(appointment);
                    _dbContext.SaveChanges();
                    Appointments.Remove(appointment);
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.Message);
            }
        }
        
        // 重置表单
        private void ResetForm()
        {
            SelectedCustomer = null;
            SelectedVehicle = null;
            SelectedTechnician = null;
            AppointmentType = string.Empty;
            AppointmentDate = DateTime.Today;
            AppointmentTime = string.Empty;
            ServiceContent = string.Empty;
            Remarks = string.Empty;
        }
        
        // 获取指定日期和技师的可用时间段
        public List<string> GetAvailableTimeSlots(DateTime date, DataTechnician technician)
        {
            // 获取该日期和技师的所有已预约时间段
            var bookedTimeSlots = _dbContext.CustomerAppointments
                .Where(a => a.AppointmentDate == date && a.TechnicianId == technician.Id && a.Status != "已取消")
                .Select(a => a.AppointmentTime)
                .ToList();
            
            // 返回未预约的时间段
            return TimeOptions.Except(bookedTimeSlots).ToList();
        }
        
        // INotifyPropertyChanged 实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}