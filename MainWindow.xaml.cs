using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            RefreshExpenses();
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (sender as TextBox).Text.ToLower();

            var filtered = db.Expenses
                .Where(x => x.Description.ToLower().Contains(text))
                .ToList();

            Expenses.Clear();

            foreach (var exp in filtered)
            {
                Expenses.Add(exp);
            }
            UpdateTotal(); // ✔ da se total menja kad filtriraš
        }

        private void UpdateTotal()
        {
            txtTotal.Text = "Total: " + Expenses.Sum(x => x.Amount) + " €";
        }
        private void RefreshExpenses()
        {
            Expenses.Clear();

            foreach (var expense in db.Expenses.ToList())
            {
                Expenses.Add(expense);
            }

            UpdateTotal();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();

            RefreshExpenses();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var expense = button.DataContext as Expense;

            EditWindow window = new EditWindow(expense);
            window.ShowDialog();

            RefreshExpenses();
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
            RefreshExpenses();
        }

        private void PaidChanged(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as System.Windows.Controls.CheckBox;
            var expense = checkbox.DataContext as Expense;

            if (expense != null)
            {
                var dbExpense = db.Expenses.FirstOrDefault(x => x.Id == expense.Id);

                if (dbExpense != null)
                {
                    dbExpense.IsPaid = checkbox.IsChecked == true;
                    db.SaveChanges();
                }
            }
        }
    }
}