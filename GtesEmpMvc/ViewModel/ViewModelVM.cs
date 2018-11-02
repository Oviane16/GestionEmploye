using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GtesEmpMvc.Models;

namespace GtesEmpMvc.ViewModel
{
    public class ViewModelVM
    {
        public List<Entreprise> allEntreprise { set; get; }
        public List<Travail> allTravail { set; get; }
        public List<Employe> allEmploye { set; get; }
    }
}