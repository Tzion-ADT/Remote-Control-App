using System.Net.Sockets;
using LogInfo;
using System.Text;
using DFM_Server.DataAccess.Models;
using System.Net;

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
//		Definition for client handler class
//      Handel the communication with the user: 1.Sending data, events ,etc...
//                                              2.Get data to change and events from the user
//
//
//	Date		Name								Description
//	----------------------------------------------------------------------------
// 2024         Tzion
//
//=============================================================================


namespace DFM_Server.Connection
{
    public class ClientHandler
    {
        private Socket clientSocket;
        private string currentIP_string;
        private IPAddress currentIP;
        private HashSet<string> Ips;
        public static event EventHandler<ServerMsgRecievedArgs> ServerMsgRecieved;


        public ClientHandler(Socket clientSocket, HashSet<string> ips)
        {
            this.clientSocket = clientSocket;
            currentIP_string = ((System.Net.IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();
            currentIP = ((System.Net.IPEndPoint)clientSocket.RemoteEndPoint).Address;
            Ips = ips;
        }

        public void Run()
        {
            try
            {

                ServerMsgRecieved += new EventHandler<ServerMsgRecievedArgs>(Server_Msg_Recieved);

                byte[] ans = new Byte[1024];
                string data = "";
                while (true)
                {
                    int ansRec = this.clientSocket.Receive(ans);
                    data += Encoding.ASCII.GetString(ans, 0, ansRec);
                    if (ServerMsgRecieved != null && data != "")
                    {
                        LoggerInfo.GetLogger().Info("Message recieved from GUI: \" + currentIP_string");

                        ServerMsgRecievedArgs ansMSGargs = new ServerMsgRecievedArgs(data);
                        Server_Msg_Recieved(this, ansMSGargs);
                    }

                    data = "";

                    //send it to the flutter remote app
                    // ------------ Test Connect to the flutter app -------------------------
                    //Socket flutteClientSocket = new Socket(clientSide.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    //System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(clientAddress);
                    //System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 1999);
                    //flutteClientSocket.Connect(remoteEP);

                    byte[] testData = System.Text.Encoding.ASCII.GetBytes(data);
                    this.clientSocket.Send(testData);
                    // ----------------------------------------------------------------------
                }
            }
            catch (Exception ex)
            {
                LoggerInfo.GetLogger().Error("ADTLink.AsynchronousClient.Send failed: " + currentIP_string);
            }
        }

        public void Server_Msg_Recieved(object sender, ServerMsgRecievedArgs e)
        {
            StatusVariables statusVariable = StatusVariables.readJson(e.msg);
            MessageBox.Show(statusVariable.ToString());
        }

        public class ServerMsgRecievedArgs : EventArgs
        {
            public string msg;
            public ServerMsgRecievedArgs(string msg)
            {
                this.msg = msg;
            }
        }
    }
}
