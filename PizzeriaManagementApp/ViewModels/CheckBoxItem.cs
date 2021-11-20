using System;

namespace PizzeriaManagementApp.ViewModels
{
    public class CheckBoxItem<T>
    {
        public T Id { get; set; }
        public string IdString { get; set; }
        public object Object { get; set; }
        public bool IsChecked { get; set; }
    }
}
