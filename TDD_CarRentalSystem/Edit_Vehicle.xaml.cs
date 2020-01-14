using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace TDD_CarRentalSystem
{
    /// <summary>
    /// Interaction logic for Edit_Vehicle.xaml
    /// </summary>
    public partial class Edit_Vehicle : MetroWindow
    {
        public Edit_Vehicle()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
