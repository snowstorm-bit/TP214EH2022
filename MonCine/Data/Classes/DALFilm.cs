//#region MÉTADONNÉES

//// Nom du fichier : DALFilm.cs
//// Date de création : 2022-04-14
//// Date de modification : 2022-04-14

//#endregion

//#region USING

//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using MongoDB.Driver;

//#endregion

//namespace MonCine.Data.Classes
//{
//    public class DALFilm : DAL<Film>
//    {
//        #region ATTRIBUTS


//        #endregion

//        #region PROPRIÉTÉS ET INDEXEURS


//        #endregion

//        #region CONSTRUCTEURS

//        public DALFilm(IMongoClient pClient = null) : base(pClient)
//        {
//        }

//        #endregion

//        public List<Film> ObtenirFilmsFiltres(Expression<Func<Film, bool>> pFiltre)
//        {
//            return DbContext.ObtenirDocumentsFiltres(pFiltre);
//        }

//        public List<Film> ObtenirFilms()
//        {
//            return DbContext.ObtenirCollectionListe();
//        }

//        public Film InsererFilm(Film pFilm)
//        {
//            return DbContext.InsererUnDocument(pFilm);
//        }

//        public List<Film> InsererPlusieursFilm(List<Film> pFilms)
//        {
//            return DbContext.InsererPlusieursDocuments(pFilms);
//        }

//        public UpdateResult MAJUnFilm<TField>(Expression<Func<Film, bool>> pFiltre,
//            List<(Expression<Func<Film, TField>> field, TField value)> pMAJDefinitions)
//        {
//            return DbContext.MAJUn(pFiltre, pMAJDefinitions);
//        }
//    }
//}