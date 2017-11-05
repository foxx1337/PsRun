using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace PsRun
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string APP_NAME = "PsRun";

        public void EntryPoint(object sender, StartupEventArgs eventArgs)
        {
            if (eventArgs.Args.Length != 1)
            {
                MessageBox.Show(
                    $"Usage:\n{APP_NAME} script.ps1\n\nRuns the PowerShell script file in a headless host",
                    APP_NAME,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    var result = RunScript(eventArgs.Args[0]);
                    if (result.Count > 0)
                    {
                        // replace this with a selectable window
                        MessageBox.Show("Script returned: " + result.ToString(), APP_NAME,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Got an error: " + e.ToString(), APP_NAME,
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Shutdown();
        }

        public Collection<PSObject> RunScript(string path)
        {
            string commands = File.ReadAllText(path);
            PowerShell powerShell = PowerShell.Create();
            powerShell.AddScript(commands);
            return powerShell.Invoke();
        }
    }
}
