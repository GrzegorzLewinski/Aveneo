using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NIP.Models;
using System.Dynamic;
using System.Diagnostics;

namespace NIP.Controllers
{
    public class MainController : Controller
    {
        private Conversion conv = new Conversion();
        private CreateLog log = new CreateLog();
        private DatabaseConnection db = new DatabaseConnection();

        // GET: /Main/
        public ActionResult Index()
        {
            
            return View();
        }



        public PartialViewResult _companyDetails(string idCompany)
        {
            ViewBag.Message = "";
            FIRMA company = null;
            //Sprawdzenie czy podano jakiś format NIP'u
            if (idCompany.Length > 9)
            {   
                //przypisanie wszystkich formatów Nip'u do tablicy
                string[] arrayConvertedIdCompany = conv.ConvertNip(idCompany);

                //Sprawdzenie czy któryś z formatów widnieje w bazie danych
                for (int i = 0; i < 3 ; i++)
                {
                    if (company == null)
                    {
                        string convertedIdCompany = arrayConvertedIdCompany[i];
                        company = db.FIRMA.SingleOrDefault(f => f.Indentyfikator == convertedIdCompany);
                    }
                }
              
            }//Sprawdzenie czy podany ciąg jest Regonem
            else if (idCompany.Length == 9)
                company = db.FIRMA.SingleOrDefault(f => f.Indentyfikator == idCompany);
            
            //W przypadku gdy nie znaleziono przedsiębiorestwa zwracana jest informacje w ViewBag o braku firmy w bazie danych.
            if (company == null)
            {                
                ViewBag.Message = "Brak danych o Firmie/Przedsiębiorstwie";
                return PartialView();
            }
            //Tworzenie logu wyszukiwania po znalezieniu NIP/KSR/Regon w bazie danych
            log.SaveLogToDatabase(company.Indentyfikator);

            return PartialView(company);             
        }

        //Post:
        public PartialViewResult _listOfAllCompany()
        {
            //Zwrócenie listy wszystkich firm w bazie danych.
            return PartialView(db.FIRMA.ToList());
        }




        //Post:
        public PartialViewResult _listOfLog()
        {
            //Zwrócenie zapisanych logów.
            return PartialView(db.LOGS.ToList());
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
