using DFM_Server.DataAccess.Models;
using LogInfo;
using System.Data.SQLite;

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
//		Definition CRUD operations in the DB :
//          1. C - Create --> WriteToDB
//          2. R - Read
//          3. U - Update
//          4. D - Delete
//
//
//	Date		Name								Description
//	----------------------------------------------------------------------------
// 2024         Tzion
//

namespace DFM_Server.DataAccess
{
    internal class CRUD
    {

        public static void WriteToDB(SQLiteConnection conn , StatusVariables statusVariables)
        {
            try
            {
                string writeData = statusVariables.writeToDBSQL();

                using (var command = new SQLiteCommand(writeData, conn))
                {
                    command.Parameters.AddWithValue("@UserStatus", statusVariables.userStatus);
                    command.Parameters.AddWithValue("@Status", statusVariables.status);
                    command.Parameters.AddWithValue("@RecipeName", statusVariables.recipeName);
                    command.Parameters.AddWithValue("@SawId", statusVariables.sawId);
                    command.Parameters.AddWithValue("@BladeName", statusVariables.bladeName);
                    command.Parameters.AddWithValue("@LoginName", statusVariables.loginName);
                    command.Parameters.AddWithValue("@AirPressure", statusVariables.airPressure);
                    command.Parameters.AddWithValue("@SpindleSpeed", statusVariables.spindleSpeed);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected +"row(s) inserted.");
                }
            }
            catch (Exception ex) 
            {
                LoggerInfo.GetLogger().Error("Erite to DB failed: " + ex.ToString());
            }
        }

        public static StatusVariables readFromDB(SQLiteConnection conn)
        {
            StatusVariables statusVariables = null;
            try
            {
                string readDataSQL = "SELECT * FROM MachineStatus";

                using (var command = new SQLiteCommand(readDataSQL, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            string UserStatus = reader.GetString(0);
                            string Status     = reader.GetString(1);
                            string RecipeName = reader.GetString(2);
                            int SawId         = reader.GetInt32(3);
                            string BladeName  = reader.GetString(4);
                            string LoginName  = reader.GetString(5);
                            int AirPressure   = reader.GetInt32(6);
                            int SpindleSpeed  = reader.GetInt32(7);

                            statusVariables = new StatusVariables
                                (
                                    UserStatus,
                                    Status,
                                    RecipeName,
                                    SawId,
                                    BladeName,
                                    LoginName,
                                    AirPressure,
                                    SpindleSpeed
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerInfo.GetLogger().Error("Erite to DB failed: " + ex.ToString());
            }

            return statusVariables;
        }
    }
}
