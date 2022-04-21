﻿#region MÉTADONNÉES

// Nom du fichier : Preference.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data.Classes
{
    /// <summary>
    /// Classe représentant une préférence d'un abonné.
    /// </summary>
    public class Preference
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Nombre maximum de catégories pouvant être sélectionnées
        /// </summary>
        public const int NB_MAX_CATEGORIES_PREF = 3;

        /// <summary>
        /// Nombre maximum d'acteurs pouvant être sélectionnés
        /// </summary>
        public const int NB_MAX_ACTEURS_PREF = 5;

        /// <summary>
        /// Nombre maximum de réalisateurs pouvant être sélectionnés
        /// </summary>
        public const int NB_MAX_REALISATEURS_PREF = 5;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Liste des identifiants des catégories préférées
        /// </summary>
        private List<ObjectId> _categoriesId;

        /// <summary>
        /// Liste des identifiants des acteurs préférés
        /// </summary>
        private List<ObjectId> _acteursId;

        /// <summary>
        /// Liste des identifiants des réalisateurs préférés
        /// </summary>
        private List<ObjectId> _realisateursId;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou défini la liste des identifiants des catégories préférées
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Lancée lorsque le nombre maximum de catégories préférées est supérieure à la constante définie.</exception>
        public List<ObjectId> CategoriesId
        {
            get { return _categoriesId; }
            set
            {
                if (value.Count > Preference.NB_MAX_CATEGORIES_PREF)
                {
                    throw new IndexOutOfRangeException(
                        $"Le nombre de catégories préférées est supérieur à {Preference.NB_MAX_CATEGORIES_PREF}");
                }

                _categoriesId = value;
            }
        }

        /// <summary>
        /// Obtient ou défini la liste des identifiants des acteurs préférés
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Lancée lorsque le nombre maximum de acteurs préférés est supérieure à la constante définie.</exception>
        public List<ObjectId> ActeursId
        {
            get { return _acteursId; }
            set
            {
                if (value.Count > Preference.NB_MAX_ACTEURS_PREF)
                {
                    throw new IndexOutOfRangeException(
                        $"Le nombre d'acteurs préférés est supérieur à {Preference.NB_MAX_ACTEURS_PREF}");
                }

                _acteursId = value;
            }
        }

        /// <summary>
        /// Obtient ou défini la liste des identifiants des réalisateurs préférés
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Lancée lorsque le nombre maximum de réalisateurs préférées est supérieure à la constante définie.</exception>
        public List<ObjectId> RealisateursId
        {
            get { return _realisateursId; }
            set
            {
                if (value.Count > Preference.NB_MAX_REALISATEURS_PREF)
                {
                    throw new IndexOutOfRangeException(
                        $"Le nombre de réalisateurs préférés est supérieur à {Preference.NB_MAX_REALISATEURS_PREF}");
                }

                _realisateursId = value;
            }
        }

        /// <summary>
        /// Liste des catégories préférées
        /// </summary>
        [BsonIgnore]
        public List<Categorie> Categories { get; set; }

        /// <summary>
        /// Liste des acteurs préférés
        /// </summary>
        [BsonIgnore]
        public List<Acteur> Acteurs { get; set; }

        /// <summary>
        /// Liste des réalisateurs préférés
        /// </summary>
        [BsonIgnore]
        public List<Realisateur> Realisateurs { get; set; }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant la création d'une préférence de l'abonné
        /// </summary>
        /// <param name="pCategoriesId">Liste des identifiants des catégories préférées</param>
        /// <param name="pActeursId">Liste des identifiants des acteurs préférés</param>
        /// <param name="pRealisateursId">Liste des identifiants des réalisateurs préférés</param>
        public Preference(List<ObjectId> pCategoriesId, List<ObjectId> pActeursId, List<ObjectId> pRealisateursId)
        {
            CategoriesId = pCategoriesId;
            ActeursId = pActeursId;
            RealisateursId = pRealisateursId;
        }

        #endregion

        #region MÉTHODES

        #region Overrides of Object

        public override string ToString()
        {
            string sautDeLigne = "\n";
            string aucun = "Aucun";
            string strFormate = sautDeLigne + "Catégories: ";
            if (Categories.Count > 0)
            {
                foreach (Categorie categorie in Categories)
                {
                    strFormate += categorie.Nom + " | ";
                }
            }
            else
            {
                strFormate += $"{aucun}e";
            }

            strFormate += sautDeLigne + "Acteurs: ";
            if (Acteurs.Count > 0)
            {
                foreach (Acteur acteur in Acteurs)
                {
                    strFormate += acteur.Nom + " | ";
                }
            }
            else
            {
                strFormate += aucun;
            }

            strFormate += sautDeLigne + "Réalisateurs: ";
            if (Realisateurs.Count > 0)
            {
                foreach (Realisateur realisateur in Realisateurs)
                {
                    strFormate += realisateur.Nom + " | ";
                }
            }
            else
            {
                strFormate += aucun;
            }

            return strFormate + sautDeLigne;
        }

        #endregion

        #endregion
    }
}