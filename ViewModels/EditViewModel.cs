using ExpenseTracker.Commands;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels
{
    public class EditViewModel : INotifyPropertyChanged
    {
        private readonly ExpenseDbContext db = new ExpenseDbContext();
        private Expense expense;

        public EditViewModel(Expense expense)
        {
            this.expense = expense;

            Description = expense.Description;
            Amount = expense.Amount.ToString("0.##");
            ExpenseDate = expense.ExpenseDate;
            IsPaid = expense.IsPaid;

            SaveCommand = new RelayCommand<Window>(SaveExpense, CanSave);
        }

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

        private bool isPaid;
        public bool IsPaid
        {
            get => isPaid;
            set
            {
                isPaid = value;
                OnPropertyChanged(nameof(IsPaid));
            }
        }

        public ICommand SaveCommand { get; }

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

            expense.Description = Description;
            expense.Amount = amountValue;
            expense.ExpenseDate = ExpenseDate.Value;
            expense.IsPaid = IsPaid;

            db.Entry(expense).State = System.Data.Entity.EntityState.Modified;
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