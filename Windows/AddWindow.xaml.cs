using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.ViewModels;
using System;
using System.Windows;

namespace ExpenseTracker.Windows
{
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            DataContext = new AddViewModel();
        }
    }
}
        