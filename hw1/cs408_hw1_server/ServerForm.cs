namespace cs408_hw1_server
{
    public partial class ServerForm : Form
    {
        private delegate void SafeCallDelegateText(string text);

        private Server _server;
        private Logger _logger;

        public ServerForm(Server server, Logger logger)
        {
            _server = server;
            _logger = logger;

            InitializeComponent();

            _logger.SetWriter(WriteTextSafe);
        }

        private void WriteTextSafe(string text)
        {
            if (logBox.InvokeRequired)
            {
                var d = new SafeCallDelegateText(logBox.AppendText);
                logBox.Invoke(d, new object[] { text });
            }
            else
            {
                logBox.AppendText(text);
            }
        }

        private void listenButton_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(portInput.Text, out serverPort))
            {
                if (_server.Listen(serverPort))
                {
                    listenButton.Enabled = false;
                    portInput.Enabled = false;
                    logBox.Enabled = true;

                    _logger.Write($"Started listening on port: {serverPort}\n");
                    toolStripStatusLabel1.Text = $"Listening on port {serverPort}";
                }
                else
                {
                    _logger.Write("Cannot start listening.\n");
                }
            }
            else
            {
                _logger.Write("Check the port number!\n");
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server.Dispose();
            Environment.Exit(0);
        }
    }
}