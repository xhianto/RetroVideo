using Microsoft.Ajax.Utilities;
using RetroVideo.Models;
using RetroVideo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetroVideo.Controllers
{
    public class HomeController : Controller
    {
        VideoService videoService = new VideoService();
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;
            return View(videoService.AlleGenres());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]
        public PartialViewResult LijstFilms(int? id)
        {
            Genre genre = videoService.FindGenreById(id);
            ViewBag.bestaat = (genre != null);
            if (ViewBag.bestaat)
            {
                ViewBag.Genre = genre.naam;
            }
            return PartialView(videoService.FindFilmByGenre(id));
        }
        public ActionResult Film(int id)
        {
            return View(videoService.FindFilmById(id));
        }
        [HttpPost]
        public ActionResult InMand(int id)
        {
            Session[id.ToString()] = 1;
            return RedirectToAction("Mandje");
        }
        public ActionResult Mandje()
        {
            List<Film> InMand = new List<Film>();
            foreach (string nummer in Session)
            {
                int filmId;
                if (int.TryParse(nummer, out filmId))
                {
                    Film film = videoService.FindFilmById(filmId);
                    if (film != null)
                    {
                        InMand.Add(film);
                    }
                }
            }
            return View(InMand);
        }
        [HttpPost]
        public ActionResult Verwijderen()
        {
            foreach (var item in Request.Form.AllKeys)
            {
                if (Session[item] != null)
                    Session.Remove(item);
            }
            return Redirect("Mandje");
        }
        public ActionResult Klant()
        {
            List<Klant> klanten = new List<Klant>();
            if (Request["deelnaam"] != null)
            {
                string deelnaam = Request["deelnaam"];
                klanten = videoService.KlantZoeken(deelnaam);
            }
            return View(klanten);
        }
        [HttpPost]
        public ActionResult Zoeken()
        {
            var deelnaam = Request["deelnaam"];
            this.TempData["deelnaam"] = deelnaam;
            return RedirectToAction("Klant");
        }
        public PartialViewResult LijstKlanten()
        {
            List<Klant> klanten = new List<Klant>();
            string deelnaam = string.Empty;
            if (this.TempData["deelnaam"] != null)
            {
                deelnaam = this.TempData["deelnaam"].ToString();
                klanten = videoService.KlantZoeken(deelnaam);
            }
            return PartialView(klanten);
        }
        public ActionResult Bevestigen(int id)
        {
            Klant klant = new Klant();
            klant = videoService.FindKlantById(id);
            ViewBag.naam = klant.voornaam + " " + klant.familienaam;
            ViewBag.aantal = Session.Count;
            return View(klant);
        }
        public ActionResult Resultaat(int id)
        {
            Klant klant = new Klant();
            klant = videoService.FindKlantById(id);
            if (Session != null)
            {
                List<Film> gelukt = new List<Film>();
                List<Film> mislukt = new List<Film>();

                foreach (string nummer in Session)
                {
                    Reservatie reservatie = new Reservatie();
                    reservatie.filmid = int.Parse(nummer);
                    reservatie.klantid = klant.id;
                    reservatie.reservatie = DateTime.Now;

                    Film film = videoService.FindFilmById(reservatie.filmid);
                    if ((film.voorraad - film.gereserveerd) >= 1)
                    {
                        videoService.BewaarReservatie(reservatie);

                        gelukt.Add(film);
                    }
                    else
                    {
                        mislukt.Add(film);
                    }
                }
                Session.Clear();
                ViewBag.gelukt = gelukt;
                ViewBag.mislukt = mislukt;
            }
            return View();
        }
        public ActionResult Reset()
        {
            videoService.DataBaseResetten();
            return RedirectToAction("Index");
        }
    }
}