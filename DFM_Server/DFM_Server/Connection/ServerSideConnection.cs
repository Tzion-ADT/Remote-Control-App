using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DFM_Server.Connection
{
    // Definition for Server side connection: listening and waiting for client sides to connect
    // This operation is in the background so the UI is available for the user
    public class ServerSideConnection
    {
        private static TcpListener serverSide;
        private int portNumber;
        private HashSet<string> Ips; // Using HashSet because it is a unique data structure
        private Label clientIPLabel;

        public ServerSideConnection(int portNumber, Label clientIPLabel)
        {

            this.portNumber = portNumber;
            this.clientIPLabel = clientIPLabel;
            Ips = new HashSet<string>();
        }

        /*
        Opening a server that will listen and wait for the client
        When the client is connected --> will establish socket to talk for every client
        */
        public async void Execute()
        {
            try
            {
                serverSide = new TcpListener(IPAddress.Any, portNumber);
                serverSide.Start();
                Console.WriteLine("Port number is: " + portNumber);

                while (true)
                {
                    try
                    {
                        TcpClient clientSide = await serverSide.AcceptTcpClientAsync();
                        string clientAddress = ((IPEndPoint)clientSide.Client.RemoteEndPoint).Address.ToString();

                        if (!Ips.Contains(clientAddress))
                        {
                            //LoggerInfo.GetLogger().Info("Connection established for " + clientAddress + ", on port " + portNumber);
                            Console.WriteLine("Connected to " + clientAddress);

                            // In the following, the connection to the DB will establish
                            Thread clientSideHandlerThread = new Thread(() => HandleClient(clientSide));
                            clientSideHandlerThread.Start();

                            List<string> clientDetailsList = new List<string>
                            {
                                clientAddress,
                                Dns.GetHostEntry(((IPEndPoint)clientSide.Client.RemoteEndPoint).Address).HostName
                            };

                            ProcessClientData(clientDetailsList); // This will help to use the data in every new connection
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        if (serverSide != null)
                        {
                            // serverSide.Stop(); Uncomment if you need to stop the server
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void HandleClient(TcpClient client)
        {
            // Handle client connection, communication, and disconnection here
            // Implement your logic for interacting with the client
        }

        private void ProcessClientData(List<string> clientDetailsList)
        {
            if (clientDetailsList.Count > 1)
            {
                this.clientIPLabel.Invoke((Action)(() =>
                {
                    this.clientIPLabel.Text += "IP: " + clientDetailsList[0] + ", PC Name: " + clientDetailsList[1]+"\n";
                    Ips.Add(clientDetailsList[0]);
                }));

                Console.WriteLine(this.clientIPLabel.Text + "</html>");
            }
        }
    }
}
