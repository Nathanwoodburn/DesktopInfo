using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace DesktopInfo
{
    public partial class Form1 : Form
    {
        // Change this to false if you want a light theme
        const bool dark = true;

        // Other variables
        const string api = "https://glances.woodburn.au/api/3/";
        private const string remoteHost = "glances.woodburn.au";
        const int topMargin = 40;
        const int leftMargin = 20;


        public Form1()
        {
            InitializeComponent();
        }
        // Hide from ALT+Tab
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.TransparencyKey = Color.White;
            if (dark)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                this.TransparencyKey = Color.Black;
            }
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this.SendToBack();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.SendToBack();
            }
        }
        private async void updatetimer_TickAsync(object sender, EventArgs e)
        {
            updatetimer.Interval = 1000;

            this.Left = Screen.PrimaryScreen.Bounds.Width - (this.Width + leftMargin);
            this.Top = topMargin;
            this.SendToBack();

            if (!IsRemoteOnline())
            {
                labelremote.Text = "Remote - Offline";
            }
            else
            {
                labelremote.Text = "Remote - Online";
                try
                {
                    // Update
                    updateDocker();
                    updateUptime();
                    updateSystem();
                }
                catch (Exception)
                {
                    labelremote.Text = "Remote - Error";
                }
            }

            if (!isLocalOnline())
            {
                labellocal.Text = "Local - Offline";
            }
            else
            {
                labellocal.Text = "Local - Online";
            }
            labellocal.Left = labelremote.Left;
            updateLocal();

        }


        private bool IsRemoteOnline()
        {
            try
            {
                Ping myPing = new Ping();
                String host = remoteHost;
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }

        }
        private bool isLocalOnline()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async void updateDocker()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(api + "docker");
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                int running = 0;
                int stopped = 0;

                foreach (var container in json["containers"].Children())
                {
                    if (container["Status"].ToString() == "running") running++;
                    else stopped++;
                }

                labelContainer.Text = "Total containers: " + (running + stopped).ToString() +
                    "\nRunning: " + running.ToString() +
                    "\nStopped: " + stopped.ToString();
            }
            catch (Exception)
            {

            }
            labelContainer.Left = this.Width - (labelContainer.Width + leftMargin);
        }
        private async void updateUptime()
        {
            labeluptime.Left = labelContainer.Left;
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(api + "uptime");
                var content = await response.Content.ReadAsStringAsync();
                if (content.Contains("502"))
                {
                    return;
                }

                labeluptime.Text = "Uptime:\n" + content.Replace("\"", "");
            }
            catch (Exception)
            {

            }
            labeluptime.Left = labelContainer.Left;
        }
        private async void updateSystem()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(api + "mem");
                var content = await response.Content.ReadAsStringAsync();
                var jsonmem = JObject.Parse(content);


                response = await client.GetAsync(api + "cpu");
                content = await response.Content.ReadAsStringAsync();
                var jsoncpu = JObject.Parse(content);

                response = await client.GetAsync(api + "fs");
                content = await response.Content.ReadAsStringAsync();
                // Remove the first and last characters from the string
                content = content.Substring(1, content.Length - 2);
                var jsonfs = JObject.Parse(content);


                Int64 gbLeft = Int64.Parse(jsonfs["free"].ToString()) / 1000000000;

                labelsystem.Text = jsonmem["percent"].ToString() + "% RAM\n" +
                    jsoncpu["total"].ToString() + "% CPU\n" +
                    jsonfs["percent"].ToString() + "% Disk\n" +
                    gbLeft + " GB left";

            }
            catch (Exception)
            {

            }
            labelsystem.Left = labelContainer.Left;
        }

        PerformanceCounter cpuCounter = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
        private void updateLocal()
        {
            try
            {
                var ramPercent = (int)(100 * (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory -
                    new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) /
                    new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory);
                var cpuPercent = (int)cpuCounter.NextValue();
                var diskTotal = GetTotalSpace("C:\\");
                var diskFree = GetTotalFreeSpace("C:\\");
                var diskPercent = (int)(100 * (diskTotal - diskFree) / diskTotal);
                diskFree = diskFree / 1000000000;

                labelstats.Text = ramPercent.ToString() + "% RAM\n" +
                    cpuPercent.ToString() + "% CPU\n" +
                    diskPercent.ToString() + "% Disk\n" +
                    diskFree.ToString() + " GB left";


            }
            catch (Exception ex)
            {
                labelstats.Text += ex.ToString();
            }
            labelstats.Left = labelContainer.Left;

        }

        private long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.AvailableFreeSpace;
                }
            }
            return -1;
        }
        private long GetTotalSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalSize;
                }
            }
            return -1;
        }

    }

}