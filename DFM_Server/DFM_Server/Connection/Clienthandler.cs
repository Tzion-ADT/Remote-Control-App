using System.Net.Sockets;
using LogInfo;
using ADTLink;
using NLog;
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
        private string currentIP;
        private HashSet<string> Ips;

        public ClientHandler(Socket clientSocket, HashSet<string> ips)
        {
            this.clientSocket = clientSocket;
            currentIP = ((System.Net.IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();
            Ips = ips;
        }

        public void Run()
        {
            try
            {
                ADTLink.AsynchronousClient.Send(clientSocket, "Test from Tzion 142 PC");
                LoggerInfo.GetLogger().Info("ADTLink.AsynchronousClient.Send succeeded: \" + currentIP");
            }
            catch (Exception ex)
            {
                LoggerInfo.GetLogger().Error("ADTLink.AsynchronousClient.Send failed: " + currentIP);
            }
            //        //GenericDAOImpl genericDAO = new GenericDAOImpl();
            //        //**********************Test for the DB**********************
            //        //Console.WriteLine("test SQL work");
            //        //GenericDAOImpl genericDAO = new GenericDAOImpl();
            //        //Console.WriteLine(genericDAO.GetPropertyBydbAndColumnAndTable("pp", "name", "recipe", ""));
            //    }

            //******************************************************************************************
        }
    }
}
