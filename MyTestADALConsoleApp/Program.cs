using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MyTestADALConsoleApp
{
    class Program
    {

        private static string resourceUri = "https://graph.microsoft.com";
        private static string clientId = "<Replace With Your Client ID> ";
        private static string redirectUri = "<Replace with your Redirect URI> ";
        private static string authority = "https://login.microsoftonline.com/";
        private static string tenantID = "<Replace with your TenantID>";
        private static AuthenticationContext authContext = null;
        private static AuthenticationResult result = null;
        private static HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            getAccessToken().Wait();
            // GetMembers utilizes HTTP Client, will print JSON Prettified in method
            getMembers().Wait();
            Console.WriteLine("\n Press Enter to exit the program \n");
            Console.ReadLine();
        }


        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }

        static async Task getMembers()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            Console.WriteLine("\n \n Retrieving users {0}", DateTime.Now.ToString());
            HttpResponseMessage response = await httpClient.GetAsync(resourceUri + "/v1.0/users?$top=5");

            if (response.IsSuccessStatusCode)
            {
                // Read the response and output it to the console.
                string users = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\n \n Printing out Users \n \n");
                Console.WriteLine(JsonPrettify(users));
                Console.WriteLine("Received Info");
            }
            else
            {
                Console.WriteLine("Failed to retrieve To Do list\nError:  {0}\n", response.ReasonPhrase);
            }
        }

        static async Task prettyJWTPrint(String myToken)
        {
            //Assume the input is in a control called txtJwtIn,
            //and the output will be placed in a control called txtJwtOut
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = myToken;
            String prettyPrint = "";

            //Check if readable token (string is in a JWT format)
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (readableToken != true)
            {
                Console.WriteLine("The token doesn't seem to be in a proper JWT format.");
            }
            if (readableToken == true)
            {
                var token = jwtHandler.ReadJwtToken(jwtInput);

                //Extract the headers of the JWT
                var headers = token.Header;
                var jwtHeader = "{";
                foreach (var h in headers)
                {
                    jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                }
                jwtHeader += "}";
                prettyPrint = "Header:\r\n" + JToken.Parse(jwtHeader).ToString(Formatting.Indented);

                //Extract the payload of the JWT
                var claims = token.Claims;
                var jwtPayload = "{";
                foreach (System.Security.Claims.Claim c in claims)
                {
                    jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                }
                jwtPayload += "}";
                prettyPrint += "\r\nPayload:\r\n" + JToken.Parse(jwtPayload).ToString(Formatting.Indented);

            }
            Console.WriteLine(prettyPrint);
        }
        static async Task getAccessToken()
        {
            int retryCount = 0;
            bool retry = false;
            authContext = new AuthenticationContext(authority + tenantID);

            do
            {
                retry = false;
                try
                {
                    // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
                    result = await authContext.AcquireTokenAsync(resourceUri, clientId, new Uri(redirectUri), new PlatformParameters(PromptBehavior.Auto));

                    Console.Write("My Access Token : \n");
                    await prettyJWTPrint(result.AccessToken);
                }
                catch (AdalException ex)
                {
                    if (ex.ErrorCode == "temporarily_unavailable")
                    {
                        retry = true;
                        retryCount++;
                        Thread.Sleep(3000);
                    }

                    Console.WriteLine(
                        String.Format("An error occurred while acquiring a token\nTime: {0}\nError: {1}\nRetry: {2}\n",
                        DateTime.Now.ToString(),
                        ex.ToString(),
                        retry.ToString()));
                }

            } while ((retry == true) && (retryCount < 3));

            if (result == null)
            {
                Console.WriteLine("Canceling attempt to get access token.\n");
                return;
            }

        }

    }
}
