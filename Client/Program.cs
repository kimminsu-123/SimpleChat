using Chungkang.GameNetwork.Network.Manager;

namespace Client
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                NetworkManager.Instance.Initialize();
                NetworkManager.Instance.Start();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Frm_Login());
        }
    }
}