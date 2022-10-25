using System;
using System.Text;
using System.Net.Http.Headers;
using System.Configuration;
using System.Net.Http;
using System.Net;
using ChandlerPro.Models;
using Newtonsoft.Json;

namespace ChandlerPro.Utils
{
    public class LimbleConnection
    {

        private static readonly HttpClient client = new HttpClient();
        public static HttpClient LimbleCredentials()
        {
            //string username = ConfigurationManager.AppSettings["limbleClientID"];
            //string password = ConfigurationManager.AppSettings["limbleClientSecret"];
            string username = ConfigurationManager.AppSettings["limbleClientIDSandBox"];
            string password = ConfigurationManager.AppSettings["limbleClientSecretSandBox"];
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
            return client;
        } 

        public static string SetUrl(string url)
        {
            return ConfigurationManager.AppSettings["limbleURL"] + url;
        }
    }
}
