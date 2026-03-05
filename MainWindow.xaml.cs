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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;  //dugme na koje je klinuto sender pretvori u framework element
            var expense = button.DataContext as Expense;  //uzmi expense objekat iz reda u kome je klinuto dugme

            if (expense == null)
                return;

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this expense?",
                "Delete confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                db.Expenses.Remove(expense);
                db.SaveChanges();

                Expenses.Remove(expense);
            }
        }
    }
}