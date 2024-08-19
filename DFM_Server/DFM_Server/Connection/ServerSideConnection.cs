using System.Net.Sockets;
using System.Net;
using LogInfo;

////////////////////////////////////////////////////////////////////////////////
//
//  COPYRIGHT (c) 2024 ADT, INC.
//
//  This software is the property of ADT Industries, Inc.
//  Any reproduction or distribution to a third party is
//  strictly forbidden unless written permission is given by an
//  authorized agent of ADT.
//
//  DESCRIPTION
//		Definition for Server side connection : listening and waiting for client sides to connect
//      This operation is in the background so the UI available for the user
//
//
//	Date		Name								Description
//	----------------------------------------------------------------------------
// 2024         Tzion
//
//=============================================================================


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
                serverSide = new TcpListener(IPAddress.Any, portNumber);//for the server

                /********************************************************************************/
                /********************************************************************************/
                /********************************************************************************/
                //add another port numer for testing because the flutter client is on the same pc 
                //ass the server.
                /*********************************************************************************/
                /********************************************************************************/
                /********************************************************************************/

                serverSide.Start();
                Console.WriteLine("Port number is: " + portNumber);

                while (true)
                {
                    try
                    {
                        Socket clientSide = await serverSide.AcceptSocketAsync();
                        string clientAddress = ((IPEndPoint)clientSide.RemoteEndPoint).Address.ToString();

                        if (!Ips.Contains(clientAddress))
                        {
                            LoggerInfo.GetLogger().Info("Connection established for " + clientAddress + ", on port " + portNumber);
                            Console.WriteLine("Connected to " + clientAddress);

                            // In the following, the connection to the DB will establish
                            Thread clientSideHandlerThread = new Thread(() =>
                            {
                                new ClientHandler(clientSide, this.Ips).Run();
                            });
                            clientSideHandlerThread.Start();

                            List<string> clientDetailsList = new List<string>
                            {
                                clientAddress,
                                Dns.GetHostEntry(((IPEndPoint)clientSide.RemoteEndPoint).Address).HostName
                            };

                            ProcessClientData(clientDetailsList); // This will help to use the data in every new connection
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerInfo.GetLogger().Error("ADTLink.AsynchronousClient.Send failed: " + ex.ToString());
                    }
                    finally
                    {
                        if (serverSide != null)
                        {
                            //TO-DO : option to check Socked connection if it still running
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

        private void ProcessClientData(List<string> clientDetailsList)
        {
            if (clientDetailsList.Count > 1)
            {
                this.clientIPLabel.Invoke((Action)(() =>
                {
                    this.clientIPLabel.Text += "IP: " + clientDetailsList[0] + ", PC Name: " + clientDetailsList[1] + "\n";
                    Ips.Add(clientDetailsList[0]);
                }));

                Console.WriteLine(this.clientIPLabel.Text + "</html>");
            }
        }
    }
}
