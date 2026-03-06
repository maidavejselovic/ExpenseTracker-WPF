using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.ViewModels;
using ExpenseTracker.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ExpenseTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainWindowViewModel();
            DataContext = vm;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Owner = this; // centriraj na ovaj prozor
            addWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addWindow.ShowDialog();

            // Refresh Expenses preko ViewModel-a
            if (DataContext is MainWindowViewModel vm)
                vm.RefreshExpenses();
        }
    }
}
   