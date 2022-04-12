using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MonCine.Data.Classes;

namespace MonCine.Data
{
    public class Administrateur : Utilisateur
    {
        public Administrateur(ObjectId pId, string pNom, string pCourriel, string pMdp) : base(pId, pNom, pCourriel, pMdp)
        {
        }

        // TODO : COMPLÉTER LES MÉTHODES DE LA CLASSE ADMINISTRATEUR
    }
}
