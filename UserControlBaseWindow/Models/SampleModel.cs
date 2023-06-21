namespace UserControlBaseWindow.Models
{
    using Bases;
    using System;

    public class SampleModel : ObservableBase
    {
        public string Name
        {
            get => name;
            set => Set(() => Name, ref name, value);
        }
        private string name;

        public int Age
        {
            get => age;
            set => Set(() => Age, ref age, value);
        }
        private int age;

        private SampleModel(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public static SampleModel CreateInstance(string name, int age)
        {
            return new SampleModel(name, age);
        }
    }
}
