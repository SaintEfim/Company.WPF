using Company.WPF.ViewModels;

namespace Company.WPF.Views;

public partial class OrderEditWindow
{
    public OrderEditWindow(
        OrderEditViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
