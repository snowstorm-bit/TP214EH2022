using System;
using System.Collections.Generic;
using System.Text;
using MonCine.Data.Classes;
using MonCine.Data.Interfaces;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace MonCineTests
{
    public class FilmTests
    {
        private static Random _rand = new Random();
        private static void GenererFilms()
        {
            List<Categorie> categories = new List<Categorie>
                {
                    new(new ObjectId(), "Horreur"),
                    new(new ObjectId(), "Fantastique"),
                    new(new ObjectId(), "Comédie"),
                    new(new ObjectId(), "Action"),
                    new(new ObjectId(), "Romance")
                };

            List<Acteur> acteurs = new List<Acteur>
                {
                    new(new ObjectId(), "Zendaya"),
                    new(new ObjectId(), "Keanu Reeves"),
                    new(new ObjectId(), "Ahmed Toumi"),
                    new(new ObjectId(), "Marvin Laeib"),
                    new(new ObjectId(), "Le Grand Gwenaël"),
                    new(new ObjectId(), "Antoine Le Merveilleux"),
                    new(new ObjectId(), "Timoté"),
                    new(new ObjectId(), "Ptite petate"),
                    new(new ObjectId(), "Mélina Chaud")
                };

            List<Realisateur> realisateurs = new List<Realisateur>()
                {
                    new(new ObjectId(), "James Cameron"),
                    new(new ObjectId(), "Steven Spielberg"),
                    new(new ObjectId(), "Tim Burton"),
                    new(new ObjectId(), "Gary Ross"),
                    new(new ObjectId(), "Michael Bay")
                };

            List<ObjectId> categoriesId = new List<ObjectId>();
            categories
                .GetRange(0, 3)
                .ForEach(x => categoriesId.Add(x.Id));

            List<ObjectId> acteursId = new List<ObjectId>();
            acteurs
                .GetRange(0, 5)
                .ForEach(x => acteursId.Add(x.Id));

            List<ObjectId> realisateursId = new List<ObjectId>();
            realisateurs
                .GetRange(0, 5)
                .ForEach(x => realisateursId.Add(x.Id));
            List<Film> films = pDalFilm.ObtenirTout();

            List<string> nomsFilm = new List<string>
            {
                "Il faut sauver le soldat Ryan",
                "La Chute du faucon noir",
                "Dunkerque",
                "1917",
                "Fury",
                "American Sniper",
                "6 Underground",
                "Notice rouge",
            };

            // Génération des films
            foreach (string nom in nomsFilm)
            {
                films.Add(FilmTests.GenererFilm(nom, pCategories, pActeurs, pRealisateurs));
            }

            // Génération de projection pour certains films choisis aléatoirement
            for (int i = 0; i < films.Count; i += FilmTests._rand.Next(1, 3))
            {
            FilmTests.GenererProjections(films[i], pSalles);
            }

        }


        private static Film GenererFilm(string pNom, List<Categorie> pCategories, List<Acteur> pActeurs,
            List<Realisateur> pRealisateurs)
        {
            DateTime dateSortie = DateTime.Now;
            dateSortie = dateSortie.AddYears(-1 * FilmTests._rand.Next(30));

            List<ObjectId> acteursId = new List<ObjectId>();
            pActeurs
                .GetRange(0, FilmTests._rand.Next(1, FilmTests._rand.Next(2, pActeurs.Count)))
                .ForEach(x => acteursId.Add(x.Id));

            List<ObjectId> realisateursId = new List<ObjectId>();
            pRealisateurs
                .GetRange(0, FilmTests._rand.Next(1, FilmTests._rand.Next(2, pRealisateurs.Count)))
                .ForEach(x => realisateursId.Add(x.Id));

            Film film = new Film
            (
                new ObjectId(),
                pNom,
                dateSortie,
                new List<Projection>(),
                new List<Note>(),
                pCategories[FilmTests._rand.Next(pCategories.Count - 1)].Id,
                acteursId,
                realisateursId
            );

            return film;
        }

        /// <summary>
        /// Permet de générer une projection pour le film et la salle pour la projection du film reçus en paramètre. 
        /// </summary>
        /// <param name="pFilm">Film auquel il faut ajouter la projection</param>
        /// <param name="pSalle">Salle dans laquelle aura lieu la projection</param>
        private static void GenererProjections(Film pFilm, List<Salle> pSalles)
        {
            DateTime dateFin = DateTime.Now;
            int nbProjections = FilmTests._rand.Next(0, 20);
            for (int i = 0; i < nbProjections; i++)
            {
                int heureSuppDebut = FilmTests._rand.Next(1, 23);

                DateTime dateDebut = dateFin.AddHours(heureSuppDebut);
                dateFin = dateDebut.AddHours(FilmTests._rand.Next(1, 23));

                pFilm.AjouterProjection(dateDebut, dateFin, pSalles[FilmTests._rand.Next(0, pSalles.Count - 1)]);
            }
        }

        private List<Abonne> GenerationAbonnes()
        {
            List<Abonne> abonnes = new List<Abonne>();

            

            abonnes.Add(new Abonne
            (
                new ObjectId(),
                $"Utilisateur {1}",
                $"utilisateur{1}@email.com",
                $"user{1}",
                new Preference(
                    categoriesId,
                    acteursId,
                    realisateursId
                )
            ));
            abonnes.Add(new Abonne
              (
                  new ObjectId(),
                  $"Utilisateur {2}",
                  $"utilisateur{2}@email.com",
                  $"user{2}",
                  new Preference(
                      categoriesId,
                      acteursId,
                      realisateursId
                  )
              )); abonnes.Add(new Abonne
               (
                   new ObjectId(),
                   $"Utilisateur {3}",
                   $"utilisateur{3}@email.com",
                   $"user{3}",
                   new Preference(
                       categoriesId,
                       acteursId,
                       realisateursId
                   )
               )); abonnes.Add(new Abonne
               (
                   new ObjectId(),
                   $"Utilisateur {4}",
                   $"utilisateur{4}@email.com",
                   $"user{4}",
                   new Preference(
                       categoriesId,
                       acteursId,
                       realisateursId
                   )
               ));
            abonnes.Add(new Abonne
              (
                  new ObjectId(),
                  $"Utilisateur {5}",
                  $"utilisateur{5}@email.com",
                  $"user{5}",
                  new Preference(
                      categoriesId,
                      acteursId,
                      realisateursId
                  )
              ));
            abonnes.Add(new Abonne
              (
                  new ObjectId(),
                  $"Utilisateur {6}",
                  $"utilisateur{6}@email.com",
                  $"user{6}",
                  new Preference(
                      categoriesId,
                      acteursId,
                      realisateursId
                  )
              ));
            return abonnes;
        }















        //[Fact]
        //public void Constructeur_Devrait_Creer_Instance_Non_Vide()
        //{

        //}
        [Fact]
        public void ObtenirTousLesFilmEtVerifierTousRetournes()
        {
            List<Film> films = GenerationFilms();
            var filmMock = new Mock<ICRUD<Film>>();

            filmMock.Setup(x => x.ObtenirTout()).Returns(films);

            Assert.Equal(films, filmMock.Object.ObtenirTout());
        }
    }
}
