using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace MonCine.Data
{
    public class Acteur
    {
        public ObjectId Id { get; set; }
        public string Nom { get; set; }

        public Acteur(ObjectId pId, string pNom)
        {
            Id = pId;
            Nom = pNom;
        }
    }
}
