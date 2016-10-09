using C_Client.Servisi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace C_Client.Pregledi.Meni
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public DataTable myDT { get; set; }

        public MainMenuPage()
        {
            InitializeComponent();
            myDT = GetDT();
            this.DataContext = this;
        }

        static DataTable GetDT()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Shop", typeof(string));
            table.Columns.Add("Place", typeof(string));
            table.Columns.Add("Year", typeof(int));

            table.Rows.Add("1", "Serbia", 2015);
            table.Rows.Add("2", "UK", 2015);
            table.Rows.Add("3", "Canada", 2016);
            table.Rows.Add("4", "Germany", 2015);
            table.Rows.Add("5", "France", 2016);
            table.Rows.Add("6", "Italy", 2014);
            return table;
        }
    }
}
