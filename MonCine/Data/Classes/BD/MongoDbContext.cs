﻿#region MÉTADONNÉES

// Nom du fichier : MongoDbContext.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

#endregion

namespace MonCine.Data.Classes.BD
{
    /// <summary>
    /// Classe permettant de gérer une collection selon le type de document spécifié.
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class MongoDbContext<TDocument>
    {
        #region ATTRIBUTS

        /// <summary>
        /// Base de données MongoDB utilisée
        /// </summary>
        private readonly IMongoDatabase _bd;

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant la création de la gestion de la collection pour le type de document spécifié.
        /// </summary>
        /// <param name="pBd">Base de données MongoDB utilisée</param>
        public MongoDbContext(IMongoDatabase pBd)
        {
            _bd = pBd;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir une collection pour le type du document spécifié.
        /// </summary>
        /// <returns>La collection pour le type du document spécifié.</returns>
        public IMongoCollection<TDocument> ObtenirCollection() =>
            _bd.GetCollection<TDocument>($"{typeof(TDocument).Name}s");

        /// <summary>
        /// Permet d'obtenir la liste de tous les documents contenus dans la collection pour le type du document spécifié.
        /// </summary>
        /// <returns>La liste de tous les documents contenus dans la collection pour le type du document spécifié.</returns>
        public List<TDocument> ObtenirCollectionListe() =>
            ObtenirCollection().Aggregate().ToList();

        /// <summary>
        /// Permet d'obtenir une liste de documents provenant de la base de données de la
        /// cinémathèque pour le type du document spécifié selon le filtre et la liste des valeurs reçu en paramètre.
        /// </summary>
        /// <typeparam name="TField">Type du document</typeparam>
        /// <param name="pField">Filtre permettant de filtrer la liste des documents provenant de la base de données</param>
        /// <param name="pObjects">Liste des objets à filtrer</param>
        /// <returns>La liste de tous les documents contenue dans la collection pour le type du document spécifié.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public List<TDocument> ObtenirDocumentsFiltres<TField>(Expression<Func<TDocument, TField>> pField,
            List<TField> pObjects)
        {
            try
            {
                return ObtenirCollection().Find(Builders<TDocument>.Filter.In(pField, pObjects)).ToList();
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : ObtenirDocumentsFiltres - Exception : {e.Message}");
            }
        }


        /// <summary>
        /// Permet d'insérer dans la base de données de la cinémathèque le document spécifié en paramètre selon le type du document spécifié.
        /// </summary>
        /// <param name="pDocument">Document à insérer dans la base de données de la cinémathèque</param>
        /// <returns>Le document inséré dans la base de données de la cinémathèque.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public void InsererUnDocument(TDocument pDocument)
        {
            try
            {
                ObtenirCollection().InsertOne(pDocument);
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : InsererUnDocument - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet d'insérer dans la base de données de la cinémathèque les documents spécifiés en paramètre selon le type du document spécifié.
        /// </summary>
        /// <param name="pDocuments">Documents à insérer dans la base de données de la cinémathèque</param>
        /// <returns>Les documents insérés dans la base de données de la cinémathèque.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public void InsererPlusieursDocuments(List<TDocument> pDocuments)
        {
            try
            {
                ObtenirCollection().InsertMany(pDocuments);
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : InsererPlusieursDocuments - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet de mettre à jour un seul document selon le type du document par un
        /// filtre et la liste des champs et de ses valeurs étant spéficiés en paramètre. 
        /// </summary>
        /// <typeparam name="TField">Type du champs</typeparam>
        /// <param name="pFiltre">Expression permettant de déterminer quel sera le document à mettre à jour</param>
        /// <param name="pMajDefinitions">Liste des champs et de ses valeurs à mettre à jour</param>
        /// <returns>Vrai si la mise à jour du document a fonctionné. Faux dans le cas contraire.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public bool MAJUn<TField>(Expression<Func<TDocument, bool>> pFiltre,
            List<(Expression<Func<TDocument, TField>> field, TField value)> pMajDefinitions)
        {
            try
            {
                var builder = Builders<TDocument>.Update;
                UpdateDefinition<TDocument> majDefinition = null;

                foreach ((Expression<Func<TDocument, TField>> field, TField value) in pMajDefinitions)
                {
                    majDefinition = majDefinition == null
                        ? builder.Set(field, value)
                        : majDefinition.Set(field, value);
                }

                return ObtenirCollection().UpdateOne(pFiltre, majDefinition).IsAcknowledged;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : MAJUn - MongoDbContext.cs - Exception : {e.Message}");
            }
        }

        #endregion
    }
}