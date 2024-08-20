using Newtonsoft.Json;

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
//		Definition machine status object 
//
//	Date		Name								Description
//	----------------------------------------------------------------------------
// 2024         Tzion
//

namespace DFM_Server.DataAccess.Models
{
    internal class StatusVariables
    {
        private string UserStatus;
        private string Status;
        private string RecipeName;
        private int SawId;
        private string BladeName;
        private string LoginName;
        private int AirPressure;
        private int SpindleSpeed;

        //public StatusVariables(){}
        public StatusVariables(string UserStatus , string Status, string RecipeName, int SawId,
            string BladeName, string LoginName, int AirPressure,
            int SpindleSpeed)
        {
            this.UserStatus = UserStatus;
            this.Status = Status;
            this.RecipeName = RecipeName;
            this.SawId = SawId;
            this.BladeName = BladeName;
            this.LoginName = LoginName;
            this.AirPressure = AirPressure;
            this.SpindleSpeed = SpindleSpeed;
        }

        public string userStatus{ get { return UserStatus; } }

        public string status { get { return Status; } }

        public string recipeName { get { return RecipeName; } }

        public int sawId { get { return SawId; } }

        public string bladeName { get { return this.BladeName; } }
        public string loginName { get { return this.LoginName; } }
        public int airPressure { get { return AirPressure; } }

        public int spindleSpeed { get { return SpindleSpeed; } }


        public static string createJson(StatusVariables statusVariables)
        {
            return JsonConvert.SerializeObject(statusVariables, Formatting.Indented);
        }

        public static StatusVariables readJson(string data)
        {
            return JsonConvert.DeserializeObject<StatusVariables>(data);
        }

        public string writeToDBSQL()
        {
            return "INSERT INTO MachineStatus (UserStatus , Status , RecipeName , SawId , BladeName , LoginName , AirPressure , SpindleSpeed)" +
                " VALUES (@UserStatus , @Status , @RecipeName , @SawId , @BladeName , @LoginName , @AirPressure , @SpindleSpeed)";
        }

        override
        public string ToString()
        {
            return this.UserStatus + this.Status + "," + this.recipeName + "," + this.SawId + "," + this.BladeName + "," + LoginName + "," +
                AirPressure + "," + "," + this.spindleSpeed + "," + this.SpindleSpeed + ".";
        }
    }
}
