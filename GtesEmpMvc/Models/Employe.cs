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
    public class Employe
    {
        String Connexion_param = @"Data Source = localhost; Database = gestion_employe; User ID=root; Password='' ";
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        public String id { set; get; }
        public String numEmploye { set; get; }
        public String nom { set; get; }
        public String addresse { set; get; }
        public DateTime dateEmbauche { set; get; }
        public String nomTravail { set; get; }
        public String nomEntreprise { set; get; }
        public Double salaire { set; get; }
        public Employe()
        {

        }
        public void enregistEmploye(Employe emp)
        {
            String lastId = getlastId();
            String lastNum = getLastNum();
            Char[] tabLastNum = lastNum.ToCharArray();
           String numEmp = "EMP001";
           if (lastNum !="")
           {
               String un = Convert.ToString(tabLastNum[tabLastNum.Length - 1]);
               String deux = Convert.ToString(tabLastNum[tabLastNum.Length - 2]);
               String troi = Convert.ToString(tabLastNum[tabLastNum.Length - 3]);
               String chiffre = troi + deux + un;
               int chiffreInt = Convert.ToInt32(chiffre);
               String EMP = "EMP";
               int chiffreApresEMP = chiffreInt + 1;
               if (chiffreApresEMP < 10)
               {
                   numEmp = EMP + "00" + Convert.ToString(chiffreApresEMP);
               }
               if (chiffreApresEMP < 100 && chiffreApresEMP > 9)
               {
                   numEmp = EMP + "0" + Convert.ToString(chiffreApresEMP);
               }
               if (chiffreApresEMP > 99 && chiffreApresEMP < 1000)
               {
                   numEmp = EMP + Convert.ToString(chiffreApresEMP);
               }
           }
            
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "insert into employe(numEmploye,nom,addresse,nomTravail,dateEmbauche,nomEntreprise) values('"+numEmp+"','" + emp.nom + "','" + emp.addresse + "','" + emp.nomTravail + "','" + emp.dateEmbauche + "','" + emp.nomEntreprise + "') ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void enregistModification(Employe emp)
        {
            //  emp.salaire = getSalaire(emp.nomTravail);
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "update employe set nom ='" + emp.nom + "',addresse='" + emp.addresse + "', nomTravail='" + emp.nomTravail + "', nomEntreprise='" + emp.nomEntreprise + "' where id='" + emp.id + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void supp(String id)
        {
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "delete from employe where id='" + id + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public double getSalaire(String nomTravail)
        {
            string sql = "SELECT salaire from travail where nomTravail='" + nomTravail + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            double Salaire = 0;
            while (reader.Read())
            {
                Salaire = Convert.ToDouble(reader["salaire"]);
            }
            return Salaire;
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
                Employe.dateEmbauche = DateTime.Parse(reader["dateEmbauche"].ToString());
                Employe.salaire = Convert.ToDouble(reader["dateEmbauche"]);
                model.Add(Employe);
            }
            return model;
        }
        public String getlastId()
        {
            String lastId = "";
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String sql = "select * from employe order by id desc limit 0,1";
                conn = new MySqlConnection(Connexion_param);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lastId = reader["id"].ToString();
                }
            }
            return lastId;
        }
        public String getLastNum()
        {
            String lastId = "";
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String sql = "select * from employe order by id desc limit 0,1";
                conn = new MySqlConnection(Connexion_param);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lastId = reader["numEmploye"].ToString();
                }
            }
            return lastId;
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
                //String date = reader["dateEmbauche"].ToString();
                Employe.dateEmbauche = DateTime.Parse(reader["dateEmbauche"].ToString());
                model.Add(Employe);
            }
            return model;
        }
    }
}