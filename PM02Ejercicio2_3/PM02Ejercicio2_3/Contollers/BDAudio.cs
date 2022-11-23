using PM02Ejercicio2_3.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM02Ejercicio2_3.Contollers
{
    public  class BDAudio
    {
        SQLiteAsyncConnection db;

        public BDAudio(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Audios>().Wait();
        }
        public Task<List<Audios>> ListaAudios()
        {
            return db.Table<Audios>().ToListAsync();
        }

        public Task<Audios> ReproducirAudio(int pid)
        {
            return db.Table<Audios>()
                .Where(i => i.id == pid).FirstOrDefaultAsync();
        }
        public Task<int> GrabarAudio(Audios audi)
        {
            if (audi.id != 0)
            {
                return db.UpdateAsync(audi);
            }
            else
            {
                return db.InsertAsync(audi);
            }
        }

        public Task<int> EliminaAudio(Audios audi)
        {
            return db.DeleteAsync(audi);
        }
    }
}
