using C_Client.Pregledi.Meni;
using C_Client.Servisi;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplicationDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            Switcher switcher = new Switcher();

            switcher.registerWindow(typeof(MainWindow), "transitionContent");
            Page mainMenuPage = new MainMenuPage();

            switcher.registerPage("mainMenu", typeof(MainWindow), mainMenuPage, TransitionType.LeftReplace);

            Switcher.navigate("mainMenu");

        }
    }
}
