using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Company.Domain.Models;
using Company.Domain.Services.Contractor;
using Company.Domain.Services.Employee;
using Company.Domain.Services.Order;
using Company.WPF.ViewModels.Base;
using Company.WPF.ViewModels.Commands;

namespace Company.WPF.ViewModels;

public class OrderEditViewModel : BaseViewModel
{
    private readonly IOrderManager _orderManager;
    private readonly OrderModel? _originalOrder;

    public Guid Id { get; private set; }

    private DateTime _date;

    public DateTime Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    private decimal _amount;

    public decimal Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            OnPropertyChanged();
        }
    }

    private EmployeeModel _selectedEmployee;

    public EmployeeModel SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            _selectedEmployee = value;
            OnPropertyChanged();
        }
    }

    private ContractorModel _selectedContractor;

    public ContractorModel SelectedContractor
    {
        get => _selectedContractor;
        set
        {
            _selectedContractor = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<EmployeeModel> AvailableEmployees { get; }
    public ObservableCollection<ContractorModel> AvailableContractors { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public OrderEditViewModel(
        IOrderManager orderManager,
        IEmployeeProvider employeeProvider,
        IContractorProvider contractorProvider,
        OrderModel? order = null)
    {
        _orderManager = orderManager;
        _originalOrder = order;

        AvailableEmployees = [];
        AvailableContractors = [];

        SaveCommand = new AsyncRelayCommand(_ => SaveAsync());
        CancelCommand = new RelayCommand(_ => CloseWindow(false));

        Date = order?.Date ?? DateTime.Today;
        Amount = order?.Amount ?? 0;

        Id = order?.Id ?? Guid.Empty;

        // async загрузка данных отдельно
        _ = InitializeAsync(employeeProvider, contractorProvider, order);
    }

    private async Task InitializeAsync(
        IEmployeeProvider employeeProvider,
        IContractorProvider contractorProvider,
        OrderModel? order)
    {
        var employees = await employeeProvider.Get();
        var contractors = await contractorProvider.Get();

        AvailableEmployees.Clear();
        foreach (var e in employees) AvailableEmployees.Add(e);

        AvailableContractors.Clear();
        foreach (var c in contractors) AvailableContractors.Add(c);

        if (order is not null)
        {
            SelectedEmployee = AvailableEmployees.First(e => e.Id == order.Employee.Id);

            SelectedContractor = AvailableContractors.First(c => c.Id == order.Contractor.Id);
        }
    }

    private async Task SaveAsync()
    {
        if (_originalOrder is null)
        {
            var order = new OrderModel
            {
                Date = Date,
                Amount = Amount,
                Employee = SelectedEmployee,
                Contractor = SelectedContractor
            };

            await _orderManager.Create(order);
        }
        else
        {
            _originalOrder.Date = Date;
            _originalOrder.Amount = Amount;
            _originalOrder.Employee = SelectedEmployee;
            _originalOrder.Contractor = SelectedContractor;

            await _orderManager.Update(_originalOrder);
        }

        CloseWindow(true);
    }

    private static void CloseWindow(
        bool result)
    {
        if (Application.Current
                .Windows
                .OfType<Views.OrderEditWindow>()
                .FirstOrDefault() is not Window window)
        {
            return;
        }

        window.DialogResult = result;
        window.Close();
    }
}
