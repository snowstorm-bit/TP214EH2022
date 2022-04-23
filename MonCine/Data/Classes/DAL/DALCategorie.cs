#region MÉTADONNÉES

// Nom du fichier : DALCategorie.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MonCine.Data.Classes.BD;
using MongoDB.Driver;

#endregion

namespace MonCine.Data.Classes.DAL
{
    /// <summary>
    /// Classe représentant une couche d'accès aux données pour les objets de type <see cref="Categorie"/>
    /// </summary>
    public class DALCategorie : DAL
    {
        #region CONSTRUCTEURS

        /// <summary>
        /// Permet la création de la couche d'accès aux données pour les objets de type <see cref="Categorie"/>
        /// </summary>
        /// <param name="pClient">L'interface client vers MongoDB</param>
        /// <param name="pDb">Base de données MongoDB utilisée</param>
        public DALCategorie(IMongoClient pClient = null, IMongoDatabase pDb = null) : base(pClient, pDb)
        {
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir la liste des catégories contenue dans la base de données de la cinémathèque.
        /// </summary>
        /// <returns>La liste des catégories contenue dans la base de données de la cinémathèque.</returns>
        public List<Categorie> ObtenirCategories()
        {
            return MongoDbContext.ObtenirCollectionListe<Categorie>(Db);
        }

        /// <summary>
        /// Permet de filtrer les catégories contenues dans la base de données de la cinémathèque selon le champs et les valeurs spécifiés en paramètre.
        /// </summary>
        /// <typeparam name="TField">Type du champs sur lequel le filtrage sera effectué</typeparam>
        /// <param name="pField">Champs sur lequel le filtrage sera effectué</param>
        /// <param name="pObjects">Liste des valeurs à filtrer/param>
        /// <returns>La liste des catégories filtrée selon le champs et les valeurs spécifiés en paramètre.</returns>
        public List<Categorie> ObtenirCategoriesFiltrees<TField>(Expression<Func<Categorie, TField>> pField,
            List<TField> pObjects)
        {
            return MongoDbContext.ObtenirDocumentsFiltres(Db, pField, pObjects);
        }

        /// <summary>
        /// Permet d'insérer la liste des catégories reçue en paramètre dans la base de données de la cinémathèque.
        /// </summary>
        /// <param name="pCategories">Liste des catégories à insérer dans la base de données</param>
        public void InsererPlusieursCategories(List<Categorie> pCategories)
        {
            MongoDbContext.InsererPlusieursDocuments(Db, pCategories);
        }

        #endregion
    }
}