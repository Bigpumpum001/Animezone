using System;
using SQLite;
using System.Threading.Tasks;
using AnimeZone.Models;
using AnimeZone.Views;
using static AnimeZone.Views.LogInPage;
using static AnimeZone.Views.SignUpPage;
using System.Collections;

namespace AnimeZone
{
    public class SQLiteHelper
    {

        public static int lastId { get; }
    
        int nlastId = AppData.lastId;
        private readonly SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Customer2>();
        }
        public Task<int> CreateCustomer2 (Customer2 customer2)
        {
            return db.InsertAsync(customer2);
        }
        public Task<List<Customer2>>ReadCustomers() 
        { 
            return db.Table<Customer2>().ToListAsync();
        
        }
        public async Task<Customer2> LoginAsync(string username, string password)
        {
            return await db.Table<Customer2>()
                           .Where(u => u.Name == username && u.Password == password)
                           .FirstOrDefaultAsync();
        }
     
        public async Task<Customer2> ReadLastLoggedInCustomer(int nlastId)
        {
            
            return await db.Table<Customer2>()
                           .Where(n => n.Id == nlastId)
                           .FirstOrDefaultAsync();
        }
        public async Task<Customer2> RegisterChecked(string namecheck2)
        {
           
            return await db.Table<Customer2>()
                           .Where(n => n.Name == namecheck2)
                           .FirstOrDefaultAsync();
        }

        public Task <int> UpdateCustomer2 (Customer2 customer2) 
        {
            return db.UpdateAsync(customer2);   
        }
        public Task<int> DeleteCustomer2 (Customer2 customer2) 
        { 
            return db.DeleteAsync(customer2);
        }

   
    }
    
}
