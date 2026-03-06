using ExpenseTracker.Commands;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ExpenseDbContext db;

        public ObservableCollection<Expense> Expenses { get; set; }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterExpenses();
            }
        }

        public string Total => $"Total: {Expenses.Sum(x => x.Amount)} €";

        // ICommand dugmad
       // public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainWindowViewModel()
        {
            db = new ExpenseDbContext();
            Expenses = new ObservableCollection<Expense>(db.Expenses.ToList());

            //AddCommand = new RelayCommand<object>(OpenAddWindow);
            EditCommand = new RelayCommand<Expense>(EditExpense);
            DeleteCommand = new RelayCommand<Expense>(DeleteExpense);
        }


        private void EditExpense(Expense expense)
        {
            if (expense == null) return;

            EditWindow editWindow = new EditWindow(expense);
            editWindow.Owner = Application.Current.MainWindow;
            editWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            editWindow.ShowDialog();

            RefreshExpenses();
        }

        private void DeleteExpense(Expense expense)
        {
            if (expense == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this expense?",
                "Delete confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                db.Expenses.Remove(expense);
                db.SaveChanges();
                RefreshExpenses();
            }
        }

        private void FilterExpenses()
        {
            var filtered = db.Expenses
                .Where(x => string.IsNullOrEmpty(SearchText) || x.Description.ToLower().Contains(SearchText.ToLower()))
                .ToList();

            Expenses.Clear();
            foreach (var exp in filtered)
                Expenses.Add(exp);

            OnPropertyChanged(nameof(Total));
        }

        public void RefreshExpenses()
        {
            Expenses.Clear();
            foreach (var expense in db.Expenses.ToList())
                Expenses.Add(expense);

            OnPropertyChanged(nameof(Total));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}