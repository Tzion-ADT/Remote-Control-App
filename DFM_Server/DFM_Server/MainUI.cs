using DFM_Server.Connection;

namespace DFM_Server
{
    public partial class Form1 : Form
    {
        private int portNumber = 1999;
        public Form1()
        {
            InitializeComponent();
        }


        private void btnPortNumber_Click(object sender, EventArgs e)
        { 
            try
            {
                //LoggerInfo.GetLogger().Info("User chose port number: " + portNumberString);

                Task.Run(() =>
                {
                    ServerSideConnection runServer = new ServerSideConnection(portNumber, this.lblIps);
                    //LoggerInfo.GetLogger().Info("runServer.Execute();");
                    runServer.Execute();
                });

                btnPortNumber.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong input, please choose again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
