using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using GtesEmpMvc.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace GtesEmpMvc.Models
{
    public class Travail
    {
        String Connexion_param = @"Data Source = localhost; Database = gestion_employe; User ID=root; Password='' ";
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;

        public String id { set; get; }
        public String Poste { set; get; }
        public String NumTravail { set; get; }
        public String NomEntreprise { set; get; }
        public Double nbH { set; get; }
        public Double TauxH { set; get; }
        public Double Salaire { set; get; }
        public Travail()
        { 
            
        }
        // int k = Convert.ToInt32("32");
        // Convert.ToDouble()
        public void enregistTravail(Travail trav)
        {
            String lastId = getlastId();
            String lastNum = getLastNum();
            String numEmp = "TRAV001";
            if (lastNum != "")
            {
                Char[] tabLastNum = lastNum.ToCharArray();
                String un = Convert.ToString(tabLastNum[tabLastNum.Length - 1]);
                String deux = Convert.ToString(tabLastNum[tabLastNum.Length - 2]);
                String troi = Convert.ToString(tabLastNum[tabLastNum.Length - 3]);
                String chiffre = troi + deux + un;
                int chiffreInt = Convert.ToInt32(chiffre);
                String EMP = "TRAV";
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
          
            trav.Salaire = trav.TauxH*trav.nbH;

            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "insert into travail(nomTravail,nomEntreprise,tauxhoraire,nbHeure,salaire,numTravail) values('" + trav.Poste + "','" + trav.NomEntreprise + "','" + trav.TauxH + "','" + trav.nbH + "','" + trav.Salaire + "','" + numEmp + "') ";
                Console.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void suppTravai(String id)
        {
            String nomTrav = getNomTrav(id); String nomEnt = getNomEntreprise(id);
            suppEmploye(nomTrav, nomEnt);
               using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "delete from travail where id='" + id + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }

        }
        public void suppEmploye(String nomTrav,String nomEnt)
        {
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "delete from employe where nomtravail='" + nomTrav + "' and nomEntreprise='" + nomEnt + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public String getNomTrav(String id)
        {
            String nomTrav = "";
            string sql = "SELECT * from travail where id='" + id + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var travail = new Travail();
                travail.Poste = reader["nomtravail"].ToString();
                nomTrav = travail.Poste;
            }
            conn.Close();

            return nomTrav;
        }
        public String getNomEntreprise(String id)
        {
            String nomEnt = "";
            string sql = "SELECT * from travail where id='" + id + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var travail = new Travail();
                travail.NomEntreprise = reader["nomEntreprise"].ToString();
                nomEnt = travail.NomEntreprise;
            }
            conn.Close();

            return nomEnt;
        }
        public String getNumTravail()
        {
            String numtravail = null;
            string sql = "SELECT * from traval order by id desc limit 0,1";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var a = reader["id"]; ;
            }
            conn.Close();
            return numtravail;
        }
        public List<Travail> getList()
        {
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
        public List<Travail> getOnTravai(String id)
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
        public void enregistrerModifiaction(Travail trav)
        {
            trav.Salaire = trav.TauxH * trav.nbH;
            String enNomTrav = getNomTrav(trav.id); String enNomEnt = getNomEntreprise(trav.id);
            String nomTravail =trav.NumTravail;String nomEntreprise = trav.NomEntreprise;
            updateEmploye(enNomTrav, enNomEnt, nomTravail, nomEntreprise);
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "update travail set nomTravail ='" + trav.Poste + "',nomEntreprise='" + trav.NomEntreprise + "' ,tauxhoraire='" + trav.TauxH + "',nbHeure='" + trav.nbH + "',salaire='" + trav.Salaire + "' where id='" + trav.id + "'";
                Console.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void updateEmploye(String enNomTrav,String enNomEnt,String nomTravail,String nomEntreprise)
        {
            //  emp.salaire = getSalaire(emp.nomTravail);
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "update employe set nomEntreprise ='" + nomEntreprise + "',nomTravail='" + nomTravail + "' where nomEntreprise='" + enNomEnt + "' and nomTravail='" + enNomTrav + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public List<Travail> getListEntreprise(String nomTravail)
        {
            string sql = "SELECT * from travail where nomTravail='"+nomTravail+"'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Travail>();
            while (reader.Read())
            {
                var Travail = new Travail();
                Travail.NomEntreprise = reader["nomEntreprise"].ToString();
                model.Add(Travail);
            }
            conn.Close();
            return model;
        }
        public String getlastId()
        {
            String lastId = "";
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String sql = "select * from travail order by id desc limit 0,1";
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
            String lastNum = "";
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String sql = "select * from travail order by id desc limit 0,1";
                conn = new MySqlConnection(Connexion_param);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lastNum = reader["numTravail"].ToString();
                }
            }
            return lastNum;
        }
    }
}