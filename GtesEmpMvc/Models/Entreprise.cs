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
    public class Entreprise
    {
        String Connexion_param = @"Data Source = localhost; Database = gestion_employe; User ID=root; Password='' ";
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;

        public String id { set; get; }
        public String raisonSociale { set; get; }
        public String Design { set; get; }
        public String addresse { set; get; }
        public String numEntreprise { set; get; }
        public Entreprise()
        {
        }

        public void enregistEntreprise(Entreprise ent)
        {
            String lastId = getlastId();
            String lastNum = getLastNum();
            String numEmp = "ENT001";
            if (lastNum!="")
            {
                Char[] tabLastNum = lastNum.ToCharArray();
                String un = Convert.ToString(tabLastNum[tabLastNum.Length - 1]);
                String deux = Convert.ToString(tabLastNum[tabLastNum.Length - 2]);
                String troi = Convert.ToString(tabLastNum[tabLastNum.Length - 3]);
                String chiffre = troi + deux + un;
                int chiffreInt = Convert.ToInt32(chiffre);
                String EMP = "ENT";
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
                String query = "insert into entreprise(numEntreprise,Design,addresse,raisonSociale) values('" + numEmp + "','" + ent.Design + "','" + ent.addresse + "','" + ent.raisonSociale + "') ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void enregistrerModifiaction(Entreprise entrep)
        {
            String nomEntreprise = entrep.Design;
            String enNomEnt = getNomEnt(entrep.id);

            updateEmploye(nomEntreprise, enNomEnt);
             updateTravail(nomEntreprise, enNomEnt);
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "update entreprise set Design ='" + entrep.Design + "',raisonSociale='" + entrep.raisonSociale + "', addresse='" + entrep.addresse + "' where id='" + entrep.id + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void updateTravail(String nomEntreprise, String enNomEnt)
        {
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "update travail set nomEntreprise ='" + nomEntreprise + "' where nomEntreprise='" + enNomEnt + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void updateEmploye(String nomEntreprise,String enNomEnt)
        {
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "update employe set nomEntreprise ='" + nomEntreprise + "' where nomEntreprise='" + enNomEnt + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        
        public List<Entreprise> getList()
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
        public String getNomEnt(String id)
        {
           String nomEnt = "";
            string sql = "SELECT * from entreprise where id='" + id + "'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var entreprise = new Entreprise();
                entreprise.Design = reader["Design"].ToString();
                nomEnt = entreprise.Design;
            }
            conn.Close();

            return nomEnt;
        }
        public void suppEnt(String id)
        {
            String nomEnt = getNomEnt(id); suppEmploye(nomEnt); suppTrav(nomEnt);
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "delete from entreprise where id='" + id + "' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void suppEmploye(String nomEnt)
        {
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "delete from employe where nomEntreprise='"+nomEnt+"' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void suppTrav(String nomEnt)
        {
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String query = "delete from travail where nomEntreprise='"+nomEnt+"' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public String getlastId()
        {
            String lastId = "";
            using (MySqlConnection cn = new MySqlConnection(Connexion_param))
            {
                String sql = "select * from entreprise order by id desc limit 0,1";
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
                String sql = "select * from entreprise order by id desc limit 0,1";
                conn = new MySqlConnection(Connexion_param);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lastId = reader["numEntreprise"].ToString();
                }
            }
            return lastId;
        }
    }
}