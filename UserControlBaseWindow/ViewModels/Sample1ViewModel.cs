using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserControlBaseWindow.ViewModels
{
    using Bases;
    using Models;

    public class Sample1ViewModel : ObservableBase
    {
        public ObservableCollection<SampleModel> Samples
        {
            get => samples;
            set => Set(() => Samples, ref samples, value);
        }
        private ObservableCollection<SampleModel> samples;

        public System.Windows.Input.ICommand ShowDialogSample2Command => new RelayCommand<object>((@object) =>
        {
            var dialogResult = Views.ViewLocator.Sample2.ShowDialog();
            System.Windows.MessageBox.Show($"DialogResult = {(dialogResult.HasValue && dialogResult.Value ? "true" : "false")}");
        });

        public Sample1ViewModel()
        {
            var samples = new List<SampleModel>()
            {
                SampleModel.CreateInstance("Bart", 10),
                SampleModel.CreateInstance("Lisa", 8),
                SampleModel.CreateInstance("Maggie", 1)
            };

            Samples = new ObservableCollection<SampleModel>(samples);
        }
    }
}
