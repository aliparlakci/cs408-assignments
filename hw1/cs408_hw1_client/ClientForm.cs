namespace cs408_hw1_client
{
    public partial class ClientForm : Form
    {
        private Client _client;
        private Logger _logger;

        public ClientForm(Client client, Logger logger)
        {
            _client = client;
            _logger = logger;

            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            _logger.SetWriter(logBox.AppendText);

            _client.OnDisconnect(() =>
            {
                connectBox.Enabled = true;
                newUserBox.Visible = false;
                disconnectButton.Enabled = false;
            });
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectBox.Enabled = false;

            string ip = ipInput.Text;
            int serverPort;

            if (Int32.TryParse(portInput.Text, out serverPort))
            {
                if (_client.Connect(ip, serverPort))
                {
                    logBox.Enabled = true;

                    _logger.Write($"You are connected!\n");
                    toolStripStatusLabel1.Text = $"Connected to {ip}:{serverPort}";

                    newUserBox.Visible = true;
                    disconnectButton.Enabled = true;
                }
                else
                {
                    _logger.Write("Connection could not be established.\n");
                    connectBox.Enabled = true;
                }
            }
            else
            {
                _logger.Write("Please check port number \n");
                connectBox.Enabled = true;
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            _client.SetTerminating();
            _client.Disconnect();
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            var message = CayGetirProtocol.Signup(nameInput.Text, surnameInput.Text, userNameInput.Text, passwordInput.Text);
            _client.Send(message);
        }
    }
}