using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using GtesEmpMvc.ViewModel;
using GtesEmpMvc.Models;

namespace GtesEmpMvc.Controllers
{
    public class EmployeController : Controller
    {



        public ActionResult Index()
        {
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allTravail = _repository.getListTravail();
            vm.allEmploye = _repository.getListEmploye();
            return View(vm);
        }
        public ActionResult getNomEntreprise()
        {
            String nomtravail = Convert.ToString(Request.Form["nomTravail"]);
            Travail trav = new Travail();
            var model = trav.getListEntreprise(nomtravail);
            return View(model);
        }
        public ActionResult ajoutEmploye()
        {
            Employe emp = new Employe();
            emp.nom = Convert.ToString(Request.Form["nom"]);
            emp.nomEntreprise =  Convert.ToString(Request.Form["nomEntreprise"]);
            emp.nomTravail = Convert.ToString(Request.Form["nomTravail"]);
            emp.addresse = Convert.ToString(Request.Form["adresse"]);
         //   emp.dateEmbauche = Convert.ToDateTime(DateTime.Now.ToString("yyyy/mm/dd"));
            Response.Write(emp.dateEmbauche);
            emp.enregistEmploye(emp);
            Employe emp2 = new Employe();
            var model = emp2.getListEmploye();
            return View(model);
        }
        public ActionResult modifierEmploye()
        {
            String id = Convert.ToString(Request.Form["id"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allTravail = _repository.getListTravail();
            vm.allEmploye = _repository.getOneEmploye(id);
            vm.allEntreprise = _repository.getListEntreprise();
            return View(vm);
        }
        public ActionResult enregistrerModifiaction()
        {
            Employe emp = new Employe();
            emp.nom = Convert.ToString(Request.Form["nom"]);
            emp.addresse = Convert.ToString(Request.Form["adresse"]);
            emp.nomTravail = Convert.ToString(Request.Form["nomTravail"]);
            emp.nomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            emp.id = Convert.ToString(Request.Form["id"]);
            emp.enregistModification(emp);
            Employe emp2 = new Employe();
            var model = emp2.getListEmploye();
            return View(model);
        }
        public ActionResult suppEmploye()
        {
            Employe emp = new Employe();
            String id = Convert.ToString(Request.Form["id"]);
            emp.supp(id);
            Employe emp2 = new Employe();
            var model = emp2.getListEmploye();
            return View(model);
        }


    }
}
