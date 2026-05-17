using System.Collections.ObjectModel;
using System.Windows.Input;
using Company.Domain.Models;
using Company.Domain.Services.Contractor;
using Company.Domain.Services.Employee;
using Company.Domain.Services.Order;
using Company.WPF.Services;
using Company.WPF.ViewModels.Base;
using Company.WPF.ViewModels.Commands;
using Company.WPF.Views;

namespace Company.WPF.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IEmployeeManager _employeeManager;
    private readonly IEmployeeProvider _employeeProvider;
    private readonly IContractorManager _contractorManager;
    private readonly IContractorProvider _contractorProvider;
    private readonly IOrderManager _orderManager;
    private readonly IOrderProvider _orderProvider;
    private readonly IWindowService _windowService;

    private ObservableCollection<EmployeeModel> _employees = new();

    public ObservableCollection<EmployeeModel> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<ContractorModel> _contractors = new();

    public ObservableCollection<ContractorModel> Contractors
    {
        get => _contractors;
        set
        {
            _contractors = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<OrderModel> _orders = new();

    public ObservableCollection<OrderModel> Orders
    {
        get => _orders;
        set
        {
            _orders = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddEmployeeCommand { get; }
    public ICommand EditEmployeeCommand { get; }
    public ICommand DeleteEmployeeCommand { get; }

    public ICommand AddContractorCommand { get; }
    public ICommand EditContractorCommand { get; }
    public ICommand DeleteContractorCommand { get; }

    public ICommand AddOrderCommand { get; }
    public ICommand EditOrderCommand { get; }
    public ICommand DeleteOrderCommand { get; }

    public MainViewModel(
        IEmployeeManager employeeManager,
        IEmployeeProvider employeeProvider,
        IContractorManager contractorManager,
        IContractorProvider contractorProvider,
        IOrderManager orderManager,
        IOrderProvider orderProvider,
        IWindowService windowService)
    {
        _employeeManager = employeeManager;
        _employeeProvider = employeeProvider;
        _contractorManager = contractorManager;
        _contractorProvider = contractorProvider;
        _orderManager = orderManager;
        _orderProvider = orderProvider;
        _windowService = windowService;

        AddEmployeeCommand = new AsyncRelayCommand(_ => OpenEmployeeEdit());
        EditEmployeeCommand = new AsyncRelayCommand(p => OpenEmployeeEdit(p as EmployeeModel), p => p is EmployeeModel);
        DeleteEmployeeCommand = new AsyncRelayCommand(p => DeleteEmployee(p as EmployeeModel), p => p is EmployeeModel);

        AddContractorCommand = new AsyncRelayCommand(_ => OpenContractorEdit());
        EditContractorCommand =
            new AsyncRelayCommand(p => OpenContractorEdit(p as ContractorModel), p => p is ContractorModel);
        DeleteContractorCommand =
            new AsyncRelayCommand(p => DeleteContractor(p as ContractorModel), p => p is ContractorModel);

        AddOrderCommand = new AsyncRelayCommand(_ => OpenOrderEdit());
        EditOrderCommand = new AsyncRelayCommand(p => OpenOrderEdit(p as OrderModel), p => p is OrderModel);
        DeleteOrderCommand = new AsyncRelayCommand(p => DeleteOrder(p as OrderModel), p => p is OrderModel);

        // Асинхронная загрузка данных
        _ = LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        await ReloadEmployees();
        await ReloadContractors();
        await ReloadOrders();
    }

    private async Task OpenEmployeeEdit(
        EmployeeModel? employee = null)
    {
        var vm = new EmployeeEditViewModel(_employeeManager, employee);
        var window = new EmployeeEditWindow(vm);

        if (_windowService.ShowDialog(vm, window) == true)
        {
            await ReloadEmployees();
        }
    }

    private async Task DeleteEmployee(
        EmployeeModel? employee)
    {
        if (employee is null) return;

        await _employeeManager.Delete(employee.Id);
        await ReloadEmployees();
    }

    private async Task ReloadEmployees()
    {
        var employees = await _employeeProvider.Get();
        Employees.Clear();
        foreach (var employee in employees)
        {
            Employees.Add(employee);
        }
    }

    private async Task OpenContractorEdit(
        ContractorModel? contractor = null)
    {
        var vm = new ContractorEditViewModel(_contractorManager, _employeeProvider, contractor);
        var window = new ContractorEditWindow(vm);
        if (_windowService.ShowDialog(vm, window) == true)
        {
            await ReloadContractors(); // ← было ReloadEmployees
        }
    }

    private async Task DeleteContractor(
        ContractorModel? contractor)
    {
        if (contractor is null) return;

        await _contractorManager.Delete(contractor.Id);
        await ReloadContractors();
    }

    private async Task ReloadContractors()
    {
        var contractors = await _contractorProvider.Get();
        Contractors.Clear();
        foreach (var contractor in contractors)
        {
            Contractors.Add(contractor);
        }
    }

    private async Task OpenOrderEdit(
        OrderModel? order = null)
    {
        var vm = new OrderEditViewModel(_orderManager, _employeeProvider, _contractorProvider, order);
        var window = new OrderEditWindow(vm);
        if (_windowService.ShowDialog(vm, window) == true)
        {
            await ReloadOrders(); // ← было ReloadEmployees
        }
    }

    private async Task DeleteOrder(
        OrderModel? order)
    {
        if (order is null) return;

        await _orderManager.Delete(order.Id);
        await ReloadOrders();
    }

    private async Task ReloadOrders()
    {
        var orders = await _orderProvider.Get();
        Orders.Clear();
        foreach (var order in orders)
        {
            Orders.Add(order);
        }
    }
}
