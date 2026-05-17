using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Company.Domain.Models;
using Company.Domain.Services.Employee;
using Company.WPF.ViewModels.Base;
using Company.WPF.ViewModels.Commands;

namespace Company.WPF.ViewModels;

public class EmployeeEditViewModel : BaseViewModel
{
    private readonly IEmployeeManager _employeeManager;
    private readonly EmployeeModel? _originalEmployee;
    private readonly AsyncRelayCommand _saveCommand;

    public Guid Id { get; private set; }

    private string _fullName = string.Empty;

    public string FullName
    {
        get => _fullName;
        set
        {
            _fullName = value;
            OnPropertyChanged();
            _saveCommand.RaiseCanExecuteChanged();
        }
    }

    private Position _position;

    public Position Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPropertyChanged();
        }
    }

    private DateTime _dateOfBirth;

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Position> AvailablePositions { get; }

    public ICommand SaveCommand => _saveCommand;
    public ICommand CancelCommand { get; }

    public EmployeeEditViewModel(
        IEmployeeManager employeeManager,
        EmployeeModel? employee = null)
    {
        _employeeManager = employeeManager;
        _originalEmployee = employee;

        AvailablePositions = new ObservableCollection<Position>(Enum.GetValues<Position>());

        _saveCommand = new AsyncRelayCommand(_ => SaveAsync(), _ => CanSave());
        CancelCommand = new RelayCommand(_ => CloseWindow(false));

        if (employee is not null)
        {
            Id = employee.Id;
            FullName = employee.FullName;
            Position = employee.Position;
            DateOfBirth = employee.DateOfBirth;
        }
        else
        {
            DateOfBirth = DateTime.Now.AddYears(-20);
            Position = Position.Worker;
        }
    }

    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(FullName);
    }

    private async Task SaveAsync()
    {
        if (!CanSave()) return;

        if (_originalEmployee is null)
        {
            var employee = new EmployeeModel
            {
                Id = Guid.NewGuid(),
                FullName = FullName,
                Position = Position,
                DateOfBirth = DateOfBirth
            };

            await _employeeManager.Create(employee);
        }
        else
        {
            _originalEmployee.FullName = FullName;
            _originalEmployee.Position = Position;
            _originalEmployee.DateOfBirth = DateOfBirth;

            await _employeeManager.Update(_originalEmployee);
        }

        CloseWindow(true);
    }

    private static void CloseWindow(
        bool result)
    {
        if (Application.Current
                .Windows
                .OfType<Views.EmployeeEditWindow>()
                .FirstOrDefault() is not Window window)
        {
            return;
        }

        window.DialogResult = result;
        window.Close();
    }
}
