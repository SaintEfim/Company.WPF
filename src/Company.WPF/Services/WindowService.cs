using System.Windows;

namespace Company.WPF.Services;

public interface IWindowService
{
    bool? ShowDialog<TViewModel>(
        TViewModel viewModel,
        Window window)
        where TViewModel : class;
}

public class WindowService : IWindowService
{
    public bool? ShowDialog<TViewModel>(
        TViewModel viewModel,
        Window window)
        where TViewModel : class
    {
        window.DataContext = viewModel;

        window.Owner = Application.Current.MainWindow;

        return Application.Current.Dispatcher.CheckAccess()
            ? window.ShowDialog()
            : Application.Current.Dispatcher.Invoke(window.ShowDialog);
    }
}
