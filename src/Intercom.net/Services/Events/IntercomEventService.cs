using System.Net;
using RestSharp;
using System;

namespace Intercom
{
    public partial class IntercomRestClient
    {
        public virtual bool SubmitEvent(IntercomEvent intercomEvent)
        {
            bool result = false;

            try
            {
                var request = new RestRequest(Method.POST)
                {
                    Resource = "events",
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new RestSharpJsonNetSerializer()
                };
                request.AddJsonBody(intercomEvent);

                var response = Execute(request);

                result = (response.StatusCode == HttpStatusCode.Accepted);
            }
            catch(Exception ex)
            {
                // TODO: log
            }

            return false;
        }
    }
}
