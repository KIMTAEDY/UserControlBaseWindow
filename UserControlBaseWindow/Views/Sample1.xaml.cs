using System;
using System.Windows;

namespace UserControlBaseWindow.Views
{
    /// <summary>
    /// Sample1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Sample1 : UserControlBase
    {
        public Sample1()
        {
            InitializeComponent();
        }

        protected internal override void OnParentWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close?", string.Empty, MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
                e.Cancel = true;

            base.OnParentWindowClosing(sender, e);
        }

        protected internal override void OnParentWindowClosed(object sender, EventArgs e)
        {
            base.OnParentWindowClosed(sender, e);
        }
    }
}
