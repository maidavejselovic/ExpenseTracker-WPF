using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System;
using System.Windows;

namespace ExpenseTracker.Windows
{
    public partial class AddWindow : Window
    {
        private readonly ExpenseDbContext db;

        public AddWindow()
        {
            InitializeComponent();
            db = new ExpenseDbContext();
        }

        // Funkcija koja proverava obavezna polja
        private void InputChanged(object sender, RoutedEventArgs e)
        {
            bool isDescriptionFilled = !string.IsNullOrWhiteSpace(txtDescription.Text);
            bool isAmountFilled = !string.IsNullOrWhiteSpace(txtAmount.Text);
            bool isDateFilled = !string.IsNullOrWhiteSpace(dpDate.Text);

            // Prikazi ili sakrij poruke o gresci
            txtDescriptionError.Visibility = isDescriptionFilled ? Visibility.Collapsed : Visibility.Visible;
            txtAmountError.Visibility = isAmountFilled ? Visibility.Collapsed : Visibility.Visible;
            dpDateError.Visibility = isDateFilled ? Visibility.Collapsed : Visibility.Visible;

            // Dugme Save je enabled samo ako su oba polja popunjena
            btnSave.IsEnabled = isDescriptionFilled && isAmountFilled && isDateFilled;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Amount must be a valid number!");
                return;
            }

            Expense expense = new Expense
            {
                Description = txtDescription.Text,
                Amount = amount,
                ExpenseDate = dpDate.SelectedDate ?? DateTime.Now,
                IsPaid = chkPaid.IsChecked ?? false
            };

            db.Expenses.Add(expense);
            db.SaveChanges();

            MessageBox.Show("Expense added!");
            this.Close();
        }
    }
}