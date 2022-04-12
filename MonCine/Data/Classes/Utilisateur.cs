using System;
using System.Collections.Generic;
using System.Text;
using MonCine.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MonCine.Data.Classes
{
    public abstract class Utilisateur
    {
        public ObjectId Id { get; set; }
        public string Nom { get; set; }
        public string Courriel { get; set; }
        public string Mdp { get; set; }

        protected Utilisateur(ObjectId pId, string pNom, string pCourriel, string pMdp)
        {
            Id = pId;
            Nom = pNom;
            Courriel = pCourriel;
            Mdp = pMdp;
        }
    }
}
