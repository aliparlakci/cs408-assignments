using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace cs408_hw1_client
{
    public class Client
    {
        private Logger _logger;

        Socket clientSocket;

        bool terminating = false;
        bool connected = false;

        public Action _onDisconnect;

        public Client(Logger logger)
        {
            _logger = logger;
        }

        public void OnDisconnect(Action func)
        {
            _onDisconnect = func;
        }

        public void SetTerminating() { terminating = true; }

        public bool Connect(string ip, int port)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(ip, port);
                connected = true;

                Thread receiveThread = new Thread(receive);
                receiveThread.Start();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Send(string message)
        {
            if (!connected)
            {
                return false;
            }

            Byte[] buffer = Encoding.Default.GetBytes(message);
            
            try
            {
                clientSocket.Send(buffer);
            }
            catch
            {
                clientSocket.Close();
                connected = false;

                if (_onDisconnect != null) _onDisconnect();

                return false;
            }

            return true;
        }

        public void Disconnect()
        {
            clientSocket.Close();
            if (_onDisconnect != null) _onDisconnect();
        }

        private void receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[1024];
                    if (clientSocket.Receive(buffer) > 0)
                    {
                        string message = Encoding.Default.GetString(buffer);
                        message = message.Substring(0, message.IndexOf('\0'));

                        _logger.Write($"{CayGetirProtocol.ParseMessage(message)}\n");
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        _logger.Write("Server has disconnected.\n");
                    }
                    else
                    {
                        terminating = false;
                    }

                    clientSocket.Close();
                    connected = false;
                    if (_onDisconnect != null) _onDisconnect();
                    _logger.Write("Succesfully disconnected!\n");
                }
            }
        }
    }
}
