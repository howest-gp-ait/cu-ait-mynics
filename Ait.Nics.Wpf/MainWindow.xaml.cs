using System;
using System.Collections.Generic;
using System.Linq;
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
using Ait.Nics.Core.Entities;
using Ait.Nics.Core.Services;

namespace Ait.Nics.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NicService nicService;
        public MainWindow()
        {
            InitializeComponent();
            nicService = new NicService();
            lstNics.ItemsSource = nicService.AllNics;
            ClearControls();
        }
        private void ClearControls()
        {
            grpInfo.Header = "-";
            lblID.Content = "-";
            lblNetworkInterfaceType.Content = "-";
            lblOperationalStatus.Content = "-";
            lblSpeed.Content = "-";
            lblMac.Content = "-";
            lblIP4.Content = "-";
            lblIP6.Content = "-";
            lblOperationalStatus.Background = Brushes.Transparent;
            lblOperationalStatus.Foreground = Brushes.Black;
            grpInfo.Background = Brushes.White;
        }
        private void lstNics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstNics.SelectedItem == null) return;

            Nic nic = (Nic)lstNics.SelectedItem;

            grpInfo.Header =  nic.Description;
            lblID.Content = nic.Id;
            lblNetworkInterfaceType.Content = nic.NetworkInterfaceType;
            lblOperationalStatus.Content = nic.OperationalStatus;
            lblOperationalStatus.Foreground = Brushes.White;
            if (lblOperationalStatus.Content.ToString().ToUpper() == "DOWN")
            {
                lblOperationalStatus.Background = Brushes.Tomato;
                grpInfo.Background = Brushes.MistyRose;
            }
            else
            {
                lblOperationalStatus.Background = Brushes.ForestGreen;
                grpInfo.Background = Brushes.Honeydew;
            }
            lblSpeed.Content = nic.Speed + " Mb";
            lblMac.Content = nic.MacAddress;
            lblIP4.Content = nic.IPv4Info;
            lblIP6.Content = nic.iPv6Info;

        }
    }
}
