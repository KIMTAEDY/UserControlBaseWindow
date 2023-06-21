using System;
using System.Linq.Expressions;
using System.ComponentModel;

namespace UserControlBaseWindow.Bases
{
    public class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        protected virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null || !(GetPropertyName(propertyExpression) is string property) || string.IsNullOrEmpty(property))
                return;

            RaisePropertyChanged(property);
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException($"{nameof(propertyExpression)} is null.");

            return ((((propertyExpression.Body as MemberExpression) ?? 
                throw new ArgumentException("Invalid argument", $"{nameof(propertyExpression)}")).Member as System.Reflection.PropertyInfo) ??
                throw new ArgumentException("Argument is not a property", $"{nameof(propertyExpression)}")).Name;
        }
    }
}
