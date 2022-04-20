#region MÉTADONNÉES

// Nom du fichier : SeedData.cs
// Date de création : 2022-04-18
// Date de modification : 2022-04-19

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MonCine.Data.Classes.DAL;
using MongoDB.Bson;
using MongoDB.Driver;

#endregion

namespace MonCine.Data.Classes.BD
{
    /// <summary>
    /// Classe permettant l'auto-génération des données
    /// </summary>
    public class SeedData
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Générateur de nombres pseudo-alatoire
        /// </summary>
        private static Random _rand = new Random();

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de générer toutes les données de base.
        /// </summary>
        /// <param name="pClient">L'interface client vers MongoDB</param>
        /// <param name="pDb">Base de données MongoDB utilisée</param>
        public static void GenererDonnees(IMongoClient pClient, IMongoDatabase pDb)
        {
            // Administrateur
            DALAdministrateur dalAdministrateur = new DALAdministrateur(pClient, pDb);
            SeedData.GenererAdministrateur(dalAdministrateur);

            // Catégories
            DALCategorie dalCategorie = new DALCategorie(pClient, pDb);
            SeedData.GenererCategories(dalCategorie);
            List<Categorie> categories = dalCategorie.ObtenirCategories();

            // Acteurs
            DALActeur dalActeur = new DALActeur(pClient, pDb);
            SeedData.GenererActeurs(dalActeur);
            List<Acteur> acteurs = dalActeur.ObtenirActeurs();

            // Réalisateurs
            DALRealisateur dalRealisateur = new DALRealisateur(pClient, pDb);
            SeedData.GenererRealisateurs(dalRealisateur);
            List<Realisateur> realisateurs = dalRealisateur.ObtenirRealisateurs();

            // Films
            DALFilm dalFilm = new DALFilm(dalCategorie, dalActeur, dalRealisateur, pClient, pDb);
            SeedData.GenererFilms(dalFilm, categories, acteurs, realisateurs);

            // Abonnées
            DALReservation dalReservation = new DALReservation(dalFilm, pClient, pDb);
            DALAbonne dalAbonne = new DALAbonne(dalCategorie, dalActeur, dalRealisateur, dalReservation, pClient, pDb);
            SeedData.GenererAbonnes(dalAbonne, categories, acteurs, realisateurs);

            // Notes
            SeedData.GenererNotes(dalFilm, dalFilm.ObtenirFilms(), dalAbonne.ObtenirAbonnes());

            // Réservations
            SeedData.GenererReservations(dalReservation, dalFilm.ObtenirFilms(), dalAbonne.ObtenirAbonnes());

            // Récompenses
            SeedData.GenererRecompenses(new DALRecompense(dalFilm, pClient, pDb), dalFilm.ObtenirFilms(), dalAbonne.ObtenirAbonnes());
        }

