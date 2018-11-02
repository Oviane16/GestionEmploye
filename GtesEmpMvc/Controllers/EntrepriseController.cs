using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using GtesEmpMvc.Models;


namespace GtesEmpMvc.Controllers
{
    public class EntrepriseController : Controller
    {
        String Connexion_param = @"Data Source = localhost; Database = gestion_employe; User ID=root; Password='' ";
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;

        public ActionResult Index()
        {
            string sql = "SELECT * from entreprise order by id desc";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Entreprise>();
            while (reader.Read())
            {
                var entreprise = new Entreprise();
                entreprise.Design = reader["Design"].ToString();
                entreprise.raisonSociale = reader["raisonSociale"].ToString();
                entreprise.addresse = reader["addresse"].ToString();
                entreprise.id = reader["id"].ToString();
                entreprise.numEntreprise = reader["numEntreprise"].ToString();
                model.Add(entreprise);
            }
            conn.Close();
            return View(model); 
        }
        public ActionResult ajoutEntreprise()
        {
            Entreprise ent = new Entreprise();
            ent.Design = Convert.ToString(Request.Form["Design"]);
            ent.raisonSociale = Convert.ToString(Request.Form["raisonSociale"]);
            ent.addresse = Convert.ToString(Request.Form["addresse"]);
            ent.enregistEntreprise(ent);

            string sql = "SELECT * from entreprise order by id desc";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Entreprise>();
            while (reader.Read())
            {
                var entreprise = new Entreprise();
                entreprise.Design = reader["Design"].ToString();
                entreprise.raisonSociale = reader["raisonSociale"].ToString();
                entreprise.addresse = reader["addresse"].ToString();
                entreprise.id = reader["id"].ToString();
                entreprise.numEntreprise = reader["numEntreprise"].ToString();
                model.Add(entreprise);
            }
            conn.Close();
            return View(model);
        }
        public ActionResult test()
        {
            return View("test");
        }
        public ActionResult suppEntreprise()
        {
            String id = Convert.ToString(Request.Form["id"]);
            Entreprise ent = new Entreprise();
            ent.suppEnt(id);
            var model = ent.getList();
            return View(model);
        }
        public ActionResult modifierEntreprise()
        {
            String id = Convert.ToString(Request.Form["id"]);
            string sql = "SELECT * from entreprise where id='"+id+"'";
            conn = new MySqlConnection(Connexion_param);
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            var model = new List<Entreprise>();
            while (reader.Read())
            {
                var entreprise = new Entreprise();
                entreprise.Design = reader["Design"].ToString();
                entreprise.raisonSociale = reader["raisonSociale"].ToString();
                entreprise.addresse = reader["addresse"].ToString();
                entreprise.id = reader["id"].ToString();
                entreprise.numEntreprise = reader["numEntreprise"].ToString();
                model.Add(entreprise);
            }
            conn.Close();
            return View(model); 
        }
        public ActionResult enregistrerModifiaction()
        {

            String Design = Convert.ToString(Request.Form["Design"]);
            String raisonSociale = Convert.ToString(Request.Form["raisonSociale"]);
            String addresse = Convert.ToString(Request.Form["addresse"]);
            String id = Convert.ToString(Request.Form["id"]);
            Entreprise entrep = new Entreprise();
            entrep.Design = Design;
            entrep.raisonSociale = raisonSociale;
            entrep.addresse = addresse;
            entrep.id = id;
            entrep.enregistrerModifiaction(entrep);
     
            Entreprise ent = new Entreprise();
            var model = ent.getList();
            return View(model);
        }

    }
}
