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
using ExpenseTracker.Data;
using ExpenseTracker.Models;

namespace ExpenseTracker.Windows
{
    public partial class EditWindow : Window
    {
        private readonly ExpenseDbContext db;
        private Expense expense;

        public EditWindow(Expense expense)
        {
            InitializeComponent();

            db = new ExpenseDbContext();
            this.expense = expense;

            // popunjavanje polja
            txtDescription.Text = expense.Description;
            txtAmount.Text = expense.Amount.ToString();
            dpDate.SelectedDate = expense.ExpenseDate;
            chkPaid.IsChecked = expense.IsPaid;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Amount must be a number");
                return;
            }

            // update podataka
            expense.Description = txtDescription.Text;
            expense.Amount = amount;
            expense.ExpenseDate = dpDate.SelectedDate ?? DateTime.Now;
            expense.IsPaid = chkPaid.IsChecked ?? false;

            db.Entry(expense).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Expense updated!");
            this.Close();
        }
    }
}
