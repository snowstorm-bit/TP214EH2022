using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonCine.Data;
using MonCine.Data.Classes;
using System.Data;
using System.Windows.Navigation;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour FAbonnes.xaml
    /// </summary>
    public partial class FAbonnes : Page
    {
        private List<Abonne> abonnes;
        private DAL dal;
        private Cinematheque cinematheque;
        public FAbonnes(DAL pDal, Cinematheque pCinematheque)
        {
            InitializeComponent();
            dal = pDal;
            cinematheque = pCinematheque;
            cinematheque = dal.ObtenirCinematheque();
            abonnes = cinematheque.Abonnes;
            DataGridAbonnes.DataContext = abonnes;
        }
    }
}

