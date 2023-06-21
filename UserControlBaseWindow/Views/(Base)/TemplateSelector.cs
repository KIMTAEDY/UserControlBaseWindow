using System.Windows;

namespace UserControlBaseWindow.Views
{
    public class TemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public DataTemplate Template1 { get; set; }

        public DataTemplate Template2 { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is bool selector))
                return null;

            return selector ? Template1 : Template2;
        }
    }
}
