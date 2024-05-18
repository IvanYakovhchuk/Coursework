namespace КР
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}