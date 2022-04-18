#region MÉTADONNÉES

// Nom du fichier : Preference.cs
// Date de création : 2022-04-10
// Date de modification : 2022-04-12

#endregion

#region USING

using System;
using System.Collections.Generic;
using MonCine.Data.Classes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data
{
    // TODO : Changer vecteur en liste
    public class Preference
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        public const int NB_MAX_CATEGORIES_PREF = 3;
        public const int NB_MAX_ACTEURS_PREF = 5;
        public const int NB_MAX_REALISATEURS_PREF = 5;

        #endregion

        #region ATTRIBUTS

        private List<ObjectId> _categoriesId;
        private List<ObjectId> _acteursId;
        private List<ObjectId> _realisateursId;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        public List<ObjectId> CategoriesId
        {
            get { return _categoriesId; }
            set
            {
                if (value.Count > Preference.NB_MAX_CATEGORIES_PREF)
                    throw new ArgumentOutOfRangeException(
                        $"Le nombre de catégories préférées est supérieur à {Preference.NB_MAX_CATEGORIES_PREF}");
                _categoriesId = value;
            }
        }

        public List<ObjectId> ActeursId
        {
            get { return _acteursId; }
            set
            {
                if (value.Count > Preference.NB_MAX_ACTEURS_PREF)
                    throw new ArgumentOutOfRangeException(
                        $"Le nombre d'acteurs préférés est supérieur à {Preference.NB_MAX_ACTEURS_PREF}");
                _acteursId = value;
            }
        }

        public List<ObjectId> RealisateursId
        {
            get { return _realisateursId; }
            set
            {
                if (value.Count > Preference.NB_MAX_REALISATEURS_PREF)
                    throw new ArgumentOutOfRangeException(
                        $"Le nombre de réalisateurs préférés est supérieur à {Preference.NB_MAX_REALISATEURS_PREF}");
                _realisateursId = value;
            }
        }

        [BsonIgnore] public List<Categorie> Categories { get; set; }
        [BsonIgnore] public List<Acteur> Acteurs { get; set; }
        [BsonIgnore] public List<Realisateur> Realisateurs { get; set; }

        #endregion

        #region CONSTRUCTEURS

        public Preference(List<ObjectId> pCategoriesId, List<ObjectId> pActeursId, List<ObjectId> pRealisateursId)
        {
            CategoriesId = pCategoriesId;
            ActeursId = pActeursId;
            RealisateursId = pRealisateursId;
        }

        #endregion


        #region Overrides of Object

        //public override string ToString()
        //{
        //}

        #endregion
    }
}