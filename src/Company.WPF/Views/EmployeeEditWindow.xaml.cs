using Company.WPF.ViewModels;

namespace Company.WPF.Views;

public partial class EmployeeEditWindow
{
    public EmployeeEditWindow(
        EmployeeEditViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
