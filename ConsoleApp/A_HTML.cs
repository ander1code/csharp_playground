using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;


namespace ConsoleApp
{
    class HTML
    {
        class HttpOperations
        {
            private static HttpOperations instance = null;

            private HttpOperations()
            {
            }

            public static HttpOperations Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new HttpOperations();
                    }
                    return instance;
                }
            }

            public async void Get()
            {
                try
                {
                    using (HttpClient httpclient = new HttpClient())
                    {
                        httpclient.Timeout = TimeSpan.FromSeconds(5);
                        string data = await httpclient.GetStringAsync("http://127.0.0.1/test/people.json");
                        Console.WriteLine("GET: {0}", data);
                    };
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
            }

            // public async Task Post() / Versões mais novas do .NET Framework
            public async void Post()
            {
                try
                {
                    // Criando dados em json:
                    string data = @"{
                        ""id"": 123,
                        ""name"": ""Anderson""
                    }";

                    // posting...
                    using (HttpClient httpclient = new HttpClient())
                    {
                        // Criando o conteudo que vai no metodo:
                        // HttpContent possui constructor protegido. Devo atribuir a construção de um StringContent.
                        using (HttpContent content = new StringContent(data, Encoding.UTF8, "application/json"))
                        {
                            httpclient.Timeout = TimeSpan.FromSeconds(5);
                            // response
                            HttpResponseMessage response = await httpclient.PostAsync("http://127.0.0.1/test/people.json", content);

                            // response.EnsureSuccessStatusCode();

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Successfully posted.");
                                Console.WriteLine("RESPONSE: \n{0}", response);
                            }
                            else
                            {
                                Console.WriteLine("No posted!");
                            }
                        }
                    }
                }             
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
                catch (TaskCanceledException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
            }

            public async void Put()
            {
                try
                {
                    // Criando dados em json:
                    string data = @"{
                        ""id"": 123,
                        ""name"": ""Anderson""
                    }";

                    // posting...
                    using (HttpClient httpclient = new HttpClient())
                    {
                        // Criando o conteudo que vai no metodo:
                        // HttpContent possui constructor protegido. Devo atribuir a construção de um StringContent.
                        using (HttpContent content = new StringContent(data, Encoding.UTF8, "application/json"))
                        {
                            httpclient.Timeout = TimeSpan.FromSeconds(5);
                            // response
                            HttpResponseMessage response = await httpclient.PutAsync("http://127.0.0.1/test/people.json", content);

                            // response.EnsureSuccessStatusCode();

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Successfully puted.");
                                Console.WriteLine("RESPONSE: \n{0}", response);
                            }
                            else
                            {
                                Console.WriteLine("No puted!");
                            }
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
                catch (TaskCanceledException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
            }

            public async void Patch()
            {
                try
                {
                    string data = @"{
                        ""id"": 123,
                        ""name"": ""Anderson""
                    }";

                    using(HttpClient httpclient = new HttpClient())
                    {
                        httpclient.Timeout = TimeSpan.FromSeconds(5);
                        using(HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), "http://127.0.0.1/test/people.json"))
                        {
                            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await httpclient.SendAsync(request);

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Successfully patched.");
                                Console.WriteLine("RESPONSE: \n{0}", response);
                            }
                            else
                            {
                                Console.WriteLine("No patched!");
                            }
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
                catch (TaskCanceledException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
            }

            public async void Delete()
            {
                try
                {
                    // deleting...
                    using (HttpClient httpclient = new HttpClient())
                    {
                        httpclient.Timeout = TimeSpan.FromSeconds(5);
                        // response
                        HttpResponseMessage response = await httpclient.DeleteAsync("http://127.0.0.1/test/people.json");

                        // response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Successfully deleted.");
                            Console.WriteLine("RESPONSE: \n{0}", response);
                        }
                        else
                        {
                            Console.WriteLine("No deleted!");
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
                catch (TaskCanceledException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
            }

            public async void Authenticate()
            {
                try
                {
                    // Criar dados:
                    string user = "anderson";
                    string password = "abc123";
                    string credential = user + ":" + password;

                    // Criar os bytes:  transforma a [string em bytes] e depois transforma esses [bytes em Base64].
                    string credential_byte = Convert.ToBase64String(Encoding.ASCII.GetBytes(credential));

                    using(HttpClient httpclient = new HttpClient())
                    {
                        httpclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credential_byte);

                        using (HttpResponseMessage response = await httpclient.GetAsync("http://127.0.0.1/test/index.html"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Authentication OK!"); // Sem um sistema de autenticação ainda!
                                Console.WriteLine(response);
                            }
                            else
                            {
                                Console.WriteLine("Authentication Fail!");
                            }
                        }
                    }
                }
                catch (HttpParseException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
                catch (TaskCanceledException e)
                {
                    Console.WriteLine("Error: {1}: {0}", e.HResult, e.Message);
                }
            }
        }
        
        public static void Principal()
        {
            HttpOperations http = HttpOperations.Instance;
            /*
            http.Get();
            http.Post();
            http.Put();
            http.Patch();
            http.Delete();
            http.Authenticate();
            */
            Console.ReadKey();
        }

        /* Versões mais novas o .NET Framework:
         * 
            public static async Task Main(string[] args)
            {
                HttpOperations http = HttpOperations.Instance;

                await http.Post();

                Console.ReadKey();
            }
        */
    }
}
