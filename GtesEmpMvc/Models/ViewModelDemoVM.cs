using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GtesEmpMvc.Models;

namespace GtesEmpMvc.ViewModel
{
    public class ViewModelDemoVM
    {
        public List<Entreprise> allEntreprise { set; get; }
        public List<Travail> allTraval { set; get; }
        public List<Employe> allEmploye { set; get; }
    }
}