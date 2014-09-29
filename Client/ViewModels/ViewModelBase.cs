using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Client.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool m_isLoading;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoading
        {
            get { return m_isLoading; }
            set { SetProperty(ref m_isLoading, value, () => IsLoading); }
        }

        protected void SetProperty<T>(ref T property, T value, Expression<Func<T>> propertyDelegate)
        {
            if (Equals(property, value))
            {
                return;
            }

            property = value;
            OnPropertyChanged(propertyDelegate);
        }

        private static string GetPropertyNameFromExpression<T>(Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression)property;
            MemberExpression memberExpression;
            var body = lambda.Body as UnaryExpression;
            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null || String.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            OnPropertyChanged(GetPropertyNameFromExpression(property));
        }
    }
}
