﻿#region MÉTADONNÉES

// Nom du fichier : Film.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data.Classes
{
    public class Film
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        private const int NB_MAX_EST_AFFICHE_PAR_ANNEE = 2;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou défini l'identifiant du film
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Obtient ou défini le nom du film
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Obtient ou défini la date de sortie internationnale du film
        /// </summary>
        public DateTime DateSortie { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des projections du film
        /// </summary>
        public List<Projection> Projections { get; set; }

        // TODO : Créer une instance de la liste avant la mise à jour
        // TODO : Comparer si la liste a été modifié après l'ajout d'une projection
        // TODO :   SI OUI ALORS
        // TODO :       Enregistrer modif dans BD
        public List<DateTime> DatesFinsAffiche { get; set; }

        /// <summary>
        /// Obtient ou défini si le film est à l'affiche
        /// </summary>
        /// <remarks>
        /// Remarque : À l'obtention de la valeur, le film est à l'affiche si la liste contient au moins une projection,
        /// si la projection est active, si la date de fin de la projection est encore valide.
        /// Autrement dit, la valeur est à false.
        /// </remarks>
        [BsonIgnore]
        public bool EstAffiche
        {
            get
            {
                if (Projections != null && Projections.Count > 0)
                {
                    Projection derniereProjection = Projections[Projections.Count - 1];
                    if (derniereProjection.EstActive)
                    {
                        if (derniereProjection.DateFin < DateTime.Now)
                        {
                            DesactiverProjection();
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Obtient ou défini la liste des notes du film
        /// </summary>
        public List<Note> Notes { get; set; }

        /// <summary>
        /// Obtient ou défini la note moyenne du film
        /// </summary>
        /// <exception cref="ArgumentNullException">Lancée lorsque la liste des notes du films est nulle.</exception>
        [BsonIgnore]
        public double NoteMoy
        {
            get
            {
                if (Notes == null)
                {
                    throw new NullReferenceException(
                        "Il est impossible d'obtenir la note moyenne du film puisque la liste des notes est nulle."
                    );
                }

                int nbNotes = Notes.Count;
                return nbNotes > 0
                    ? Notes.Sum(pX => pX.NoteFilm) / nbNotes
                    : 0;
            }
        }

        /// <summary>
        /// Obtient ou défini l'identifiant de la catégorie du film
        /// </summary>
        public ObjectId CategorieId { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des identifiants des acteurs du film
        /// </summary>
        public List<ObjectId> ActeursId { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des identifiants des réalisateurs du film
        /// </summary>
        public List<ObjectId> RealisateursId { get; set; }

        /// <summary>
        /// Obtient ou défini la catégorie du film
        /// </summary>
        [BsonIgnore]
        public Categorie Categorie { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des acteurs du film
        /// </summary>
        [BsonIgnore]
        public List<Acteur> Acteurs { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des réalisateurs du film
        /// </summary>
        [BsonIgnore]
        public List<Realisateur> Realisateurs { get; set; }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant la création d'un film.
        /// </summary>
        /// <param name="pId">Identifiant du film</param>
        /// <param name="pNom">Nom du film</param>
        /// <param name="pDateSortie">Date de sortie internationnale du film</param>
        /// <param name="pProjections">Liste des projections du film</param>
        /// <param name="pNotes">Liste des notes du film</param>
        /// <param name="pCategorieId">Identifiant de la catégorie du film</param>
        /// <param name="pActeursId">Liste des identifiants des acteurs du film</param>
        /// <param name="pRealisateursId">Liste des identifiants des réalisateurs du film</param>
        public Film(ObjectId pId, string pNom, DateTime pDateSortie, List<Projection> pProjections, List<Note> pNotes,
            ObjectId pCategorieId, List<ObjectId> pActeursId, List<ObjectId> pRealisateursId)
        {
            Id = pId;
            Nom = pNom;
            DateSortie = pDateSortie;
            Projections = pProjections;
            Notes = pNotes;
            CategorieId = pCategorieId;
            ActeursId = pActeursId;
            RealisateursId = pRealisateursId;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de retirer un film à l'affiche.
        /// </summary>
        /// <returns>Vrai si le film n'est plus à l'affiche. Faux si l'opération n'a pas pu être effectuée.</returns>
        public bool RetirerAffiche()
        {
            if (Projections.Count > 0)
            {
                foreach (Projection projection in Projections)
                {
                    if (Projections[Projections.Count - 1] == projection)
                    {
                        DesactiverDerniereProjection();
                    }
                    else if (projection.EstActive)
                    {
                        projection.EstActive = false;
                    }
                }
                
                return true;
            }

            return false;
        }

        private void DesactiverDerniereProjection()
        {
            Projection derniereProjection = Projections[Projections.Count - 1];
            if (derniereProjection.EstActive)
            {
                derniereProjection.EstActive = false;
                DatesFinsAffiche.Add(derniereProjection.DateFin < DateTime.Now
                    ? derniereProjection.DateFin
                    : DateTime.Now
                );
            }
        }
        /// <summary>
        /// Permet d'ajouter une projection au film.
        /// </summary>
        /// <param name="pDateDebut">Date de début de la projection du film</param>
        /// <param name="pDateFin">Date de fin de la projection du film</param>
        /// <param name="pSalle">Salle de la projection</param>
        /// <returns>Vrai si l'ajout de la projection s'est effectué avec succès.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Lancée lorsque la date de début ou de fin de la projection est inférieure à la date de sortie internationnale du film.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Lancée à l'ajout d'une projection alors que le nombre de projections maximum par année du film est déjà atteint.
        /// </exception>
        public bool AjouterProjection(DateTime pDateDebut, DateTime pDateFin, Salle pSalle)
        {
            if (DateSortie > pDateDebut)
            {
                throw new ArgumentOutOfRangeException(
                    "pDateDebut",
                    "La date de début de la projection doit être supérieure à la date de sortie internationnale du film."
                );
            }

            if (DateSortie > pDateFin)
            {
                throw new ArgumentOutOfRangeException(
                    "pDateFin",
                    "La date de fin de la projection doit être supérieure à la date de sortie internationnale du film."
                );
            }

            if (pDateDebut < DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(
                    "pDateDebut",
                    "La date de début de la projection doit être supérieure à la date actuelle."
                );
            }

            // Gestion reprojection
            if (Projections.Count > 0)
            {
                Projection derniereProjection = Projections[Projections.Count - 1];
                if (derniereProjection.DateFin > pDateDebut)
                {
                    throw new ArgumentOutOfRangeException(
                        "pDateDebut",
                        "La date de début de la projection à ajouter doit être supérieure à la date de fin de la dernière projection du film."
                    );
                }
                // Si l'année est la même que la dernière projection désactivée,
                // Vérifie dans la liste des dates de fin à l'affiche du film si le nombre de fois à l'affiche est atteint
                // Exemple :
                // NB_MAX_EST_AFFICHE_PAR_ANNEE = 5, DatesFinAffiche.Count = 3 => Rentre PAS dans la boucle puisque le nombre maximum ne sera jamais atteint
                // NB_MAX_EST_AFFICHE_PAR_ANNEE = 5, DatesFinAffiche.Count = 7 => Rentre DANS la boucle puisque le nombre maximum pourrait potentiellement être atteint
                else if (pDateDebut.Year == derniereProjection.DateFin.Year &&
                         Film.NB_MAX_EST_AFFICHE_PAR_ANNEE < DatesFinsAffiche.Count)
                {
                    #region EXEMPLE ITÉRATIONS SUR BOUCLE

                    // NB_MAX_EST_AFFICHE_PAR_ANNEE = 3
                    // [0] = 2021
                    // [1] = 2022
                    // [2] = 2022
                    // [3] = 2022
                    // Année courante = 2022

                    // Nb. itération => 3 - 1 => 2
                    // Itération 1
                    // DatesFinsAffiche[(4) - 1 - (0)] => DatesFinsAffiche[3] => 2022 
                    // DatesFinsAffiche[(4) - 2 - (0)] => DatesFinsAffiche[2] => 2022
                    // Vrai
                    // Itération 2
                    // DatesFinsAffiche[(4) - 1 - (1)] => DatesFinsAffiche[2] => 2022
                    // DatesFinsAffiche[(4) - 2 - (1)] => DatesFinsAffiche[1] => 2022
                    // Vrai

                    #endregion

                    int i = 0;
                    while (
                        i < Film.NB_MAX_EST_AFFICHE_PAR_ANNEE - 1 &&
                        DatesFinsAffiche[DatesFinsAffiche.Count - 1 - i].Year ==
                        DatesFinsAffiche[DatesFinsAffiche.Count - 2 - i].Year
                    )
                    {
                        i++;
                    }

                    // À la première itération dans la boucle, la comparaison, si vrai, vaut pour 2
                    // Il faut donc ajouter + 1 pour arriver au bon nombre de date à l'affiche trouvé pour la même année 
                    if (i + 1 == Film.NB_MAX_EST_AFFICHE_PAR_ANNEE)
                    {
                        throw new InvalidOperationException(
                            $"Il est impossible d'ajouter une autre projection puisque celle-ci a déjà été projetée {Film.NB_MAX_EST_AFFICHE_PAR_ANNEE}");
                    }
                }
            }

            Projections.Add(new Projection(pDateDebut, pDateFin, pSalle));

            return true;
        }

        public override string ToString()
        {
            return Nom;
        }

        #endregion
    }
}