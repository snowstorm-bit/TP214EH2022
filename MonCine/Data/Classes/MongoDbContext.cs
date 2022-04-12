using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MonCine.Data.Classes
{
    public class MongoDbContext
    {
        private IMongoDatabase _bd;

        public MongoDbContext(IMongoDatabase pBd)
        {
            _bd = pBd;
        }

        /// <summary>
        /// Permet d'obtenir une collection pour le type du document spécifié.
        /// </summary>
        /// <typeparam name="TDocument">Type du document</typeparam>
        /// <returns>La collection pour le type du document spécifié.</returns>
        public IMongoCollection<TDocument> ObtenirCollection<TDocument>() =>
            _bd.GetCollection<TDocument>($"{typeof(TDocument).Name}s");

        /// <summary>
        /// Permet d'obtenir la liste de tous les documents contenus dans la collection pour le type du document spécifié.
        /// </summary>
        /// <typeparam name="TDocument">Type du document</typeparam>
        /// <returns>La liste de tous les documents contenus dans la collection pour le type du document spécifié.</returns>
        public List<TDocument> ObtenirCollectionListe<TDocument>() =>
            ObtenirCollection<TDocument>().Aggregate().ToList();

        /// <summary>
        /// Permet d'obtenir une liste de documents provenant de la base de données de la
        /// cinémathèque pour le type du document spécifié selon le filtre reçu en paramètre.
        /// </summary>
        /// <typeparam name="TDocument">Type du document</typeparam>
        /// <param name="pFiltre">Filtre permettant de filtrer la liste des documents provenant de la base de données</param>
        /// <returns>La liste de tous les documents contenus dans la collection pour le type du document spécifié.</returns>
        public List<TDocument> ObtenirDocumentsFiltres<TDocument>(Expression<Func<TDocument, bool>> pFiltre) =>
            ObtenirCollection<TDocument>().FindSync(pFiltre).Current.ToList();

        /// <summary>
        /// Permet d'insérer dans la base de données de la cinémathèque le document spécifié en paramètre selon le type du document spécifié.
        /// </summary>
        /// <typeparam name="TDocument">Type du document</typeparam>
        /// <param name="pDocument">Document à insérer dans la base de données de la cinémathèque</param>
        /// <returns>Le document inséré dans la base de données de la cinémathèque.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public TDocument InsererUnDocument<TDocument>(TDocument pDocument)
        {
            try
            {
                ObtenirCollection<TDocument>().InsertOne(pDocument);
                return pDocument;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : InsererUnDocument - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet d'insérer dans la base de données de la cinémathèque les documents spécifiés en paramètre selon le type du document spécifié.
        /// </summary>
        /// <typeparam name="TDocument">Type du document</typeparam>
        /// <param name="pDocuments">Documents à insérer dans la base de données de la cinémathèque</param>
        /// <returns>Les documents insérés dans la base de données de la cinémathèque.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public List<TDocument> InsererPlusieursDocuments<TDocument>(List<TDocument> pDocuments)
        {
            try
            {
                ObtenirCollection<TDocument>().InsertMany(pDocuments);
                return pDocuments;
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
        /// <typeparam name="TDocument">Type du document</typeparam>
        /// <typeparam name="TField">Type du champs</typeparam>
        /// <param name="pFiltre">Expression permettant de déterminer quel sera le document à mettre à jour</param>
        /// <param name="pUpdateDefinitions">Liste des champs et de ses valeurs à mettre à jour</param>
        /// <returns>Le résultat suite la mise à jour du document.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public UpdateResult MAJUn<TDocument, TField>(Expression<Func<TDocument, bool>> pFiltre,
            List<(Expression<Func<TDocument, TField>> field, TField value)> pUpdateDefinitions)
        {
            try
            {
                var builder = Builders<TDocument>.Update;
                UpdateDefinition<TDocument> updateDefinition = null;

                foreach ((Expression<Func<TDocument, TField>> field, TField value) tuple in pUpdateDefinitions)
                {
                    updateDefinition = updateDefinition == null 
                        ? builder.Set(tuple.field, tuple.value) 
                        : updateDefinition.Set(tuple.field, tuple.value);
                }

                return ObtenirCollection<TDocument>().UpdateOne(pFiltre, updateDefinition);
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : MAJUn - Exception : {e.Message}");
            }
        }
    }
}