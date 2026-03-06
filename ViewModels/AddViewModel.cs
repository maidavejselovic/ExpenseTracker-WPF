using ExpenseTracker.Commands;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels
{
    public class AddViewModel : INotifyPropertyChanged
    {
        private readonly ExpenseDbContext db = new ExpenseDbContext();

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string amount;
        public string Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private DateTime? expenseDate;
        public DateTime? ExpenseDate
        {
            get => expenseDate;
            set
            {
                expenseDate = value;
                OnPropertyChanged(nameof(ExpenseDate));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public bool IsPaid { get; set; }

        public ICommand SaveCommand { get; }

        public AddViewModel()
        {
            // Vezuje dugme Save sa metodama:
            // SaveExpense - šta dugme radi kada se klikne
            // CanSave - kada dugme može biti aktivno (enabled/disabled)
            SaveCommand = new RelayCommand<Window>(SaveExpense, CanSave);
        }

        private bool CanSave(Window window)
        {
            return !string.IsNullOrWhiteSpace(Description)
                && !string.IsNullOrWhiteSpace(Amount)
                && ExpenseDate != null;
        }

        private void SaveExpense(Window window)
        {
            if (!decimal.TryParse(Amount, out decimal amountValue))
            {
                MessageBox.Show("Amount must be a number");
                return;
            }

            Expense expense = new Expense
            {
                Description = Description,
                Amount = amountValue,
                ExpenseDate = ExpenseDate.Value,
                IsPaid = IsPaid
            };

            db.Expenses.Add(expense);
            db.SaveChanges();

            window.Close();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}