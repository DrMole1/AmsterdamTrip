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
                result = await _connection.InsertAsync(new Museums { Name = _name, Image = _image, Address = _address, Hourly = _hourly, Expectation = _expectation, IsChecked = 0, Date = "" });

                StatusMessage = $"{result}  museum added : {_name}.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to add museum : {_name}.\n Erreur : {ex.Message}";
            }
        }

        public async Task<int> CheckMuseumAsync(Museums museum, int _isChecked, string _date)
        {
            int result = 0;

            try
            {
                museum.IsChecked = _isChecked;
                museum.Date = _date;
                result = await _connection.UpdateAsync(museum);

                StatusMessage = $"{result}  museum checked.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to check museum.\n Erreur : {ex.Message}";
            }

            return result;
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

        public async Task<int> DeleteMuseumAsync(Museums museum)
        {
            int result = 0;

            try
            {
                result = await _connection.DeleteAsync(museum);

                StatusMessage = $"{result}  museum deleted.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to delete museum.\n Erreur : {ex.Message}";
            }

            return result;
        }

        public async Task<int> UpdateMuseumAsync(Museums museum, string _name, byte[] _image, string _address, string _hourly, int _expectation)
        {
            int result = 0;

            try
            {
                museum.Name = _name;
                museum.Image = _image;
                museum.Address = _address;
                museum.Hourly = _hourly;
                museum.Expectation = _expectation;
                result = await _connection.UpdateAsync(museum);

                StatusMessage = $"{result}  museum updated.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to update museum.\n Erreur : {ex.Message}";
            }

            return result;
        }

        public async Task<int> AddPhotoMuseumAsync(Museums museum, int _slot, byte[] _photo)
        {
            int result = 0;

            try
            {
                if(_slot == 1) { museum.Photo_01 = _photo; }
                else if(_slot == 2) { museum.Photo_02 = _photo; }
                else if(_slot == 3) { museum.Photo_03 = _photo; }

                result = await _connection.UpdateAsync(museum);

                StatusMessage = $"{result}  photo added.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error to add photo.\n Erreur : {ex.Message}";
            }

            return result;
        }
    }
}
