namespace UserControlBaseWindow.Views
{
    /// <summary>
    /// Regist single instance views.
    /// </summary>
    public static class ViewLocator
    {
        public static Sample1 Sample1 { get; private set; }

        public static Sample2 Sample2 { get; private set; }

        static ViewLocator()
        {
            Sample1 = new Sample1();
            Sample2 = new Sample2();
        }
    }
}
