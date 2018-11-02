using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using GtesEmpMvc.Models;

namespace GtesEmpMvc.Models
{
    public class Repository
    {
        String Connexion_param = @"Data Source = localhost; Database = gestion_employe; User ID=root; Password=''; ";
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        public List<Entreprise> getListEntreprise()
        {
            string sql = "SELECT * from entreprise order by id desc";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Entreprise>();
            while (reader.Read())
            {
                var Entreprise = new Entreprise();
                Entreprise.Design = reader["Design"].ToString();
                Entreprise.raisonSociale = reader["raisonSociale"].ToString();
                Entreprise.addresse = reader["addresse"].ToString();
                Entreprise.id = reader["id"].ToString();
                Entreprise.numEntreprise = reader["numEntreprise"].ToString();
                model.Add(Entreprise);
            }
            return model;
        }
        public List<Entreprise> getOneEntreprise(String nomEntreprise)
        {
            string sql = "SELECT * from entreprise where Design= '"+nomEntreprise+"' ";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Entreprise>();
            while (reader.Read())
            {
                var Entreprise = new Entreprise();
                Entreprise.Design = reader["Design"].ToString();
                Entreprise.raisonSociale = reader["raisonSociale"].ToString();
                Entreprise.addresse = reader["addresse"].ToString();
                Entreprise.id = reader["id"].ToString();
                Entreprise.numEntreprise = reader["numEntreprise"].ToString();
                model.Add(Entreprise);
            }
            return model;
        }
        public List<Travail> getListTravail()
        {
          //  string sql = "SELECT * from travail order by id desc";
            string sql = "SELECT * from travail inner join entreprise on travail.nomEntreprise=entreprise.Design";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Travail>();
            while (reader.Read())
            {
                var Travail = new Travail();
                Travail.Poste = reader["nomTravail"].ToString();
                Travail.NomEntreprise = reader["nomEntreprise"].ToString();
                Travail.TauxH = Convert.ToDouble(reader["tauxhoraire"]);
                Travail.nbH = Convert.ToDouble(reader["nbHeure"]); ;
                Travail.Salaire = Convert.ToDouble(reader["salaire"]);
                Travail.NumTravail = reader["numTravail"].ToString();
                Travail.id = reader["id"].ToString();
                model.Add(Travail);
            }
            return model;
        }
        public List<Travail> getOnTravail(String id)
        {
            string sql = "SELECT * from travail where id='" + id + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Travail>();
            while (reader.Read())
            {
                var Travail = new Travail();
                Travail.Poste = reader["nomTravail"].ToString();
                Travail.NomEntreprise = reader["nomEntreprise"].ToString();
                Travail.TauxH = Convert.ToDouble(reader["tauxhoraire"]);
                Travail.nbH = Convert.ToDouble(reader["nbHeure"]); ;
                Travail.Salaire = Convert.ToDouble(reader["salaire"]);
                Travail.NumTravail = reader["numTravail"].ToString();
                Travail.id = reader["id"].ToString();
                model.Add(Travail);
            }
            return model;
        }
        public List<Employe> getListEmploye()
        {
            string sql = "SELECT * from employe INNER JOIN travail on employe.nomTravail = travail.nomTravail and employe.nomEntreprise = travail.nomEntreprise";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Employe>();
            while (reader.Read())
            {
                var Employe = new Employe();
                Employe.id = reader["id"].ToString();
                Employe.numEmploye = reader["numEmploye"].ToString();
                Employe.nomTravail = reader["nomTravail"].ToString();
                Employe.nom = reader["nom"].ToString();
                Employe.addresse = reader["addresse"].ToString();
                Employe.nomEntreprise = reader["nomEntreprise"].ToString();
              //  Employe.dateEmbauche = DateTime.Parse(reader["dateEmbauche"].ToString());
                Employe.salaire = Convert.ToDouble(reader["salaire"]);
                model.Add(Employe);
            }
            return model;
        }
        public List<Employe> getOneEmploye(String id)
        {
            string sql = "SELECT * from employe where id='" + id + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Employe>();
            while (reader.Read())
            {
                var Employe = new Employe();
                Employe.id = reader["id"].ToString();
                Employe.numEmploye = reader["numEmploye"].ToString();
                Employe.nom = reader["nom"].ToString();
                Employe.addresse = reader["addresse"].ToString();
                Employe.nomEntreprise = reader["nomEntreprise"].ToString();
                Employe.nomTravail = reader["nomTravail"].ToString();
                Employe.dateEmbauche = Convert.ToDateTime(reader["dateEmbauche"].ToString());
                model.Add(Employe);
            }
            return model;
        }
        public List<Employe> getListEmpForEnt(String nomEntreprise)
        {
            string sql = "SELECT * from employe INNER JOIN travail on employe.nomTravail = travail.nomTravail and employe.nomEntreprise = travail.nomEntreprise where employe.nomEntreprise='" + nomEntreprise + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Employe>();
            while (reader.Read())
            {
                var Employe = new Employe();
                Employe.id = reader["id"].ToString();
                Employe.nom = reader["nom"].ToString();
                Employe.id = reader["nomTravail"].ToString();
                Employe.addresse = reader["nomEntreprise"].ToString();
                Employe.salaire = Convert.ToDouble(reader["salaire"]);
                Employe.numEmploye = reader["numEmploye"].ToString();
                //String date = reader["dateEmbauche"].ToString();
               // Employe.dateEmbauche = DateTime.Parse(reader["dateEmbauche"].ToString());
                model.Add(Employe);
            }
            return model;
        }
        public List<Employe> getListEmpForEntB(String nomEntreprise,int days)
        {
            string sql = "SELECT * from employe INNER JOIN travail on employe.nomTravail = travail.nomTravail and employe.nomEntreprise = travail.nomEntreprise where employe.nomEntreprise='" + nomEntreprise + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Employe>();
            while (reader.Read())
            {
                var Employe = new Employe();
                Employe.id = reader["id"].ToString();
                Employe.nom = reader["nom"].ToString();
                Employe.id = reader["nomTravail"].ToString();
                Employe.addresse = reader["nomEntreprise"].ToString();
                Employe.salaire = Convert.ToDouble(reader["salaire"])*days;
                Employe.numEmploye = reader["numEmploye"].ToString();
                //String date = reader["dateEmbauche"].ToString();
                // Employe.dateEmbauche = DateTime.Parse(reader["dateEmbauche"].ToString());
                model.Add(Employe);
            }
            return model;
        }
    }
}