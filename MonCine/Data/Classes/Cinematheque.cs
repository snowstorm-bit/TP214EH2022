#region MÉTADONNÉES

// Nom du fichier : Cinematheque.cs
// Date de création : 2022-04-12
// Date de modification : 2022-04-12

#endregion

#region USING

using System;
using System.Collections.Generic;

#endregion

namespace MonCine.Data.Classes
{
    public class Cinematheque
    {
        #region ATTRIBUTS

        /// <summary>
        /// Utilisateur courant de la cinémathèque
        /// </summary>
        private Utilisateur _utilisateurCourant;

        /// <summary>
        /// Liste des catégories de la cinémathèque
        /// </summary>
        private List<Categorie> _categories;

        /// <summary>
        /// Liste des acteurs de la cinémathèque
        /// </summary>
        private List<Acteur> _acteurs;

        /// <summary>
        /// Liste des réalisateurs de la cinémathèque
        /// </summary>
        private List<Realisateur> _realisateurs;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou défini la liste des abonnés de la cinémathèque
        /// </summary>
        public List<Abonne> Abonnes { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des films de la cinémathèque
        /// </summary>
        public List<Film> Films { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des récompenses de la cinémathèque
        /// </summary>
        public List<Recompense> Recompenses { get; set; }

        /// <summary>
        /// Obtient ou défini l'administrateur de la cinémathèque
        /// </summary>
        public Utilisateur UtilisateurCourant
        {
            get { return _utilisateurCourant; }
            set
            {
                _utilisateurCourant = value ?? throw new ArgumentNullException("UtilisateurCourant",
                    "L'utilisateur courant de la cinémathèque ne peut pas être nul.");
            }
        }

        /// <summary>
        /// Obtient la liste des catégories de la cinémathèque
        /// </summary>
        public List<Categorie> Categories
        {
            get { return _categories; }
        }

        /// <summary>
        /// Obtient la liste des acteurs de la cinémathèque
        /// </summary>
        public List<Acteur> Acteurs
        {
            get { return _acteurs; }
        }

        /// <summary>
        /// Obtient la liste des réalisateurs de la cinémathèque
        /// </summary>
        public List<Realisateur> Realisateurs
        {
            get { return _realisateurs; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur d'une cinémathèque pour une existant déjà.
        /// </summary>
        /// <param name="pAdmin">Administrateur de la cinémathèque</param>
        /// <param name="pAbonnes">Liste des abonnés de la cinémathèque</param>
        /// <param name="pFilms">Liste des films de la cinémathèque</param>
        /// <param name="pCategories">Liste des catégories d'une cinémathèque</param>
        /// <param name="pActeurs">Liste des acteurs d'une cinémathèque</param>
        /// <param name="pRealisateurs">Liste des réalisateurs d'une cinémathèque</param>
        /// <param name="pRecompenses">Liste des récompenses d'une cinémathèque</param>
        public Cinematheque(Utilisateur pAdmin, List<Abonne> pAbonnes, List<Film> pFilms,
            List<Categorie> pCategories, List<Acteur> pActeurs, List<Realisateur> pRealisateurs,
            List<Recompense> pRecompenses)
        {
            UtilisateurCourant = pAdmin;
            Abonnes = pAbonnes;
            Films = pFilms;
            _categories = pCategories;
            _acteurs = pActeurs;
            _realisateurs = pRealisateurs;
            Recompenses = pRecompenses;
        }

        #endregion
    }
}