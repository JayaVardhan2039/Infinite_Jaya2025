using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Code_Challenge_9.Repository;
using Code_Challenge_9.Models;

namespace Code_Challenge_9.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _repository;

        public MoviesController()
        {
            _repository = new MovieRepository();
        }

        // GET: Movies
        public ActionResult Index() => View(_repository.GetAll());

        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = _repository.GetById(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Delete(int id)
        {
            var movie = _repository.GetById(id);
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult SearchByYear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchByYear(int year)
        {
            return RedirectToAction("MoviesByYear", new { year });
        }
        public ActionResult MoviesByYear(int year)
        {
            var movies = _repository.GetByYear(year);
            ViewBag.Year = year;
            return View(movies);
        }

        public ActionResult SearchByDirector()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchByDirector(string directorName)
        {
            return RedirectToAction("MoviesByDirector", new { directorName });
        }

        public ActionResult MoviesByDirector(string directorName)
        {
            var movies = _repository.GetByDirector(directorName);
            ViewBag.Director = directorName;
            return View(movies);
        }

    }

}