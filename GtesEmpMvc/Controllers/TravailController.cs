using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using GtesEmpMvc.Models;
using GtesEmpMvc.ViewModel;


namespace GtesEmpMvc.Controllers
{
    public class TravailController : Controller
    {
        //
        // GET: /Travail/
        String Connexion_param = @"Data Source = localhost; Database = gestion_employe; User ID=root; Password='' ";
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;

        public ActionResult Index()
        {
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allEntreprise = _repository.getListEntreprise();
            vm.allTravail = _repository.getListTravail();
            return View(vm);
        }
        public ActionResult getListTravail()
        {
            Travail trav = new Travail();
            var model = trav.getList();
            return View(model);
        }

        public ActionResult ajoutTravail()
        {

             Travail trav = new Travail();
            trav.NomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            trav.Poste = Convert.ToString(Request.Form["poste"]);
            trav.TauxH = Convert.ToDouble(Request.Form["tauxH"]);
            trav.nbH = Convert.ToDouble(Request.Form["nbH"]);
            trav.enregistTravail(trav);
            Travail trav2 = new Travail();
            var model = trav2.getList();
            return View(model);
        }
        public ActionResult suppTravail()
        {
            String id = Convert.ToString(Request.Form["id"]);

           Travail trav3 = new Travail();
           trav3.suppTravai(id);

            var model = trav3.getList();
            return View(model);
        }
        public ActionResult modifierTravail()
        {
            String id = Convert.ToString(Request.Form["id"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allTravail = _repository.getOnTravail(id);
            vm.allEntreprise = _repository.getListEntreprise();
            return View(vm);
        }
        public ActionResult getListNomEnt()
        {
            Entreprise entreprise = new Entreprise();
            var model = entreprise.getList();
            return View(model);
        }
        public ActionResult enregistrerModifiaction()
        {
            Travail trav = new Travail();
            trav.NomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            trav.Poste = Convert.ToString(Request.Form["poste"]);
            trav.TauxH = Convert.ToDouble(Request.Form["tauxH"]);
            trav.nbH = Convert.ToDouble(Request.Form["nbH"]);
            trav.id = Convert.ToString(Request.Form["id"]);
            trav.enregistrerModifiaction(trav);

            Travail trav2 = new Travail();
            var model = trav2.getList();
            return View(model);
        }
    }
}
