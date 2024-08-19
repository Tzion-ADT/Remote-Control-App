using Newtonsoft.Json;


namespace DFM_Server.DataAccess.Models
{
    internal class StatusVariables
    {
        private string Status;
        private string RecipeName;
        private int SawId;
        private string BladeName;
        private string LoginName;
        private int AirPressure;
        private int SpindleSpeed;
        private int UPH_LastDay;

        //public StatusVariables(){}
        public StatusVariables(string Status, string RecipeName, int SawId,
            string BladeName, string LoginName, int AirPressure,
            int SpindleSpeed, int UPH_LastDay)
        {
            this.Status = Status;
            this.RecipeName = RecipeName;
            this.SawId = SawId;
            this.BladeName = BladeName;
            this.LoginName = LoginName;
            this.AirPressure = AirPressure;
            this.SpindleSpeed = SpindleSpeed;
            this.UPH_LastDay = UPH_LastDay;
        }

        public string status { get { return Status; } }

        public string recipeName { get { return RecipeName; } }

        public int sawId { get { return SawId; } }

        public string bladeName { get { return this.BladeName; } }
        public string loginName { get { return this.LoginName; } }
        public int airPressure { get { return AirPressure; } }

        public int spindleSpeed { get { return SpindleSpeed; } }

        public int _UPH_LastDay { get { return UPH_LastDay; } }


        public static string createJson(StatusVariables statusVariables)
        {
            return JsonConvert.SerializeObject(statusVariables, Formatting.Indented);
        }

        public static StatusVariables readJson(string data)
        {
            return JsonConvert.DeserializeObject<StatusVariables>(data);
        }

        override
        public string ToString()
        {
            return this.Status + "," + this.recipeName + "," + this.SawId + "," + this.BladeName + "," + LoginName + "," +
                AirPressure + "," + "," + this.spindleSpeed + "," + this.SpindleSpeed + "," + this.UPH_LastDay + ".";
        }
    }
}
