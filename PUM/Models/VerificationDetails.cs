using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PUM.Models
{
    public class VerificationDetails
    {

        public class Rootobject
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public Subject subject { get; set; }
            public string requestDateTime { get; set; }
            public string requestId { get; set; }
        }

        public class Subject
        {
            public object[] authorizedClerks { get; set; }
            public string regon { get; set; }
            public string restorationDate { get; set; }
            public string workingAddress { get; set; }
            public bool hasVirtualAccounts { get; set; }
            public string statusVat { get; set; }
            public string krs { get; set; }
            public string restorationBasis { get; set; }
            public string[] accountNumbers { get; set; }
            public string registrationDenialBasis { get; set; }
            public string nip { get; set; }
            public string removalDate { get; set; }
            public object[] partners { get; set; }
            public string name { get; set; }
            public string registrationLegalDate { get; set; }
            public string removalBasis { get; set; }
            public string pesel { get; set; }
            public Representative[] representatives { get; set; }
            public string residenceAddress { get; set; }
            public string registrationDenialDate { get; set; }
        }

        public class Representative
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string nip { get; set; }
            public string companyName { get; set; }
        }

        public class VerifiedNIP
        {
            public string regon { get; set; }
            public string workingAddress { get; set; }
            public string statusVat { get; set; }
            public string accountNumbers { get; set; }
            public string nip { get; set; }
            public string name { get; set; }
            public string registrationLegalDate { get; set; }
            public string residenceAddress { get; set; }
            public string requestDateTime { get; set; }
            public string requestId { get; set; }
        }

        public async Task<Rootobject> VerifyNIP(string nip)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://wl-api.mf.gov.pl/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("https://wl-api.mf.gov.pl/api/search/nip/" + nip + "?date=" + DateTime.Today.ToString("yyyy-MM-dd"));
                var json_ = "[" + await response.Content.ReadAsStringAsync() + "]";
                Console.WriteLine(json_);
                var result = JsonConvert.DeserializeObject<List<Rootobject>>(json_);

                return result[0];
            }
        }
    }
}
