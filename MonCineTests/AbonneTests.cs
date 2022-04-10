using System;
using MonCine.Data;
using Xunit;

namespace MonCineTests
{
    public class AbonneTests
    {
        [Fact]
        public void Constructeur_Devrait_Creer_Abonne_Quand_Est_Valeur_Non_Nulle()
        {
            // Arrange, Act et Assert
            Assert.Throws<ArgumentNullException>(() => new Abonne());
        }
    }
}
