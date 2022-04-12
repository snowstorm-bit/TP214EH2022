using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
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
        /// <typeparam name="Document">Type du document</typeparam>
        /// <returns>La collection pour le type du document spécifié.</returns>
        public IMongoCollection<Document> ObtenirCollection<Document>() =>
            _bd.GetCollection<Document>($"{typeof(Document).Name}s");

        /// <summary>
        /// Permet d'obtenir la liste de tous les documents contenus dans la collection pour le type du document spécifié.
        /// </summary>
        /// <typeparam name="Document">Type du document</typeparam>
        /// <returns>La liste de tous les documents contenus dans la collection pour le type du document spécifié.</returns>
        public List<Document> ObtenirCollectionListe<Document>() =>
            ObtenirCollection<Document>().Aggregate().ToList();

        /// <summary>
        /// Permet d'insérer dans la base de données de la cinémathèque le document spécifié en paramètre selon le type du document spécifié.
        /// </summary>
        /// <typeparam name="Document">Type du document</typeparam>
        /// <param name="document">Document à insérer dans la base de données de la cinémathèque</param>
        /// <returns>Le document inséré dans la base de données de la cinémathèque.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public Document InsererUnDocument<Document>(Document document)
        {
            try
            {
                ObtenirCollection<Document>().InsertOne(document);
                return document;
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : InsererPlusieursDocuments - Exception : {e.Message}");
            }
        }

        /// <summary>
        /// Permet d'insérer dans la base de données de la cinémathèque les documents spécifiés en paramètre selon le type du document spécifié.
        /// </summary>
        /// <typeparam name="Document">Type du document</typeparam>
        /// <param name="documents">Documents à insérer dans la base de données de la cinémathèque</param>
        /// <returns>Les documents insérés dans la base de données de la cinémathèque.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public List<Document> InsererPlusieursDocuments<Document>(List<Document> documents)
        {
            try
            {
                ObtenirCollection<Document>().InsertMany(documents);
                return documents;
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
        /// <typeparam name="Document">Type du document</typeparam>
        /// <typeparam name="TField">Type du champs</typeparam>
        /// <param name="pFilter">Expression permettant de déterminer quel sera le document à mettre à jour</param>
        /// <param name="pUpdateDefinitions">Liste des champs et de ses valeurs à mettre à jour</param>
        /// <returns>Le résultat suite la mise à jour du document.</returns>
        /// <exception cref="ExceptionBD">Lancée lorsqu'une erreur liée à la base de données de la cinémathèque se produit.</exception>
        public UpdateResult MAJUn<Document, TField>(Expression<Func<Document, bool>> pFilter,
            List<(Expression<Func<Document, TField>> field, TField value)> pUpdateDefinitions)
        {
            try
            {
                var builder = Builders<Document>.Update;
                UpdateDefinition<Document> updateDefinition = null;

                foreach ((Expression<Func<Document, TField>> field, TField value) tuple in pUpdateDefinitions)
                {
                    updateDefinition = updateDefinition == null 
                        ? builder.Set(tuple.field, tuple.value) 
                        : updateDefinition.Set(tuple.field, tuple.value);
                }

                return ObtenirCollection<Document>().UpdateOne(pFilter, updateDefinition);
            }
            catch (Exception e)
            {
                throw new ExceptionBD($"Méthode : InsererPlusieursDocuments - Exception : {e.Message}");
            }
        }
    }
}