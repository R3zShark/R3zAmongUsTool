using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace R3zAmongUsTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Settings.Default.AmongUsPath != "null")
            {
                /*if (File.Exists(Settings.Default.AmongUsPath))
                {
                    MessageBox.Show(FileVersionInfo.GetVersionInfo(Settings.Default.AmongUsPath).FileVersion);
                    if (FileVersionInfo.GetVersionInfo(Settings.Default.AmongUsPath).FileVersion == "")
                    {

                    }
                }*/
                infoBlock.Visibility = Visibility.Collapsed;
                installMods.Visibility = Visibility.Visible;
                uninstallToU.Visibility = Visibility.Visible;
                uninstallAllMods.Visibility = Visibility.Visible;
                runCrewLink.Visibility = Visibility.Visible;
                runAmongUs.Visibility = Visibility.Visible;
                gamePath.Text = Settings.Default.AmongUsPath;
            }
        }

        bool result;

        public void FindGame()
        {
            result = false;

            while (!result)
            {
                var files = new List<string>();
                foreach (DriveInfo d in DriveInfo.GetDrives().Where(x => x.IsReady == true))
                {
                    try
                    {
                        gamePath.Text = Directory.GetFiles(d.RootDirectory.FullName + "Program Files (x86)", "Among Us.exe", SearchOption.AllDirectories)[0];

                        result = true;
                        MessageBox.Show("FOUND IT!");
                        progressBar.Visibility = Visibility.Collapsed;
                        cancelSearchBtn.Visibility = Visibility.Collapsed;
                        Settings.Default.AmongUsPath = gamePath.Text;
                        Settings.Default.Save();

                        installMods.Visibility = Visibility.Visible;
                        uninstallToU.Visibility = Visibility.Visible;
                        uninstallAllMods.Visibility = Visibility.Visible;
                        runCrewLink.Visibility = Visibility.Visible;
                        runAmongUs.Visibility = Visibility.Visible;
                    }
                    catch
                    {

                    }
                    try
                    {
                        gamePath.Text = Directory.GetFiles(d.RootDirectory.FullName + "SteamLibrary", "Among Us.exe", SearchOption.AllDirectories)[0];

                        result = true;
                        MessageBox.Show("FOUND IT!");
                        progressBar.Visibility = Visibility.Collapsed;
                        cancelSearchBtn.Visibility = Visibility.Collapsed;
                        Settings.Default.AmongUsPath = gamePath.Text;
                        Settings.Default.Save();

                        installMods.Visibility = Visibility.Visible;
                        uninstallToU.Visibility = Visibility.Visible;
                        uninstallAllMods.Visibility = Visibility.Visible;
                        runCrewLink.Visibility = Visibility.Visible;
                        runAmongUs.Visibility = Visibility.Visible;
                    }
                    catch
                    {

                    }
                }
            }

        }

        private void gamePath_Initialized(object sender, EventArgs e)
        {
        }

        private void gamePath_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                gamePath.Text = Directory.GetFiles(gamePath.Text)[0];
                Settings.Default.AmongUsPath = gamePath.Text;
                Settings.Default.Save();
                infoBlock.Visibility = Visibility.Collapsed;
                installMods.Visibility = Visibility.Visible;
                uninstallAllMods.Visibility= Visibility.Visible;
                uninstallToU.Visibility= Visibility.Visible;
                runCrewLink.Visibility= Visibility.Visible;
                runAmongUs.Visibility= Visibility.Visible;
            }
        }

        private void findGameBtn_Click(object sender, RoutedEventArgs e)
        {
            infoBlock.Visibility = Visibility.Collapsed;
            FindGame();
        }

        private void cancelSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            result = true;

            cancelSearchBtn.Visibility = Visibility.Collapsed;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void installMods_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods");

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://github.com/eDonnes124/Town-Of-Us-R/releases/download/v3.3.1/ToU.v3.3.1.zip", System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\TownOfUs v3.3.1.zip");
                    client.DownloadFile("https://drive.google.com/u/0/uc?id=1McBW2paotXPQpZItj05vTQTbb-zhyeo1&export=download", System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\R3zAmongUsMod.dll");

                    InstallMods();
                }
            }
            else
            {
                if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\TownOfUs v3.3.1.zip"))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile("https://github.com/eDonnes124/Town-Of-Us-R/releases/download/v3.3.1/ToU.v3.3.1.zip", System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\TownOfUs v3.3.1.zip");
                    }
                }

                if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\R3zAmongUsMod.dll"))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile("https://drive.google.com/u/0/uc?id=1McBW2paotXPQpZItj05vTQTbb-zhyeo1&export=download", System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\R3zAmongUsMod.dll");
                    }
                }

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\R3zAmongUsMod.dll"))
                {
                    File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\R3zAmongUsMod.dll");
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile("https://drive.google.com/u/0/uc?id=1McBW2paotXPQpZItj05vTQTbb-zhyeo1&export=download", System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\R3zAmongUsMod.dll");
                    }
                }
                InstallMods();
            }
        }

        void InstallMods()
        {
            UninstallAllMods(true);
            string path = gamePath.Text.Substring(0, gamePath.Text.LastIndexOf(@"\"));
            ZipFile.ExtractToDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\TownOfUs v3.3.1.zip", path);

            var dirs = Directory.GetDirectories(path + @"\ToU v3.3.1");
            foreach (var dir in dirs)
            {
                if (dir.Contains("BepInEx"))
                {
                    Directory.Move(dir, path + @"\BepInEx");
                }
                else if (dir.Contains("mono"))
                {
                    Directory.Move(dir, path + @"\mono");
                }
            }

            var files = Directory.GetFiles(path + @"\ToU v3.3.1");
            foreach (var file in files)
            {
                if (file.Contains("doorstop_config.ini"))
                {
                    File.Move(file, path + @"\doorstop_config.ini");
                }
                else if (file.Contains("steam_appid.txt"))
                {
                    File.Move(file, path + @"\steam_appid.txt");
                }
                else if (file.Contains("winhttp.dll"))
                {
                    File.Move(file, path + @"\winhttp.dll");
                }
            }

            Directory.Delete(path + @"\ToU v3.3.1", true);

            File.Move(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\R3zAmongUsMod.dll", path + @"\BepInEx\plugins\R3zAmongUsMod.dll");

            MessageBox.Show("Done installing the mods!");
        }

        private void uninstallToU_Click(object sender, RoutedEventArgs e)
        {
            string path = gamePath.Text.Substring(0, gamePath.Text.LastIndexOf(@"\")) + @"\BepInEx\plugins";

            try
            {
                File.Delete(path + @"\Submerged.dll");
                File.Delete(path + @"\TownOfUs.dll");
                MessageBox.Show("Town of Us is now Uninstalled... \n\n The Custom Hats mod will still be available in game!");
            }
            catch { }
        }

        private void uninstallAllMods_Click(object sender, RoutedEventArgs e)
        {
            UninstallAllMods(false);
        }

        void UninstallAllMods(bool isInstalling)
        {
            string path = gamePath.Text.Substring(0, gamePath.Text.LastIndexOf(@"\"));

            if (Directory.Exists(path + @"\ToU v3.3.1"))
            {
                Directory.Delete(path + @"\ToU v3.3.1", true);
            }

            try
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    if (dir.Contains("BepInEx"))
                    {
                        Directory.Delete(path + @"\BepInEx", true);
                    }
                    else if (dir.Contains("mono"))
                    {
                        Directory.Delete(path + @"\mono", true);
                    }
                }

                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    if (file.Contains("doorstop_config.ini"))
                    {
                        File.Delete(path + @"\doorstop_config.ini");
                    }
                    else if (file.Contains("steam_appid.txt"))
                    {
                        File.Delete(path + @"\steam_appid.txt");
                    }
                    else if (file.Contains("winhttp.dll"))
                    {
                        File.Delete(path + @"\winhttp.dll");
                    }
                }
                if (!isInstalling)
                {
                    MessageBox.Show("Done Uninstalling all mods... \n\n Your game should be completely vanilla now...");
                }
            }
            catch { }
        }

        private void runCrewLink_Click(object sender, RoutedEventArgs e)
        {
            if(!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods");
            }

            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\BetterCrewLink.exe"))
            {
                Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\BetterCrewLink.exe");
            }
            else
            {
                MessageBox.Show("BetterCrewLink.exe was not found... \n\nPlease give the application a minute to download Better Crewlink!");
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://github.com/OhMyGuus/BetterCrewLink/releases/download/v3.0.3/Better-CrewLink-Setup-3.0.3.exe", System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\BetterCrewLink.exe");
                }
                Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"auMods\BetterCrewLink.exe");
            }
        }

        private void runAmongUs_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(gamePath.Text);
        }
    }
}
