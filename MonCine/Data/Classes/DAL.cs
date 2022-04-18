#region MÉTADONNÉES

// Nom du fichier : DAL.cs
// Date de création : 2022-04-12
// Date de modification : 2022-04-12

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

#endregion

namespace MonCine.Data.Classes
{
    public class DAL
    {
        #region ATTRIBUTS

        private IMongoClient _mongoDbClient;
        private IMongoDatabase _db;
        private Random _rand = new Random();
        private MongoDbContext _dbContext;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        public MongoDbContext DbContext
        {
            get { return _dbContext; }
        }

        #endregion

        #region CONSTRUCTEURS

        public DAL(IMongoClient pClient = null)
        {
            _mongoDbClient = pClient ?? OuvrirConnexion();
            _db = ObtenirBD();
            _dbContext = new MongoDbContext(_db);
        }

        #endregion

        #region MÉTHODES

        private IMongoClient OuvrirConnexion()
        {
            try
            {
                return new MongoClient("mongodb://localhost:27017/");
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : OuvrirConnexion - Exception : {e.Message}");
            }
        }

        private IMongoDatabase ObtenirBD()
        {
            try
            {
                return _mongoDbClient.GetDatabase("TP2DB");
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirBD - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet d'obtenir tous les abonnés contenu dans la base de données de la cinémathèque.
        /// </summary>
        /// <returns>La liste des abonnés contenu dans la base de données de la cinémathèque.</returns>
        /// <remarks>
        /// Remarque : S'il n'y a aucun abonné contenu dans la base de données, alors la
        /// méthode crée des abonnés et les ajoutes à la base de données.
        /// </remarks>
        private Utilisateur ObtenirAdministrateur()
        {
            try
            {
                List<Administrateur> administrateurs = _dbContext.ObtenirCollectionListe<Administrateur>();

                switch (administrateurs.Count)
                {
                    case > 1:
                        throw new IndexOutOfRangeException(
                            "La base de données contient plus d'un administrateur pour la cinémathèque");
                    case 1:
                        return administrateurs[0];
                    default:
                        return _dbContext.InsererUnDocument(
                            new Administrateur(
                                new ObjectId(),
                                "Administrateur",
                                "admin@email.com",
                                "admin"
                            )
                        );
                }
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirAdministrateur - Exception : {e.Message}");
            }
        }

        private List<Categorie> ObtenirCategories()
        {
            try
            {
                List<Categorie> categories = _dbContext.ObtenirCollectionListe<Categorie>();

                if (categories.Count > 0)
                    return categories;

                return _dbContext.InsererPlusieursDocuments(
                    new List<Categorie>
                    {
                        new(new ObjectId(), "Horreur"),
                        new(new ObjectId(), "Fantastique"),
                        new(new ObjectId(), "Comédie"),
                        new(new ObjectId(), "Action"),
                        new(new ObjectId(), "Romance")
                    }
                );
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirCategories - Exception : {e.Message}");
            }
        }

        private List<Acteur> ObtenirActeurs()
        {
            try
            {
                List<Acteur> acteurs = _dbContext.ObtenirCollectionListe<Acteur>();

                if (acteurs.Count > 0)
                    return acteurs;
                return _dbContext.InsererPlusieursDocuments(
                    new List<Acteur>
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
                    }
                );
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirActeurs - Exception : {e.Message}");
            }
        }

        private List<Realisateur> ObtenirRealisateurs()
        {
            try
            {
                List<Realisateur> realisateurs = _dbContext.ObtenirCollectionListe<Realisateur>();

                if (realisateurs.Count > 0)
                    return realisateurs;
                return _dbContext.InsererPlusieursDocuments(
                    new List<Realisateur>()
                    {
                        new(new ObjectId(), "James Cameron"),
                        new(new ObjectId(), "Steven Spielberg"),
                        new(new ObjectId(), "Tim Burton"),
                        new(new ObjectId(), "Gary Ross"),
                        new(new ObjectId(), "Michael Bay")
                    }
                );
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirRealisateurs - Exception : {e.Message}");
            }
        }

        private List<Projection> GenererProjection(int nbPlacesMax)
        {
            DateTime dateDebut = DateTime.Now;
            int heureSuppDebut = _rand.Next(1, 23);

            dateDebut = dateDebut.AddHours(heureSuppDebut);
            DateTime dateFin = dateDebut.AddHours(_rand.Next(1, 23));

            List<Projection> projections = new List<Projection>
            {
                new Projection(dateDebut.Date, dateFin.Date, _rand.Next(1, nbPlacesMax))
            };

            return projections;
        }

        private Film GenererFilm(string pNom, List<Categorie> pCategories, List<Acteur> pActeurs,
            List<Realisateur> pRealisateurs)
        {
            DateTime dateSortie = DateTime.Now;
            dateSortie = dateSortie.AddYears(-1 * _rand.Next(30));

            int nbPlacesMax = _rand.Next(30, 60);

            List<ObjectId> acteursId = new List<ObjectId>();
            pActeurs
                .GetRange(0, _rand.Next(0, _rand.Next(0, pActeurs.Count)))
                .ForEach(x => acteursId.Add(x.Id));

            List<ObjectId> realisateursId = new List<ObjectId>();
            pRealisateurs
                .GetRange(0, _rand.Next(0, _rand.Next(0, pRealisateurs.Count)))
                .ForEach(x => realisateursId.Add(x.Id));

            return new Film
            (
                new ObjectId(),
                pNom,
                dateSortie.Date,
                true,
                GenererProjection(nbPlacesMax),
                new List<Note>(),
                0,
                pCategories[_rand.Next(pCategories.Count - 1)].Id,
                acteursId,
                realisateursId
            );
        }

        private List<Film> ObtenirFilms(List<Categorie> pCategories, List<Acteur> pActeurs,
            List<Realisateur> pRealisateurs)
        {
            try
            {
                List<Film> films = _dbContext.ObtenirCollectionListe<Film>();

                if (films.Any()) return films;

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

                foreach (string nom in nomsFilm)
                {
                    films.Add(GenererFilm(nom, pCategories, pActeurs, pRealisateurs));
                }

                return _dbContext.InsererPlusieursDocuments(films);
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirFilms - Exception : {e.Message}");
            }
        }

        private List<Reservation> GenererReservations(List<Film> pFilms)
        {
            List<Reservation> reservations = new List<Reservation>();
            int nbReservations = _rand.Next(pFilms.Count);

            for (int i = 0; i < nbReservations; i++)
            {
                if (pFilms[i].Projections.Count > 0)
                {
                    int indexFilm = _rand.Next(0, pFilms.Count - 1);
                    int indexProjection = _rand.Next(pFilms[indexFilm].Projections.Count - 1);
                    int nbPlaces = _rand.Next(10);

                    if (pFilms[indexFilm].Projections[indexProjection].NbPlacesRestantes - nbPlaces > -1)
                    {
                        reservations.Add(new Reservation(
                            pFilms[indexFilm].Projections[indexProjection].DateDebut,
                            pFilms[indexFilm].Id,
                            nbPlaces
                        ));

                        _dbContext.MAJUn<Film, object>
                        (
                            x => x.Id == pFilms[i].Id,
                            new List<(Expression<Func<Film, object>> field, object value)>
                            {
                                (
                                    x => x.Projections[indexProjection].NbPlacesRestantes,
                                    pFilms[indexFilm].Projections[indexProjection].NbPlacesRestantes - nbPlaces
                                ),
                            }
                        );
                    }
                }
            }

            return reservations;
        }

        private void GenererNotes(List<Film> pFilm, List<Abonne> pAbonnes)
        {
            foreach (Film film in pFilm)
            {
                int nbNotes = _rand.Next(0, pAbonnes.Count);

                if (nbNotes > 0)
                {
                    int sommeNotes = 0;
                    for (int i = 0; i < nbNotes; i++)
                    {
                        int note = _rand.Next(10);
                        sommeNotes += note;
                        film.Notes.Add(new Note(pAbonnes[i].Id, note));
                    }

                    _dbContext.MAJUn<Film, object>(x => x.Id == film.Id,
                        new List<(Expression<Func<Film, object>> field, object value)>
                        {
                            (x => x.Notes, film.Notes),
                            (x => x.NoteMoy, sommeNotes / nbNotes)
                        });
                }
            }
        }

        private Abonne GenererAbonne(int numero, List<Categorie> pCategories,
            List<Acteur> pActeurs, List<Realisateur> pRealisateurs, List<Film> pFilms)
        {
            List<ObjectId> categoriesId = new List<ObjectId>();
            pCategories
                .GetRange(0, _rand.Next(0, Preference.NB_MAX_CATEGORIES_PREF))
                .ForEach(x => categoriesId.Add(x.Id));

            List<ObjectId> acteursId = new List<ObjectId>();
            pActeurs
                .GetRange(0, _rand.Next(0, Preference.NB_MAX_ACTEURS_PREF))
                .ForEach(x => acteursId.Add(x.Id));

            List<ObjectId> realisateursId = new List<ObjectId>();
            pRealisateurs
                .GetRange(0, _rand.Next(0, Preference.NB_MAX_REALISATEURS_PREF))
                .ForEach(x => realisateursId.Add(x.Id));

            return new Abonne
            (
                new ObjectId(),
                $"Utilisateur {numero}",
                $"utilisateur_{numero}@email.com",
                $"user{numero}",
                DateTime.Now,
                GenererReservations(pFilms),
                new List<ObjectId>(),
                new Preference(
                    categoriesId,
                    acteursId,
                    realisateursId
                )
            );
        }

        /// <summary>
        /// Permet d'obtenir tous les abonnés contenu dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pFilms">Liste des films pour créer des réservations</param>
        /// <returns>La liste des abonnés contenu dans la base de données de la cinémathèque.</returns>
        /// <remarks>
        /// Remarque : S'il n'y a aucun abonné contenu dans la base de données, alors la
        /// méthode crée des abonnés et les ajoutes à la base de données.
        /// </remarks>
        private List<Abonne> ObtenirAbonnes(List<Categorie> pCategories,
            List<Acteur> pActeurs, List<Realisateur> pRealisateurs, List<Film> pFilms)
        {
            try
            {
                List<Abonne> abonnes = _dbContext.ObtenirCollectionListe<Abonne>();

                if (abonnes.Any()) return abonnes;

                for (int i = 0; i < 6; i++)
                {
                    abonnes.Add(GenererAbonne(i + 1, pCategories, pActeurs, pRealisateurs, pFilms));
                }

                _dbContext.InsererPlusieursDocuments(abonnes);

                // Génère des notes pour les films une fois les abonnés créés.
                GenererNotes(pFilms, abonnes);

                return abonnes;
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirAbonnes - Exception : {e.Message}");
            }
        }
        private List<TicketGratuit> GenererTicketSGratuits(List<Film> pFilms)
        {
            List<TicketGratuit> ticketSGratuits = new List<TicketGratuit>();
            for (int i = 0; i < _rand.Next(pFilms.Count); i++)
            {
                ticketSGratuits.Add(new TicketGratuit(new ObjectId(), pFilms[i].Id));
            }

            return ticketSGratuits;
        }

        private List<Recompense> GenererRecompenses(List<Film> pFilms)
        {
            List<Recompense> recompenses = new List<Recompense>();
            for (int i = 0; i < _rand.Next(pFilms.Count); i++)
            {
                recompenses.Add(new TicketGratuit(new ObjectId(), pFilms[i].Id));
            }

            return recompenses;
        }

        private List<Recompense> ObtenirRecompenses(List<Film> pFilms)
        {
            try
            {
                List<Recompense> recompenses = new List<Recompense>();

                List<TicketGratuit> ticketsGratuits = _dbContext.ObtenirCollectionListe<TicketGratuit>();

                recompenses.AddRange(ticketsGratuits.Any()
                    ? ticketsGratuits
                    : _dbContext.InsererPlusieursDocuments(GenererTicketSGratuits(pFilms))
                );

                return recompenses;
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirAbonnes - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet d'obtenir tous les abonnés contenu dans la base de données de la cinémathèque.
        /// </summary>
        /// <returns>La liste des abonnés contenu dans la base de données de la cinémathèque.</returns>
        /// <remarks>
        /// Remarque : S'il n'y a aucun abonné contenu dans la base de données, alors la
        /// méthode crée des abonnés et les ajoutes à la base de données.
        /// </remarks>
        public Cinematheque ObtenirCinematheque()
        {
            try
            {
                List<Categorie> categories = ObtenirCategories();
                List<Acteur> acteurs = ObtenirActeurs();
                List<Realisateur> realisateurs = ObtenirRealisateurs();
                List<Film> films = ObtenirFilms(categories, acteurs, realisateurs);
                List<Recompense> recompenses = ObtenirRecompenses(films);

                return new Cinematheque(
                    ObtenirAdministrateur(),
                    ObtenirAbonnes(categories, acteurs, realisateurs, films),
                    films,
                    categories,
                    acteurs,
                    realisateurs,
                    recompenses
                );
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirCinematheques - Exception : {e.Message}");
            }
        }

        #endregion
    }
}