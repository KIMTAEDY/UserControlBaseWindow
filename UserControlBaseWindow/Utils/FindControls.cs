using System.Windows;

namespace UserControlBaseWindow.Utils
{
    internal static class FindControls
    {
        #region [Method~] FindVisualParent
        internal static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                    return correctlyTyped;

                parent = System.Windows.Media.VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
        #endregion
    }
}
