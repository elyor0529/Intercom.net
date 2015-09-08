using System;
using System.Reflection;
using System.Text;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Authenticators;

namespace Intercom
{
    public partial class IntercomRestClient
    {
        private readonly RestClient _client;

        public string BaseUrl { get; private set; }

        public IntercomRestClient(string appId, string apiKey)
        {
            BaseUrl = "https://api.intercom.io/";

            // silverlight friendly way to get current version
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(assembly.FullName);
            var version = assemblyName.Version;

            _client = new RestClient
            {
                UserAgent = "intercom-net/" + version + " (.NET " + Environment.Version + ")",
                Authenticator = new HttpBasicAuthenticator(appId, apiKey),
                BaseUrl = new Uri(BaseUrl),
                Timeout = 30500,
            };

#if FRAMEWORK
            _client.AddDefaultHeader("Accept-charset", "utf-8");
#endif
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual T Execute<T>(IRestRequest request) where T : new()
        {
            request.OnBeforeDeserialization = (resp) =>
            {
                // for individual resources when there's an error to make
                // sure that RestException props are populated
                if (((int)resp.StatusCode) >= 400)
                {
                    // have to read the bytes so .Content doesn't get populated
                    const string restException = "{{ \"RestException\" : {0} }}";
                    var content = resp.RawBytes.AsString(); //get the response content
                    var newJson = string.Format(restException, content);

                    resp.Content = null;
                    resp.RawBytes = Encoding.UTF8.GetBytes(newJson.ToString());
                }
            };

            var response = _client.Execute<T>(request);
            return response.Data;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual IRestResponse Execute(IRestRequest request)
        {
            return _client.Execute(request);
        }
    }
}