        /// <summary>
        /// Permet de générer un administrateur afin de l'insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalAdministrateur">Couche d'accès au donnés pour l'administrateur</param>
        /// <exception cref="IndexOutOfRangeException">Lancée lorsque la base de données de la cinémathèque contient plus de 1 administrateur.</exception>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererAdministrateur(DALAdministrateur pDalAdministrateur)
        {
            try
            {
                Administrateur administrateur = pDalAdministrateur.ObtenirAdministrateur();

                if (administrateur == null)
                    pDalAdministrateur.InsererAdministrateur(
                        new Administrateur(
                            new ObjectId(),
                            "Administrateur",
                            "admin@email.com",
                            "admin"
                        )
                    );
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
                throw new ExceptionBD($"Méthode : GenererAdministrateur - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet de générer des catégories afin de les insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalCategorie">Couche d'accès aux données pour les catégories</param>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererCategories(DALCategorie pDalCategorie)
        {
            try
            {
                List<Categorie> categories = pDalCategorie.ObtenirCategories();

                if (categories.Count == 0)
                    pDalCategorie.InsererPlusieursCategories(
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
                throw new ExceptionBD($"Méthode : GenererCategories - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet de générer des acteurs afin de les insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalActeur">Couche d'accès aux données pour les acteurs</param>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererActeurs(DALActeur pDalActeur)
        {
            try
            {
                List<Acteur> acteurs = pDalActeur.ObtenirActeurs();

                if (acteurs.Count == 0)
                    pDalActeur.InsererPlusieursActeurs(
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
                throw new ExceptionBD($"Méthode : GenererAbonnes - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet de générer des réalisateurs afin de les insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalRealisateur">Couche d'accès aux données pour les réalisateurs</param>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererRealisateurs(DALRealisateur pDalRealisateur)
        {
            try
            {
                List<Realisateur> realisateurs = pDalRealisateur.ObtenirRealisateurs();

                if (realisateurs.Count == 0)
                    pDalRealisateur.InsererPlusieursRealisateurs(
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
                throw new ExceptionBD($"Méthode : GenererRealisateurs - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet de générer des films afin de les insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalFilm">Couche d'accès aux données pour les films</param>
        /// <param name="pCategories">Liste des catégories</param>
        /// <param name="pActeurs">Liste des acteurs</param>
        /// <param name="pRealisateurs">Liste des réalisateurs</param>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererFilms(DALFilm pDalFilm, List<Categorie> pCategories, List<Acteur> pActeurs,
            List<Realisateur> pRealisateurs)
        {
            try
            {
                List<Film> films = pDalFilm.ObtenirFilms();

                if (!films.Any())
                {
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
                        films.Add(SeedData.GenererFilm(nom, pCategories, pActeurs, pRealisateurs));
                    }

                    // Génération de projection pour certains films choisis aléatoirement
                    for (int i = 0; i < films.Count; i++)
                    {
                        i = SeedData._rand.Next(i, films.Count - 1);
                        SeedData.GenererProjection(films[i], SeedData._rand.Next(30, 60));
                    }

                    pDalFilm.InsererPlusieursFilm(films);
                }
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : GenererFilms - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet de générer un film à selon les informations reçues en paramètre.
        /// </summary>
        /// <param name="pNom">Nom du film</param>
        /// <param name="pCategories">Catégorie du film</param>
        /// <param name="pActeurs">Liste des acteurs du film</param>
        /// <param name="pRealisateurs">Liste des réalisateurs du film</param>
        /// <returns>Le film généré selon les informations reçues en paramètres.</returns>
        private static Film GenererFilm(string pNom, List<Categorie> pCategories, List<Acteur> pActeurs,
            List<Realisateur> pRealisateurs)
        {
            DateTime dateSortie = DateTime.Now;
            dateSortie = dateSortie.AddYears(-1 * SeedData._rand.Next(30));

            List<ObjectId> acteursId = new List<ObjectId>();
            pActeurs
                .GetRange(0, SeedData._rand.Next(1, SeedData._rand.Next(2, pActeurs.Count)))
                .ForEach(x => acteursId.Add(x.Id));

            List<ObjectId> realisateursId = new List<ObjectId>();
            pRealisateurs
                .GetRange(0, SeedData._rand.Next(1, SeedData._rand.Next(2, pRealisateurs.Count)))
                .ForEach(x => realisateursId.Add(x.Id));

            Film film = new Film
            (
                new ObjectId(),
                pNom,
                dateSortie,
                new List<Projection>(),
                new List<Note>(),
                pCategories[SeedData._rand.Next(pCategories.Count - 1)].Id,
                acteursId,
                realisateursId
            );

            return film;
        }

        /// <summary>
        /// Permet de générer une projection pour le film et le nombre de places maximum reçus en paramètre. 
        /// </summary>
        /// <param name="pFilm">Film auquel il faut ajouter la projection</param>
        /// <param name="nbPlacesMax">nombre de place maximum pour la projection du film</param>
        private static void GenererProjection(Film pFilm, int nbPlacesMax)
        {
            DateTime dateDebut = DateTime.Now;
            int heureSuppDebut = SeedData._rand.Next(1, 23);

            dateDebut = dateDebut.AddHours(heureSuppDebut);
            DateTime dateFin = dateDebut.AddHours(SeedData._rand.Next(1, 23));

            pFilm.AjouterProjection(dateDebut, dateFin, nbPlacesMax);
        }

        /// <summary>
        /// Permet de générer des abonnés afin de les insérer dans la base de données.
        /// </summary>
        /// <param name="pDalAbonne">Couche d'accès aux données pour les abonnés</param>
        /// <param name="pCategories">Liste des catégories</param>
        /// <param name="pActeurs">Liste des acteusr</param>
        /// <param name="pRealisateurs">Liste des réalisateurs</param>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererAbonnes(DALAbonne pDalAbonne, List<Categorie> pCategories,
            List<Acteur> pActeurs, List<Realisateur> pRealisateurs)
        {
            try
            {
                List<Abonne> abonnes = pDalAbonne.ObtenirAbonnes();

                if (!abonnes.Any())
                {
                    int nbAbonnesGeneres = SeedData._rand.Next(6, 30);

                    for (int i = 0; i < nbAbonnesGeneres; i++)
                    {
                        abonnes.Add(SeedData.GenererAbonne(i + 1, pCategories, pActeurs, pRealisateurs));
                    }

                    pDalAbonne.InsererPlusieursAbonnes(abonnes);
                }
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
        /// Permet de générer un abonné selon les informations reçues en paramètre.
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="pCategories"></param>
        /// <param name="pActeurs"></param>
        /// <param name="pRealisateurs"></param>
        /// <returns></returns>
        private static Abonne GenererAbonne(int numero, List<Categorie> pCategories,
            List<Acteur> pActeurs, List<Realisateur> pRealisateurs)
        {
            List<ObjectId> categoriesId = new List<ObjectId>();
            pCategories
                .GetRange(0, SeedData._rand.Next(0, Preference.NB_MAX_CATEGORIES_PREF))
                .ForEach(x => categoriesId.Add(x.Id));

            List<ObjectId> acteursId = new List<ObjectId>();
            pActeurs
                .GetRange(0, SeedData._rand.Next(0, Preference.NB_MAX_ACTEURS_PREF))
                .ForEach(x => acteursId.Add(x.Id));

            List<ObjectId> realisateursId = new List<ObjectId>();
            pRealisateurs
                .GetRange(0, SeedData._rand.Next(0, Preference.NB_MAX_REALISATEURS_PREF))
                .ForEach(x => realisateursId.Add(x.Id));

            return new Abonne
            (
                new ObjectId(),
                $"Utilisateur {numero}",
                $"utilisateur_{numero}@email.com",
                $"user{numero}",
                new Preference(
                    categoriesId,
                    acteursId,
                    realisateursId
                )
            );
        }

        /// <summary>
        /// Permet de générer des notes afin de les insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalFilm">Couche d'accès aux données pour les films</param>
        /// <param name="pFilms">Liste des films dont il faut ajouter des notes</param>
        /// <param name="pAbonnes">Liste des abonnés</param>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererNotes(DALFilm pDalFilm, List<Film> pFilms, List<Abonne> pAbonnes)
        {
            try
            {
                foreach (Film film in pFilms)
                {
                    if (film.Notes.Count == 0 && pAbonnes.Count > 0)
                    {
                        int nbNotes = SeedData._rand.Next(1, pAbonnes.Count);
                        for (int i = 0; i < nbNotes; i++)
                        {
                            film.Notes.Add(new Note(pAbonnes[i].Id, SeedData._rand.Next(10)));
                        }

                        pDalFilm.MAJUnFilm(
                            x => x.Id == film.Id,
                            new List<(Expression<Func<Film, object>> field, object value)>
                            {
                                (x => x.Notes, film.Notes)
                            });
                    }
                }
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
        /// Permet de générer des réservations afin de les insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalReservation">Couche d'accès aux données pour les réservations</param>
        /// <param name="pFilms">Liste des films</param>
        /// <param name="pAbonnes">Liste des abonnés</param>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données se produit.</exception>
        private static void GenererReservations(DALReservation pDalReservation, List<Film> pFilms,
            List<Abonne> pAbonnes)
        {
            try
            {
                List<Reservation> reservations = pDalReservation.ObtenirReservations();

                if (!reservations.Any())
                {
                    int nbReservations = SeedData._rand.Next(0, pFilms.Count);

                    for (int i = 0; i < nbReservations; i++)
                    {
                        if (pFilms[i].Projections.Count > 0)
                        {
                            int indexFilm = SeedData._rand.Next(0, pFilms.Count - 1);
                            int indexProjection = pFilms[indexFilm].Projections.Count - 1;
                            int nbPlaces = SeedData._rand.Next(1, 10);

                            if (pFilms[indexFilm].Projections[indexProjection].NbPlacesRestantes - nbPlaces > -1)
                            {
                                reservations.Add(new Reservation(
                                    new ObjectId(),
                                    pFilms[indexFilm].Projections[indexProjection].DateDebut,
                                    pFilms[indexFilm],
                                    pAbonnes[SeedData._rand.Next(0, pAbonnes.Count - 1)].Id,
                                    nbPlaces
                                ));
                            }
                        }
                    }

                    if (reservations.Count > 0)
                    {
                        pDalReservation.InsererPlusieursReservations(reservations);
                    }
                }
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : GenererReservations - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet de générer des récompenses de type <see cref="TicketGratuit"/>.
        /// </summary>
        /// <param name="pFilms">Liste des films</param>
        /// <param name="pAbonnes">Liste des abonnés</param>
        /// <returns>Liste des récompenses de type <see cref="TicketGratuit"/> générée.</returns>
        private static List<TicketGratuit> GenererTicketGratuits(List<Film> pFilms, List<Abonne> pAbonnes)
        {
            List<TicketGratuit> ticketGratuits = new List<TicketGratuit>();
            int nbTicketGratuitsGeneres = SeedData._rand.Next(pFilms.Count);
            for (int i = 0; i < nbTicketGratuitsGeneres; i++)
            {
                ticketGratuits.Add(new TicketGratuit(
                    new ObjectId(),
                    pFilms[i].Id,
                    pAbonnes[SeedData._rand.Next(0, pAbonnes.Count - 1)].Id)
                );
            }

            return ticketGratuits;
        }

        /// <summary>
        /// Permet de générer des récompenses afin de les insérer dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pDalRecompense">Couche d'accès aux données pour les récompenses</param>
        /// <param name="pFilms">Liste des films</param>
        /// <param name="pAbonnes">Liste des abonnés</param>
        private static void GenererRecompenses(DALRecompense pDalRecompense, List<Film> pFilms, List<Abonne> pAbonnes)
        {
            try
            {
                List<Recompense> recompenses = pDalRecompense.ObtenirRecompenses();

                if (!recompenses.Any())
                {
                    recompenses.AddRange(SeedData.GenererTicketGratuits(pFilms, pAbonnes));

                    if (recompenses.Count > 0)
                        pDalRecompense.InsererPlusieursRecompenses(recompenses);
                }
            }
            catch (ExceptionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : GenererRecompenses - Exception : {e.Message}");
            }
        }
        #endregion
    }
}