// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.IO;
using System.Net;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json.Linq;

namespace L03
{
    class Program
    {
        private static DriveService _service;
        private static string _token;

        protected static string[] scopes = { DriveService.Scope.Drive };
        protected static UserCredential credential;
        static string ApplicationName = "datc1618";
        protected static DriveService service;
        // protected readonly FileExtensionContentTypeProvider fileExtensionProvider;

        static void Main(string[] args)
        {
            init();
           GetMyFiles();
          // CreateService();
        }

        static void CreateService()
        {
            using (var stream =
                new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    Environment.UserName, // use a const or read it from a config file
                    CancellationToken.None,
                    // null).Result;
                    new FileDataStore(credPath, true)).Result;

                // fileExtensionProvider = new FileExtensionContentTypeProvider();
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            Console.WriteLine(credential.Token.AccessToken);
            _token = credential.Token.AccessToken;
        }

        static void init()
        {
            string[] scopes = new string[] {
                DriveService.Scope.Drive,
                DriveService.Scope.DriveFile
            };

        
            var clientId = "<your client id>" ;
            var clientSecret = "<your client secret>";
            string credPath = ".";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                scopes,
                "User",
                //Environment.UserName
                CancellationToken.None,

                new FileDataStore(credPath, true)
                // null
                ).Result;

            _service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            _token = credential.Token.AccessToken;

            Console.WriteLine("Token: " + _token);
        }

        static void GetMyFiles()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/drive/v3/files?q='root'%20in%20parents"); //%20and%20name%20contains%20datc
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + _token);

            using (var response = request.GetResponse())
            {
                using (Stream data = response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    string text = reader.ReadToEnd();
                    var myData = JObject.Parse(text);
                    foreach (var file in myData["files"])
                    {
                        // Console.WriteLine(file);
                        if (file["mimeType"].ToString() != "application/vnd.google-apps.folder")
                        {
                            Console.WriteLine("File name: " + file["name"]);
                        }

                    }
                }
            }

        }
    }
}
