namespace UserControlBaseWindow.ViewModels
{
    public class Sample1ViewModel : ViewModelBase
    {
        public System.Windows.Input.ICommand ShowDialogSample2Command => new RelayCommand<object>((@object) =>
        {
            var dialogResult = Views.ViewLocator.Sample2.ShowDialog();
            System.Windows.MessageBox.Show($"DialogResult = {(dialogResult.HasValue && dialogResult.Value ? "true" : "false")}");
        });
    }
}
