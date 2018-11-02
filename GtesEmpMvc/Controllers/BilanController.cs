using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using GtesEmpMvc.Models;
using GtesEmpMvc.ViewModel;
using System.Web.Helpers;
using System.Web.UI.DataVisualization;
using Rotativa;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts;
using iTextSharp.text.html;
using RazorPDF;


namespace GtesEmpMvc.Controllers
{
    public class BilanController : Controller
    {
        //
        // GET: /Bilan/

        public ActionResult Index()
        {
            Entreprise ent = new Entreprise();
            var model = ent.getList();
            return View(model);
        }
        public ActionResult getInfoAndList()
        {
            String nomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allEntreprise = _repository.getOneEntreprise(nomEntreprise);
            vm.allEmploye = _repository.getListEmpForEnt(nomEntreprise);
            return View(vm);
        }
        public ActionResult getInfoAndListMoi()
        {
            String nomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allEntreprise = _repository.getOneEntreprise(nomEntreprise);
            vm.allEmploye = _repository.getListEmpForEnt(nomEntreprise);
            return View(vm);
        }
        public ActionResult getInfoAndListAnnee()
        {
            String nomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allEntreprise = _repository.getOneEntreprise(nomEntreprise);
            vm.allEmploye = _repository.getListEmpForEnt(nomEntreprise);
            return View(vm);
        }
        public ActionResult afficheDiagramme()
        {
            String nomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allEntreprise = _repository.getOneEntreprise(nomEntreprise);
            vm.allEmploye = _repository.getListEmpForEnt(nomEntreprise);

            return View(vm);

        }
        public ActionResult imprimer()
        {
            String nomEntreprise = Convert.ToString(Request.QueryString["nomEntreprise"]);
           return new Rotativa.UrlAsPdf("http://localhost:61070/Bilan/imprimerAction?nomEntreprise="+nomEntreprise) { FileName = "Bilan.pdf" };
        }
        public ActionResult imprimerAction()
        {
            String nomEntreprise = Convert.ToString(Request.QueryString["nomEntreprise"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();
            vm.allEntreprise = _repository.getOneEntreprise(nomEntreprise);
            vm.allEmploye = _repository.getListEmpForEnt(nomEntreprise);
            Response.Write(nomEntreprise);
            return View(vm);
        }
        public ActionResult pdf()
        {
            var model = new List<Employe>();
            Employe em = new Employe();
             model = em.getListEmploye();
            return new RazorPDF.PdfResult(model, "PDF");
        }
        public ActionResult afficheEntreDeux()
        {

            String nomEntreprise = Convert.ToString(Request.Form["nomEntreprise"]);
            Repository _repository = new Repository();
            ViewModelVM vm = new ViewModelVM();


            String start = Convert.ToString(Request.Form["start"]);
            String end = Convert.ToString(Request.Form["end"]);
            DateTime sdate = Convert.ToDateTime(start);
            DateTime edate = Convert.ToDateTime(end);
            TimeSpan ts = edate - sdate;
            int days = ts.Days;
            String d = days.ToString();

            vm.allEntreprise = _repository.getOneEntreprise(nomEntreprise);
            vm.allEmploye = _repository.getListEmpForEntB(nomEntreprise,days);

            return View(vm);
        }
    

    
    }
}
