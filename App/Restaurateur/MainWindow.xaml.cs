using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Restaurateur
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DgListOrders.ItemsSource = WebService.Data.GetListOrder();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 60);
            timer.Tick += new EventHandler(refresh);
            timer.Start();
        }

        private void refresh(object sender, EventArgs e)
        {
            DgListOrders.ItemsSource = WebService.Data.GetListOrder();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string status = (sender as ComboBox).SelectedItem as string;
            int index = DgListOrders.Items.IndexOf(DgListOrders.CurrentItem);

            List<Order> orders = WebService.Data.GetListOrder();

            if (index >= 0)
                WebService.Data.SetOrderStatus(orders[index].Id_Order, status);
        }
    }
}
