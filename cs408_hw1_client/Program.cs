namespace cs408_hw1_client
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var logger = new Logger();
            var client = new Client(logger);

            ApplicationConfiguration.Initialize();
            var clientForm = new ClientForm(client, logger);
            Application.Run(clientForm);
        }
    }
}