using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace web
{
    public static class GlobalVariable
    {
        public static HttpClient webApiClient = new HttpClient();

        static GlobalVariable()
        {
            webApiClient.BaseAddress = new Uri("http://localhost:9080/pidev-web/api/");
            webApiClient.DefaultRequestHeaders.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}