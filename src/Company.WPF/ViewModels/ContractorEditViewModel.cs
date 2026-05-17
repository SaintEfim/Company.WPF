using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Company.Domain.Models;
using Company.Domain.Services.Contractor;
using Company.Domain.Services.Employee;
using Company.WPF.ViewModels.Base;
using Company.WPF.ViewModels.Commands;

namespace Company.WPF.ViewModels;

public class ContractorEditViewModel : BaseViewModel
{
    private readonly IContractorManager _contractorManager;
    private readonly ContractorModel? _originalContractor;
    private readonly AsyncRelayCommand _saveCommand;

    public Guid Id { get; private set; }

    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
            _saveCommand.RaiseCanExecuteChanged();
        }
    }

    private string _inn = string.Empty;

    public string Inn
    {
        get => _inn;
        set
        {
            _inn = value;
            OnPropertyChanged();
            _saveCommand.RaiseCanExecuteChanged();
        }
    }

    private EmployeeModel _selectedCurator = null!;

    public EmployeeModel SelectedCurator
    {
        get => _selectedCurator;
        set
        {
            _selectedCurator = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<EmployeeModel> AvailableEmployees { get; }

    public ICommand SaveCommand => _saveCommand;
    public ICommand CancelCommand { get; }

    public ContractorEditViewModel(
        IContractorManager contractorManager,
        IEmployeeProvider employeeProvider,
        ContractorModel? contractor = null)
    {
        _contractorManager = contractorManager;
        _originalContractor = contractor;

        AvailableEmployees = [];

        _saveCommand = new AsyncRelayCommand(_ => SaveAsync(), _ => CanSave());
        CancelCommand = new RelayCommand(_ => CloseWindow(false));

        if (contractor is not null)
        {
            Id = contractor.Id;
            Name = contractor.Name;
            Inn = contractor.INN;
        }

        _ = InitializeAsync(employeeProvider, contractor);
    }

    private async Task InitializeAsync(
        IEmployeeProvider employeeProvider,
        ContractorModel? contractor)
    {
        var employees = await employeeProvider.Get();

        AvailableEmployees.Clear();
        foreach (var e in employees) AvailableEmployees.Add(e);

        if (contractor is not null)
        {
            SelectedCurator = AvailableEmployees.Single(e => e.Id == contractor.Curator.Id);
        }
    }

    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Inn);
    }

    private async Task SaveAsync()
    {
        if (!CanSave()) return;

        if (_originalContractor is null)
        {
            var contractor = new ContractorModel
            {
                Id = Guid.NewGuid(),
                Name = Name,
                INN = Inn,
                Curator = SelectedCurator
            };

            await _contractorManager.Create(contractor);
        }
        else
        {
            _originalContractor.Name = Name;
            _originalContractor.INN = Inn;
            _originalContractor.Curator = SelectedCurator;

            await _contractorManager.Update(_originalContractor);
        }

        CloseWindow(true);
    }

    private static void CloseWindow(
        bool result)
    {
        if (Application.Current
                .Windows
                .OfType<Views.ContractorEditWindow>()
                .FirstOrDefault() is not Window window)
        {
            return;
        }

        window.DialogResult = result;
        window.Close();
    }
}
