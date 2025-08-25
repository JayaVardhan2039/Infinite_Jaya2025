using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_Challenge_9.Models;

namespace Code_Challenge_9.Repository
{
   
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
        IEnumerable<Movie> GetByYear(int year);
        IEnumerable<Movie> GetByDirector(string directorName);
    }

}
