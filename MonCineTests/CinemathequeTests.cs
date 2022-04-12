using System;
using System.Collections.Generic;
using MonCine.Data;
using MonCine.Data.Classes;
using Moq;
using Xunit;

namespace MonCineTests
{
    public class CinemathequeTests
    {
        [Fact]
        public void Constructeur_Devrait_Creer_Instance_Quand_Administrateur_Non_Nul()
        {
            // Arrange
            Cinematheque cinematheque = new Cinematheque(
                null,
                new List<Abonne>(),
                new List<Film>(),
                new List<Categorie>(),
                new List<Acteur>(),
                new List<Realisateur>(),
                new List<Recompense>()
            );

            // Act et assert
            Assert.Throws<ArgumentNullException>(() => cinematheque);
        }
    }
}