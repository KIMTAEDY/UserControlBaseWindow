using System.Windows.Input;

namespace UserControlBaseWindow.ViewModels
{
    using Bases;
    using Views;

    public class Sample2ViewModel : ObservableBase
    {
        private int countShowSample3 = 0;

        public bool? TemplateSelector
        {
            get => templateSelector;
            set => Set(() => TemplateSelector, ref templateSelector, value);
        }
        private bool? templateSelector;

        public ICommand OKCommand => new RelayCommand<object>((@object) =>
        {
            if (!(@object is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBase.UserControlBaseWindow parentWindow))
                return;

            parentWindow.DialogResult = true;
            parentWindow.Close();

            TemplateSelector = null;
        });

        public ICommand ShowSample3Command => new RelayCommand<object>((@object) =>
        {
            new Sample3()
            {
                CanCloseEscapeKey = true,
                CaptionHeight = 35.0,
                ShowInTaskbar = true,
                ResizeMode = System.Windows.ResizeMode.CanMinimize,
                Owner = @object as UserControlBase,
                Title = $"Sample 3 : {++countShowSample3}",
            }.Show();
        });

        public Sample2ViewModel()
        {
        }

        ~Sample2ViewModel()
        {
        }
    }
}
