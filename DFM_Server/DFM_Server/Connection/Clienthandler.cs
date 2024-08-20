using System.Net.Sockets;
using LogInfo;
using System.Text;
using DFM_Server.DataAccess.Models;
using System.Net;
using System.Data.SQLite;
using DFM_Server.DataAccess;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private string GUI_APP = "GUI";

        private string FLUTTER_APP = "FLUTTER";
        private string FLUTTER_APP_WANT_DATA = "Flutter here, send me data";


        private Socket clientSocket;
        private string currentIP_string;
        private IPAddress currentIP;
        private HashSet<string> Ips;
        public static event EventHandler<ServerMsgRecievedArgs> ServerMsgRecieved;

        private SQLiteConnection conn = DBConnection.GetInstance(@"Data Source=I:\Subversion\Remote-Control-App\DFM_Server\DFM_Server\db\RemoteControl.db;Version=3;");



        public ClientHandler(Socket clientSocket, HashSet<string> ips)
        {
            this.clientSocket = clientSocket;
            currentIP_string = ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();
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

                    if (data != "")
                    {
                        //data came from the GUI --> will store to the DB 
                        if (data.Contains(GUI_APP))
                        {
                            LoggerInfo.GetLogger().Info("Message recieved from GUI: \" + currentIP_string");

                            if (ServerMsgRecieved != null)
                            {
                                ServerMsgRecievedArgs ansMSGargs = new ServerMsgRecievedArgs(data);
                                Server_Msg_Recieved(this, ansMSGargs);
                            }

                            data = "";
                        }

                        //data from flutter applications --> check if need to send data back or save in DB 
                        if (data.Contains(FLUTTER_APP_WANT_DATA))
                        {
                            sendToFlutter(data);
                        }
                        
                    }
                }

                    /********************************************************************************/
                    /********************************************************************************/
                    /********************************************************************************/
                    //add another port number for testing because the flutter client is on the same pc 
                    //as the server.
                    //TcpListener clientSideFlutter = new TcpListener(IPAddress.Any, 1998);//for the server
                    /*********************************************************************************/
                    /********************************************************************************/
                    /********************************************************************************/

                    //send it to the flutter remote app
                    // ------------ Test Connect to the flutter app -------------------------
                    //Socket flutteClientSocket = new Socket(clientSide.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    //IPAddress ipAdd = System.Net.IPAddress.Parse("199.203.44.132");
                    //IPEndPoint remoteEP = new IPEndPoint(ipAdd, 1998);
                    //flutteClientSocket.Connect(remoteEP);
            }
            catch (Exception ex)
            {
                LoggerInfo.GetLogger().Error("ADTLink.AsynchronousClient.Send failed: " + currentIP_string);
            }
        }
        /// <summary>
        /// Get data from(!!!) GUI using ADTLink DLL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Server_Msg_Recieved(object sender, ServerMsgRecievedArgs e)
        {
            StatusVariables statusVariable = StatusVariables.readJson(e.msg);
            CRUD.WriteToDB(conn , statusVariable);
        }

        public void sendToFlutter(string data)
        {
            string dataToSend = StatusVariables.createJson(CRUD.readFromDB(conn));
            try
            {
                this.clientSocket.Send(Encoding.ASCII.GetBytes(dataToSend));
            }
            catch (Exception ex) 
            {
                LoggerInfo.GetLogger().Error("sendToFlutter failed: " + ex.ToString());
            }
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
