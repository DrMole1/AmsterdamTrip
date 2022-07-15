using AmsterdamTrip.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AmsterdamTrip.Repositories
{
    public class MuseumsRepository
    {
        private SQLiteAsyncConnection _connection;

        public string StatusMessage { get; set; }

        public MuseumsRepository(string _dbPath)
        {
            _connection = new SQLiteAsyncConnection(_dbPath);

            // Create Table if not exist
            _connection.CreateTableAsync<Museums>();
        }

        public async Task AddNewMuseumAsync(string _name, byte[] _image, string _address, string _hourly, int _expectation)
        {
            int result = 0;

            try
            {
                result = await _connection.InsertAsync(new Museums { Name = _name, Image = _image, Address = _address, Hourly = _hourly, Expectation = _expectation });

                StatusMessage = $"{result}  museum added : {_name}.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to add museum : {_name}.\n Erreur : {ex.Message}";
            }
        }

        public async Task<List<Museums>> GetMuseumsAsync()
        {
            try
            {
                return await _connection.Table<Museums>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to get list of museums.\n Erreur : {ex.Message}";
            }

            return new List<Museums>();
        }

        public async Task ClearAllMuseumAsync()
        {
            int result = 0;

            try
            {
                result = await _connection.DropTableAsync<Museums>();

                StatusMessage = "All museums cleared !";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to clear museums. \n Erreur : {ex.Message}";
            }
        }
    }
}
