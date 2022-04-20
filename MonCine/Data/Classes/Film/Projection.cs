#region MÉTADONNÉES

// Nom du fichier : Projection.cs
// Date de création : 2022-04-14
// Date de modification : 2022-04-19

#endregion

#region USING

using System;

#endregion

namespace MonCine.Data.Classes
{
    public class Projection
    {
        #region ATTRIBUTS

        /// <summary>
        /// Date de fin de la projection
        /// </summary>
        private DateTime _dateFin;

        /// <summary>
        /// Nombre de places maximum de la projection
        /// </summary>
        private int _nbPlacesMax;

        /// <summary>
        /// Nombre de places restantes de la projection
        /// </summary>
        private int _nbPlacesRestantes;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou défini si la projection est active
        /// </summary>
        public bool EstActive { get; set; }

        /// <summary>
        /// Obtient ou défini la date de début de la projection
        /// </summary>
        public DateTime DateDebut { get; set; }

        /// <summary>
        /// Obtient ou défini la date de fin de la projection
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque valeur est supérieure à la date de début de la projection.</exception>
        public DateTime DateFin
        {
            get { return _dateFin; }
            set
            {
                if (DateDebut > value)
                    throw new ArgumentOutOfRangeException(
                        "La date de fin de la projection doit être supérieure à la date de début de la projection."
                    );
                _dateFin = value;
            }
        }

        /// <summary>
        /// Obtient ou défini le nombre de places maximum de la projection
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque la valeur est inférieure à 1.</exception>
        public int NbPlacesMax
        {
            get { return _nbPlacesMax; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(
                        "Le nombre de places maximum pour la projection doit être supérieur à 0."
                    );
                _nbPlacesMax = value;
            }
        }

        /// <summary>
        /// Obtient ou défini le nombre de places restantes de la projection
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque la valeur est négative.</exception>
        public int NbPlacesRestantes
        {
            get { return _nbPlacesRestantes; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(
                        "Il est impossible d'affectuer une valeur négative au nombre de places restantes de la projection"
                    );
                _nbPlacesRestantes = value;
            }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant la création d'une projection étant active.
        /// </summary>
        /// <param name="pDateDebut">Date de début de la projection</param>
        /// <param name="pDateFin">Date de fin de la projection</param>
        /// <param name="pNbPlacesMax">Nombre de places maximum de la projection</param>
        public Projection(DateTime pDateDebut, DateTime pDateFin, int pNbPlacesMax)
        {
            DateDebut = pDateDebut;
            DateFin = pDateFin;
            NbPlacesMax = pNbPlacesMax;
            NbPlacesRestantes = NbPlacesMax;
            EstActive = true;
        }

        #endregion
    }
}