using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace MonCine.Data
{
    public class Categorie
    {
        public ObjectId Id { get; set; }
        public string Nom { get; set; }

        public Categorie(ObjectId pId, string pNom)
        {
            Id = pId;
            Nom = pNom;
        }
    }
}
