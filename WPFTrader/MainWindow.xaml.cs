using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestionnaireBDD;
using MetierTrader;

namespace WPFTrader
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GstBdd unGst;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            unGst = new GstBdd();
            lstTraders.ItemsSource = unGst.getAllTraders();
        }

        private void lstTraders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstTraders.SelectedItem != null)
            {
                lstActions.ItemsSource = unGst.getAllActionsByTrader((lstTraders.SelectedItem as Trader).NumTrader);
                lstActionsNonPossedees.ItemsSource = unGst.getAllActionsNonPossedees((lstTraders.SelectedItem as Trader).NumTrader);
                txtTotalPortefeuille.Text = (unGst.getTotalPortefeuille((lstTraders.SelectedItem as Trader).NumTrader)).ToString();

            }
        }

        private void lstActions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstActions.SelectedItem != null)
            {
                //txtTotalPortefeuille.Text = (unGst.getTotalPortefeuille((lstTraders.SelectedItem as Trader).NumTrader)).ToString();
            }
        }

        private void btnVendre_Click(object sender, RoutedEventArgs e)
        {
            

            if(lstActions.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une action", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if(txtQuantiteVendue.Text == "")
                {
                    MessageBox.Show("Veuillez saisir une quantité", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    int qte = (lstActions.SelectedItem as ActionPerso).Quantite - Convert.ToInt16(txtQuantiteVendue.Text);
                    //unGst.SupprimerActionAcheter((lstActions.SelectedItem as ActionPerso).NumAction, (lstTraders.SelectedItem as Trader).NumTrader);
                    unGst.UpdateQuantite((lstActions.SelectedItem as ActionPerso).NumAction, (lstTraders.SelectedItem as Trader).NumTrader, qte); 
                }
                //unGst.SupprimerActionAcheter((lstActions.SelectedItem as ActionPerso).NumAction, (lstTraders.SelectedItem as Trader).NumTrader);
                //unGst.UpdateQuantite((lstActions.SelectedItem as ActionPerso).NumAction, (lstActions.SelectedItem as 
            }
        }

        private void btnAcheter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
