using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext() : base("MySqlConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ExpenseDbContext>());
        }
         
        public DbSet<Expense> Expenses { get; set; }
    }
}