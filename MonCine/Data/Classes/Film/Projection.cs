#region MÉTADONNÉES

// Nom du fichier : Projection.cs
// Date de création : 2022-04-12
// Date de modification : 2022-04-12

#endregion

#region USING

using System;

#endregion

namespace MonCine.Data.Classes
{
    public class Projection
    {
        #region PROPRIÉTÉS ET INDEXEURS

        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int NbPlacesMax { get; set; }
        public int NbPlacesRestantes { get; set; }

        #endregion

        #region CONSTRUCTEURS

        public Projection(DateTime pDateDebut, DateTime pDateFin, int pNbPlacesMax)
        {
            DateDebut = pDateDebut;
            DateFin = pDateFin;
            NbPlacesMax = pNbPlacesMax;
        }

        #endregion
    }
}