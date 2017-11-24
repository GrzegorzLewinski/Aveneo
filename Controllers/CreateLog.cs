using NIP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace NIP.Controllers
{
    public class CreateLog
    {
        private DatabaseConnection db = new DatabaseConnection();
        LOGS log = new LOGS();

        public void SaveLogToDatabase(string idCompany)
        {   
            //Stworzenie nowego logu , przypisanie mu NIP/KSR/Regon należącego do firmy , przypisanie aktualnej daty i zapisanie do bazy danych.
            log.Data = DateTime.Now;
            log.Indentyfikator = idCompany;
            db.LOGS.Add(log);
            db.SaveChanges();
        }
        

        

    }
}