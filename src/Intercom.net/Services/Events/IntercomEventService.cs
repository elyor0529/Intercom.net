using System.Net;
using RestSharp;

namespace Intercom
{
    public partial class IntercomRestClient
    {
        public virtual bool SubmitEvent(IntercomEvent intercomEvent)
        {
            var request = new RestRequest(Method.POST)
            {
                Resource = "events",
                RequestFormat = DataFormat.Json,
                JsonSerializer = new RestSharpJsonNetSerializer()
            };
            request.AddJsonBody(intercomEvent);

            var response = Execute(request);

            return (response.StatusCode == HttpStatusCode.Accepted);
        }
    }
}
