using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace IBDoc
{
    class Program
    {
        static void Main()
        {
            RunClient();
            Console.ReadLine();
        }

        static void RunClient()
        {
            Patient patient = new Patient()
            {
                patient_name = "John",
                patient_initials = "Thomas os 465465465",
                telephone = "69696969",
                moderate_threshold = 150,
                high_threshold = 750,
                id = 5565,
                email= "o@ff.gr"
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://ibdoc-portal.net:443");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var access_token = "TvVZiR3aWbkiXYBIZlpNDZm8NdVQrX";
                
                Results results= new Results()
                {
                    patient_id=5564,
                    criteria = "list"
                };

                // Create a new Token (not required if the old one is still valid). We should better pass the credentials instance as a parameter
                //Token token = GetTokenAsync(client).GetAwaiter().GetResult();

                //if (token != null)
                //    Console.WriteLine("Access Token: {0}", token.access_token);
                //else
                //    Console.WriteLine("Access Token is NULL");

                //List<Patient> patients=GetAllPatients(client, access_token).GetAwaiter().GetResult();
                //Patient patientWithID=GetPatientById(client, "5565", access_token).GetAwaiter().GetResult();
                //Patient newPatient = CreatePatient(client, patient, access_token).GetAwaiter().GetResult();
                //Patient updatePatient = UpdatePatient(client, patient, access_token).GetAwaiter().GetResult();
                //UpdateInstance(client, patient, access_token).Wait();
                //List<Results> resultsList = GetResults(client, results, access_token).GetAwaiter().GetResult();
                //Boolean patientExists=CheckIfPatientExists(client, patient, access_token).GetAwaiter().GetResult().exists;
            }
        }

        public static async Task<List<Patient>> GetAllPatients(HttpClient client, string accessToken)
        {
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await client.GetAsync("/api/2.0/patients/");

            if (CheckStatusCode(response.StatusCode))
            {
                response.EnsureSuccessStatusCode();

                List<Patient> patients = await response.Content.ReadAsAsync<List<Patient>>();

                return patients;
            }
            else
            {
                return null;
            }
        }

        public static async Task<Patient> GetPatientById(HttpClient client, string id, string accessToken)
        {
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync("/api/2.0/patients/" +id+ "/");
            if (CheckStatusCode(response.StatusCode))
            {
                response.EnsureSuccessStatusCode();

                Patient patient = await response.Content.ReadAsAsync<Patient>();
                return patient;
            }
            else
            {
                return null;
            }
        }
        
        static async Task<Token> GetTokenAsync(HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();

            Credentials credentials = new Credentials()
            {
                username = "ddan@pccint.eu",
                password = "k0!,3.1dJ32W",
                client_id = "ieS@IalA5-4zbVt9kyMx8!vd3fraboJxmTIPIXyx",
                client_secret = "9.5WabO6IZT8DrIMCJ95Qy:J@qkl5OTK5IZDpP_Gc@7:aG38vbRsX6dvFfplJ0RWaDGCd9UXb;XKXl97z-FzGGodBYkGDViTQVA!fbZxCuijRMqcFYddBePp!1:Ds;bK",
                grant_type = "password"
            };

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "username", credentials.username },
                { "password", credentials.password },
                { "client_id", credentials.client_id },
                { "client_secret", credentials.client_secret },
                { "grant_type", credentials.grant_type },
            });

            var response = await client.PostAsync(
                new Uri("https://ibdoc-portal.net/api/2.0/o/token/").ToString(),
                parameters);

            response.EnsureSuccessStatusCode();
            Token token = null;
            token = await response.Content.ReadAsAsync<Token>();
           
            return token;
        }

        private static async Task<Patient> CreatePatient(HttpClient client, Patient patientCreate, string access_token)
        {
            if (patientCreate == null)
                return new Patient();

            client.DefaultRequestHeaders.Clear();
            var parameters = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "patient_name", patientCreate.patient_name}
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var response = await client.PostAsync(
                new Uri("https://ibdoc-portal.net/api/2.0/patients/").ToString(),
                parameters);

            Patient patientResult = null;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                patientResult = await response.Content.ReadAsAsync<Patient>();
            }
            return patientResult;
        }

        private static async Task<Patient> UpdatePatient(HttpClient client, Patient patientUpdate, string access_token)
        {
            if (patientUpdate == null)
                return new Patient();

            client.DefaultRequestHeaders.Clear();
            var parameters = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "pk", patientUpdate.id.ToString() },
                { "patient_name", patientUpdate.patient_name},
                { "moderate_threshold", patientUpdate.moderate_threshold.ToString(CultureInfo.InvariantCulture)},
                { "high_threshold", patientUpdate.high_threshold.ToString(CultureInfo.InvariantCulture) }

            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var response = await client.PutAsync(
                new Uri("https://ibdoc-portal.net/api/2.0/patients/"+patientUpdate.id+"/").ToString(),
                parameters);

            Patient patientResult = null;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                patientResult = await response.Content.ReadAsAsync<Patient>();             
            }
            return patientResult;
        }

        //private static async Task<Patient> UpdateInstance(HttpClient client, Patient instanceUpdate, string access_token) //Patch Problem. Needs PatchAsync
        //{
        //    if (instanceUpdate == null)
        //        return new Patient();

        //    client.DefaultRequestHeaders.Clear();
        //    var parameters = new FormUrlEncodedContent(new Dictionary<string, string> {
        //        { "pk", instanceUpdate.id },

        //    });

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        //    var response = await PatchAsync(client,
        //        new Uri("https://ibdoc-portal.net/api/2.0/patients/" + instanceUpdate.id + "/").ToString(),
        //        parameters);

        //    Patient patientResult = null;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        patientResult = await response.Content.ReadAsAsync<Patient>();
        //        Console.WriteLine("Instance updated with patient_id: {0} ", patientResult.id);
        //    }

        //    return patientResult;
        //}

        public static async Task<HttpResponseMessage> PatchAsync(HttpClient client, Uri requestUri, HttpContent iContent)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }

            return response;
        } //Use on UpdateInstance

        private static async Task<PatientExistsCredentials> CheckIfPatientExists(HttpClient client, Patient patient, string access_token)
        {
            client.DefaultRequestHeaders.Clear();
            var parameters = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "email", patient.email}
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var response = await client.PostAsync(
                new Uri("https://ibdoc-portal.net:443/api/2.0/patient_exists/").ToString(),
                parameters);
            
            PatientExistsCredentials patientExists= null;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                patientExists = await response.Content.ReadAsAsync<PatientExistsCredentials>();
                Console.WriteLine("Patient with email: {0}, {1}", patientExists.email, patientExists.exists);
            }

            return patientExists;
        }

        public static async Task<List<Results>> GetResults(HttpClient client, Results results, string accessToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                
            HttpResponseMessage response = await client.GetAsync("/api/2.0/results/?patient_id=" +results.patient_id+ "&criteria=" +results.criteria);
            if (CheckStatusCode(response.StatusCode))
            {
                response.EnsureSuccessStatusCode();

                List<Results> listResults = await response.Content.ReadAsAsync<List<Results>>();
                foreach (var result in listResults)
                {
                    Console.WriteLine("Id: {0}, result {1}", result.patient_id, result.result);
                }
                return listResults;
            }
            else
            {
                return null;
            }
        }

        private static bool CheckStatusCode(HttpStatusCode statusCode)
        {
            //Check if authorized. Else generate new token
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("You are unauthorized");
                return false;
            }

            if (statusCode == HttpStatusCode.BadRequest)
            {
                Console.WriteLine("Bad Request");
                return false;
            }
            return true;
        }
    }
}


