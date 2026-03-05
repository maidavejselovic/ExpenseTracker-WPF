using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ExpenseTracker
{
    public partial class MainWindow : Window
    {
        private readonly ExpenseDbContext db;

        public ObservableCollection<Expense> Expenses { get; set; }

        public MainWindow()
        {
            InitializeComponent(); 
            db = new ExpenseDbContext();

            // Učitavanje rashoda iz baze
            Expenses = new ObservableCollection<Expense>(db.Expenses.ToList());

            DataContext = this;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();

            RefreshExpenses();
        }

        private void RefreshExpenses()
        {
            Expenses.Clear();

            foreach (var expense in db.Expenses.ToList())
            {
                Expenses.Add(expense);
            }
        }
    }
}