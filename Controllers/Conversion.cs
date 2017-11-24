using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NIP.Controllers
{
    public class Conversion
    {
        /* klasa mająca na celu rozpoznanie formatu i zamiany na pozostałe formaty podanego Nip'u i zwrócenie ich w String Array.
         */
        public string[] ConvertNip(string id)
        {
            
            string countryNip=null, dashNip = null, normalNip = null;

            if (id.Length == 10 )//normalny nip 7777777777
            {
                countryNip = ConvertToCountryNip(id);
                dashNip = ConvertToDashNip(id);
                normalNip = id;
            }
            else if (id.Length == 12)// krajowy nip PL7777777777
            {
                normalNip = ConvertToNormalNip(id);
                dashNip = ConvertToDashNip(id);
                countryNip = id;
            }
            else if (id.Length == 13)// nip z myślnikami 777-777-77-77
            {
                normalNip = ConvertToNormalNip(id);
                countryNip = ConvertToCountryNip(id);
                dashNip = id;
            }
            

            string[] nip = { normalNip, dashNip, countryNip };

                return nip;
        }

        public string ConvertToCountryNip(string Nip)//return PL7777777777
        {
            string countryNip = null;

            if (Nip.Length == 10)//7777777777 -> PL7777777777
            {
                countryNip = "PL" + Nip;
            }
            else if(Nip.Length==13)//777-777-77-77 -> PL7777777777
            {
                countryNip = "PL" + Nip[0] + Nip[1] + Nip[2] + Nip[4] + Nip[5] + Nip[6] + Nip[8] + Nip[9] + Nip[10] + Nip[11];
            }
            return countryNip;
        }

        public string ConvertToDashNip(string Nip)//return 777-777-77-77
        {   
            string dashNip=null;

            if (Nip.Length == 10)//7777777777 -> 777-777-77-77
            {   
                dashNip =""+ Nip[0] + Nip[1] + Nip[2] + "-" + Nip[3] + Nip[4] + Nip[5] + "-" + Nip[6] + Nip[7] + "-" + Nip[8] + Nip[9];
            }
            else if (Nip.Length == 12)//PL7777777777 -> 777-777-77-77
            {
                dashNip =""+ Nip[2] + Nip[3] + Nip[4] + "-" + Nip[5] + Nip[6] + Nip[7] + "-" + Nip[8] + Nip[9] + "-" + Nip[10] + Nip[11];
            }
            return dashNip;  
        }

        public string ConvertToNormalNip(string Nip)//return 7777777777
        {
            string normalNip = null;

            if(Nip.Length==12)//PL7777777777-> 7777777777 
            {
                normalNip =""+Nip[2] + Nip[3] + Nip[4] + Nip[5] + Nip[6] + Nip[7] + Nip[8] + Nip[9] + Nip[10] + Nip[11];
            }
            else if(Nip.Length==13)//777-777-77-77 -> 7777777777
            { 
                normalNip =""+ Nip[0] + Nip[1] + Nip[2] + Nip[4] + Nip[5] + Nip[6] + Nip[8] + Nip[9] + Nip[11] + Nip[12];
            }
            return normalNip;
        }


    }
}