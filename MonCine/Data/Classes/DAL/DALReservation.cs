#region MÉTADONNÉES

// Nom du fichier : DALReservation.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

#endregion

namespace MonCine.Data.Classes.DAL
{
    /// <summary>
    /// Classe représentant une couche d'accès aux données pour les objets de type <see cref="Reservation"/>
    /// </summary>
    public class DALReservation : DAL<Reservation>
    {
        #region ATTRIBUTS

        /// <summary>
        /// Couche d'accès aux données pour les films
        /// </summary>
        private DALFilm _dalFilm;

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Permet la création de la couche d'accès aux données pour les objets de type <see cref="Reservation"/> selon les couches d'accès aux données spécifiés en paramètre.
        /// </summary>
        /// <param name="pDalCategorie">Couche d'accès aux données pour les catégories</param>
        /// <param name="pDalActeur">Couche d'accès aux données pour les acteurs</param>
        /// <param name="pDalRealisateur">Couche d'accès aux données pour les réalisateurs</param>
        /// <param name="pClient">L'interface client vers MongoDB</param>
        /// <param name="pDb">Base de données MongoDB utilisée</param>
        /// <remarks>Remarque : <em>Ce constructeur doit être utilisé lorsqu'il existe déjà une instance pour les couches d'accès aux données des catégories, des acteurs et des réalisateurs.</em></remarks>
        public DALReservation(DALCategorie pDalCategorie, DALActeur pDalActeur, DALRealisateur pDalRealisateur,
            IMongoClient pClient = null, IMongoDatabase pDb = null) : base(pClient, pDb)
        {
            DALCategorie dalCategorie = pDalCategorie;
            DALActeur dalActeur = pDalActeur;
            DALRealisateur dalRealisateur = pDalRealisateur;

            _dalFilm = new DALFilm(dalCategorie, dalActeur, dalRealisateur, MongoDbClient, Db);
        }

        /// <summary>
        /// Permet la création de la couche d'accès aux données pour les objets de type <see cref="Reservation"/> selon les couches d'accès aux données spécifiés en paramètre.
        /// </summary>
        /// <param name="pDalFilm">Couche d'accès aux données pour les films</param>
        /// <param name="pClient">L'interface client vers MongoDB</param>
        /// <param name="pDb">Base de données MongoDB utilisée</param>
        /// <remarks>Remarque : <em>Ce constructeur doit être utilisé lorsqu'il existe déjà une instance pour la couches d'accès aux données des films.</em></remarks>
        public DALReservation(DALFilm pDalFilm, IMongoClient pClient = null, IMongoDatabase pDb = null) : base(pClient,
            pDb)
        {
            _dalFilm = pDalFilm;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir la liste des réservations contenue dans la base de données de la cinémathèque.
        /// </summary>
        /// <returns>La liste des réservations contenue dans la base de données de la cinémathèque.</returns>
        public List<Reservation> ObtenirReservations()
        {
            return ObtenirObjetsDansReservations(DbContext.ObtenirCollectionListe());
        }

        /// <summary>
        /// Permet de filtrer les réservations contenues dans la base de données de la cinémathèque selon le champs et les valeurs spécifiés en paramètre.
        /// </summary>
        /// <typeparam name="TField">Type du champs sur lequel le filtrage sera effectué</typeparam>
        /// <param name="pField">Champs sur lequel le filtrage sera effectué</param>
        /// <param name="pObjects">Liste des valeurs à filtrer</param>
        /// <returns>La liste des réservations filtrée selon le champs et les valeurs spécifiés en paramètre.</returns>
        public List<Reservation> ObtenirReservationsFiltrees<TField>(Expression<Func<Reservation, TField>> pField,
            List<TField> pObjects)
        {
            return ObtenirObjetsDansReservations(DbContext.ObtenirDocumentsFiltres(pField, pObjects));
        }

        /// <summary>
        /// Permet d'obtenir les objets pour les attributs d'un <see cref="Reservation"/> faisant référence à une autre
        /// collection dans la base de données de la cinémathèque pour toute la liste de abonnés spécifiée en paramètre.
        /// </summary>
        /// <param name="pReservations">Liste des réservations</param>
        /// <returns>
        /// La liste des réservations dont les attributs faisant référence à une autre collection
        /// dans la base de données de la cinémathèque sont à présent définis par des objets non nul.
        /// </returns>
        private List<Reservation> ObtenirObjetsDansReservations(List<Reservation> pReservations)
        {
            List<ObjectId> filmIds = new List<ObjectId>();

            foreach (Reservation reservation in pReservations)
            {
                if (!filmIds.Contains(reservation.FilmId))
                {
                    filmIds.Add(reservation.FilmId);
                }
            }

            List<Film> films = _dalFilm.ObtenirFilmsFiltres(x => x.Id, filmIds);

            foreach (Reservation reservation in pReservations)
            {
                reservation.Film = films.Find(x => x.Id == reservation.FilmId);
            }

            return pReservations;
        }

        /// <summary>
        /// Permet d'insérer la liste des réservations reçue en paramètre dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pReservations">Liste des réservations à insérer dans la base de données</param>
        public void InsererPlusieursReservations(List<Reservation> pReservations)
        {
            foreach (Reservation reservation in pReservations)
            {
                MAJProjectionFilm(reservation.Film);
            }

            DbContext.InsererPlusieursDocuments(pReservations);
        }

        /// <summary>
        /// Permet de mettre à jour la liste des projections du film spécifié en paramètre. 
        /// </summary>
        /// <param name="pFilm">Film à mettre la liste des projections à jour</typeparam>
        private void MAJProjectionFilm(Film pFilm)
        {
            _dalFilm.MAJProjectionsFilm(pFilm);
        }

        #endregion
    }
}