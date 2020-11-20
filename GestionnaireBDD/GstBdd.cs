using MySql.Data.MySqlClient;
using System;
using MetierTrader;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        // Constructeur
        public GstBdd()
        {
            string chaine = "Server=localhost;Database=bourse;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        public List<Trader> getAllTraders()
        {
            List<Trader> mesTraders = new List<Trader>();
            cmd = new MySqlCommand("select idTrader, nomTrader from trader", cnx);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Trader unTrader = new Trader (Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                mesTraders.Add(unTrader);
            }
            dr.Close();
            return mesTraders;
        }
        public List<ActionPerso> getAllActionsByTrader(int numTrader)
        {
            List<ActionPerso> mesActionsPersos = new List<ActionPerso>();
            cmd = new MySqlCommand("SELECT numAction, nomAction, prixAchat, quantite, prixAchat*quantite as TOTAL from action a INNER JOIN acheter ac ON ac.numAction = a.idAction where numTrader =" + numTrader, cnx);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ActionPerso uneActionPerso = new ActionPerso(Convert.ToInt16(dr[0].ToString()), dr[1].ToString(), Convert.ToDouble(dr[2].ToString()), Convert.ToInt16(dr[3].ToString()), Convert.ToDouble(dr[4].ToString()));
                mesActionsPersos.Add(uneActionPerso);

            }
            dr.Close();
            return mesActionsPersos;
        }

        public List<MetierTrader.Action> getAllActionsNonPossedees(int numTrader)
        {
            List<MetierTrader.Action> mesActions = new List<MetierTrader.Action>();
            cmd = new MySqlCommand("select idAction, nomAction from action where idAction not in (select numAction from action a inner join acheter ac on ac.numAction = a.idAction where numTrader = " + numTrader + ")", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MetierTrader.Action uneAction = new MetierTrader.Action(Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                mesActions.Add(uneAction);
            }
            dr.Close();


            return mesActions;
        }

        public void SupprimerActionAcheter(int numAction, int numTrader)
        {
            cmd = new MySqlCommand("delete * from acheter where numAction = " + numAction + "and numTrader = " + numTrader, cnx);
            cmd.ExecuteNonQuery();
        }

        public void UpdateQuantite(int numAction, int numTrader, int quantite)
        {
            cmd = new MySqlCommand("update acheter set quantite = " + quantite+ " where numTrader =" + numTrader + " and numAction = " + numAction, cnx);

        }

        public double getCoursReel(int numAction)
        {
            return 0;
        }
        public void AcheterAction(int numAction, int numTrader, double prix, int quantite)
        {

        }
        public double getTotalPortefeuille(int numTrader)
        {
            double total;
            cmd = new MySqlCommand("SELECT SUM(prixAchat*quantite) AS total FROM acheter a INNER JOIN action ac ON ac.idAction = a.numAction where numTrader =" + numTrader, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            total = Convert.ToDouble(dr[0].ToString());
            dr.Close();
            return total;
        }
    }
}
