using Company.WPF.ViewModels;

namespace Company.WPF.Views;

public partial class ContractorEditWindow
{
    public ContractorEditWindow(
        ContractorEditViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
