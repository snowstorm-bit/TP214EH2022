﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;

namespace MonCine.Data.Classes.DAL
{
    /// <summary>
    /// Classe représentant une couche d'accès aux données pour les objets de type <see cref="Realisateur"/>
    /// </summary>
    public class DALRealisateur : DAL<Realisateur>
    {
        #region CONSTRUCTEURS

        /// <summary>
        /// Permet la création de la couche d'accès aux données pour les objets de type <see cref="Realisateur"/>
        /// </summary>
        /// <param name="pClient">L'interface client vers MongoDB</param>
        /// <param name="pDb">Base de données MongoDB utilisée</param>
        public DALRealisateur(IMongoClient pClient = null, IMongoDatabase pDb = null) : base(pClient, pDb)
        {
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Permet d'obtenir la liste des réalisateurs contenue dans la base de données de la cinémathèque.
        /// </summary>
        /// <returns>La liste des réalisateurs contenue dans la base de données de la cinémathèque.</returns>
        public List<Realisateur> ObtenirRealisateurs()
        {
            return DbContext.ObtenirCollectionListe();
        }

        /// <summary>
        /// Permet de filtrer les réalisateurs contenues dans la base de données de la cinémathèque selon le champs et les valeurs spécifiés en paramètre.
        /// </summary>
        /// <typeparam name="TField">Type du champs sur lequel le filtrage sera effectué</typeparam>
        /// <param name="pField">Champs sur lequel le filtrage sera effectué</param>
        /// <param name="pObjects">Liste des valeurs à filtrer/param>
        /// <returns>La liste des réalisateurs filtrée selon le champs et les valeurs spécifiés en paramètre.</returns>
        public List<Realisateur> ObtenirRealisateursFiltres<TField>(Expression<Func<Realisateur, TField>> pField, List<TField> pObjects)
        {
            return DbContext.ObtenirDocumentsFiltres(pField, pObjects);
        }

        /// <summary>
        /// Permet d'insérer la liste des réalisateurs reçue en paramètre dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pRealisateurs">Liste des réalisateurs à insérer dans la base de données</param>
        public void InsererPlusieursRealisateurs(List<Realisateur> pRealisateurs)
        {
            DbContext.InsererPlusieursDocuments(pRealisateurs);
        }

        #endregion
    }
}