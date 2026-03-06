using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.ViewModels;
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
            var vm = new AddViewModel();
            DataContext = vm;
        }
    }
}
        