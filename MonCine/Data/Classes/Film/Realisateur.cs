using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace MonCine.Data
{
    public class Realisateur
    {
        public ObjectId Id { get; set; }
        public string Nom { get; set; }

        public Realisateur(ObjectId pId, string pNom)
        {
            Id = pId;
            Nom = pNom;
        }
    }
}
