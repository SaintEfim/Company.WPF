namespace Company.WPF.ViewModels.Base;

public interface IRequestClose
{
    event EventHandler<RequestCloseEventArgs> RequestClose;
}

public class RequestCloseEventArgs : EventArgs
{
    public bool DialogResult { get; }

    public RequestCloseEventArgs(
        bool dialogResult)
    {
        DialogResult = dialogResult;
    }
}
