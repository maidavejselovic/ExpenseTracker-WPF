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
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System.Collections.ObjectModel;
using ExpenseTracker.Windows;

namespace ExpenseTracker
{
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();  
        }

    }
}