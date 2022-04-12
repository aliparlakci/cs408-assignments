namespace cs408_hw1_server
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var db = new Database();
            var logger = new Logger();
            var server = new Server(db, logger);

            ApplicationConfiguration.Initialize();
            Application.Run(new ServerForm(server, logger));
        }
    }
}