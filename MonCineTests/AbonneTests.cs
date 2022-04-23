using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using Moq;
using MongoDB.Bson;
using MonCine.Data.Interfaces;

namespace MonCineTests
{
    public class AbonneTests
    {
        private static Random _rand = new Random();
        private List<Abonne> generationAbonnes()
        {
            int nbAbonnesGeneres = AbonneTests._rand.Next(6, 30);
            List<Abonne> abonnes = new List<Abonne>();
            for (int i = 0; i < nbAbonnesGeneres; i++)
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
                    .GetRange(0, AbonneTests._rand.Next(0, Preference.NB_MAX_CATEGORIES_PREF))
                    .ForEach(x => categoriesId.Add(x.Id));

                List<ObjectId> acteursId = new List<ObjectId>();
                acteurs
                    .GetRange(0, AbonneTests._rand.Next(0, Preference.NB_MAX_ACTEURS_PREF))
                    .ForEach(x => acteursId.Add(x.Id));

                List<ObjectId> realisateursId = new List<ObjectId>();
                realisateurs
                    .GetRange(0, AbonneTests._rand.Next(0, Preference.NB_MAX_REALISATEURS_PREF))
                    .ForEach(x => realisateursId.Add(x.Id));

                abonnes.Add(new Abonne
                (
                    new ObjectId(),
                    $"Utilisateur {i + 1}",
                    $"utilisateur{i + 1}@email.com",
                    $"user{i + 1}",
                    new Preference(
                        categoriesId,
                        acteursId,
                        realisateursId
                    )
                ));
            }
            return abonnes;
        }
        [Fact]
        public void ObtenirAbonnesSiTousLesAbonnesSontRetournes()
        {
            List<Abonne> abonnes = generationAbonnes();
            var abonneMock = new Mock<ICRUD<Abonne>>();

            abonneMock.Setup(x => x.ObtenirTout()).Returns(abonnes);
            var abonnesMock = abonneMock.Object.ObtenirTout();
            Assert.Equal(abonnesMock, abonnes);
        }
    }
}