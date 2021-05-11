using System;
using System.Collections.Generic;
using cadastro_series.interfaces;

namespace cadastro_series
{
    public class SerieRepository : IRepository<Serie>
    {
        private List<Serie> seriesList = new List<Serie>();
        public List<Serie> List()
        {
            return seriesList;
        }
        public Serie ReturnById(int id)
        {
            return seriesList[id];
        }
        public void Insert(Serie serie)
        {
            seriesList.Add(serie);
        }
        public void Update(int id, Serie serie)
        {
            seriesList[id] = serie;
        }
        public void Delete(int id)
        {
            seriesList[id].Delete();
        }
        public int NextId()
        {
            return seriesList.Count;
        }
    }
}