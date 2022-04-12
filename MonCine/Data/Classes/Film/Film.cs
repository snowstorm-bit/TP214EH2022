using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MonCine.Data
{
    public class Film
    {
        public ObjectId Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateSortieInternationnale { get; set; }

        public bool Etat { get; set; }

        public List<Projection> Projections { get; set; }

        public List<Note> Notes { get; set; }
        public double NoteMoy { get; set; }

        public ObjectId CategorieId { get; set; }
        public List<ObjectId> ActeursId { get; set; }
        public List<ObjectId> RealisateursId { get; set; }

        [BsonIgnore] public Categorie Categorie { get; set; }
        [BsonIgnore] public List<Acteur> Acteurs { get; set; }
        [BsonIgnore] public List<Realisateur> Realisateurs { get; set; }

        public Film(ObjectId pId, string pNom, DateTime pDateSortieInternationnale, bool pEtat,
            List<Projection> pProjections, double pNoteMoy, ObjectId pCategorieId, List<ObjectId> pActeursId,
            List<ObjectId> pRealisateursId)
        {
            Id = pId;
            Nom = pNom;
            DateSortieInternationnale = pDateSortieInternationnale;
            Etat = pEtat;
            Projections = pProjections;
            NoteMoy = pNoteMoy;
            CategorieId = pCategorieId;
            ActeursId = pActeursId;
            RealisateursId = pRealisateursId;
        }

        public bool Projeter(DateTime pDateDebut, DateTime pDateFin, int pNbPlacesMax)
        {
            // TODO : Compléter la méthode Projeter de Film
            return false;
        }
    }
}