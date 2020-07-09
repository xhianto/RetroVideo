using RetroVideo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace RetroVideo.Services
{
    public class VideoService
    {
        public List<Genre> AlleGenres()
        {
            using (var db = new RetroVideoEntities())
            {
                return db.Genres.ToList();
            }
        }
        public List<Film> FindFilmByGenre(int? genreId)
        {
            List<Film> films = new List<Film>();
            using(var db = new RetroVideoEntities())
            {
                films = (from film in db.Films
                         where film.genreid == genreId
                         select film).ToList();
            }
            return films;
        }
        public Genre FindGenreById(int? genreId)
        {
            Genre genre = new Genre();
            using (var db = new RetroVideoEntities())
            {
                genre = (from genr in db.Genres
                                where genr.id == genreId
                                select genr).FirstOrDefault();
            }
            return (genre);
        }
        public Film FindFilmById(int id)
        {
            using (var db = new RetroVideoEntities())
            {
                return db.Films.Find(id);
            }
        }
        public List<Klant> KlantZoeken(string deelnaam)
        {
            List<Klant> klanten = new List<Klant>();
            using (var db = new RetroVideoEntities())
            {
                klanten = (from klant in db.Klanten
                           where klant.familienaam.Contains(deelnaam)
                           orderby klant.familienaam
                           select klant).ToList();
            }
            return klanten;
        }
        public Klant FindKlantById(int id)
        {
            using (var db = new RetroVideoEntities())
            {
                return db.Klanten.Find(id);
            }
        }
        public void BewaarReservatie(Reservatie reservatie)
        {
            using (var db = new RetroVideoEntities())
            {
                Film film = db.Films.Find(reservatie.filmid);
                film.gereserveerd += 1;
                db.Reservaties.Add(reservatie);
                db.SaveChanges();
            }
        }
        public void DataBaseResetten()
        {
            using (var db = new RetroVideoEntities())
            {
                List<Reservatie> reservaties = new List<Reservatie>();
                reservaties = db.Reservaties.ToList();
                foreach (Reservatie reservatie in reservaties)
                {
                    db.Reservaties.Remove(reservatie);
                }
                List<Film> films = new List<Film>();
                films = db.Films.ToList();
                foreach (Film film in films)
                {
                    film.gereserveerd = 0;
                }
                db.SaveChanges();
            }
        }
    }
}