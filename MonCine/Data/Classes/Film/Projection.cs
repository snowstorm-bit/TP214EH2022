using System;
using System.Collections.Generic;
using System.Text;

namespace MonCine.Data
{
    public class Projection
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int NbPlacesMax { get; set; }
        public int NbPlacesRestantes { get; set; }

        public Projection(DateTime pDateDebut, DateTime pDateFin, int pNbPlacesMax)
        {
            DateDebut = pDateDebut;
            DateFin = pDateFin;
            NbPlacesMax = pNbPlacesMax;
        }
    }
}
