using System;
using System.Diagnostics;

namespace ApiServer.Services
{
    public class CmdExecutorService
    {
        public void ExecuteCommand(string command)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "/k " + command);
                processStartInfo.UseShellExecute = true;
                processStartInfo.CreateNoWindow = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                // Handle any error that might occur during process start
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
