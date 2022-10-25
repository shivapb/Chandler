using ChandlerPro.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChandlerPro.Utils
{
    public class EpicorConnection
    {
        public static RootPOHeader getPOs()
        {
            RootPOHeader resPOHeader = null;
            try
            {
                string username = ConfigurationManager.AppSettings["Username"];
                string password = ConfigurationManager.AppSettings["Password"];
                string API_POes = ConfigurationManager.AppSettings["API_POes"];
                string COLS = ConfigurationManager.AppSettings["selectcols_POes"];
                string X_API_Key = ConfigurationManager.AppSettings["X-API-Key"];
                string sBaseURL;

                sBaseURL = getBaseURL();
                string URL = sBaseURL + API_POes;

                var client = new WebClient { Credentials = new NetworkCredential(username, password) };
                client.Headers.Add("X-API-Key", X_API_Key);
                var response = client.DownloadString(URL + COLS);
                resPOHeader = JsonConvert.DeserializeObject<RootPOHeader>(response);
                return resPOHeader;


            }
            catch (Exception ex)
            {
                return resPOHeader;

            }
        }

        public static RootPODtl getPODetails()
        {
            RootPODtl resPODtl = null;
            try
            {
                string username = ConfigurationManager.AppSettings["Username"];
                string password = ConfigurationManager.AppSettings["Password"];
                string sBaseURL;
                sBaseURL = getBaseURL();
                string URL = sBaseURL + ConfigurationManager.AppSettings["API_PODetails"];
                string COLS = ConfigurationManager.AppSettings["selectcols_PODetails"];
                string X_API_Key = ConfigurationManager.AppSettings["X-API-Key"];
                var client = new WebClient { Credentials = new NetworkCredential(username, password) };
                client.Headers.Add("X-API-Key", X_API_Key);
                var response = client.DownloadString(URL + COLS);
                resPODtl = JsonConvert.DeserializeObject<RootPODtl>(response);
                return resPODtl;
            }
            catch (Exception ex)
            {
                return resPODtl;
            }
        }


        public static RootPORel getPORels()
        {
            RootPORel resRootPORel = null;
            try
            {
                string username = ConfigurationManager.AppSettings["Username"];
                string password = ConfigurationManager.AppSettings["Password"];
                string sBaseURL;
                sBaseURL = getBaseURL();
                string URL = sBaseURL + ConfigurationManager.AppSettings["API_PORels"];
                string COLS = ConfigurationManager.AppSettings["selectcols_PORels"];
                string X_API_Key = ConfigurationManager.AppSettings["X-API-Key"];
                var client = new WebClient { Credentials = new NetworkCredential(username, password) };
                client.Headers.Add("X-API-Key", X_API_Key);
                var response = client.DownloadString(URL + COLS);
                resRootPORel = JsonConvert.DeserializeObject<RootPORel>(response);
                return resRootPORel;
            }
            catch (Exception ex)
            {
                return resRootPORel;
            }
        }


        private static string getBaseURL()
        {
            string sRetuURL = string.Empty;
            string ROOT_URL = ConfigurationManager.AppSettings["ROOT_URL"];
            string COMPANY_ID = ConfigurationManager.AppSettings["companyID"];

            sRetuURL = ROOT_URL + "/" + COMPANY_ID + "/";

            return sRetuURL;
        }
    }
}
