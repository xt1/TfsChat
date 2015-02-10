using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace TfsChatServices
{
    public abstract class TfsRestServiceBase
    {
        protected RestClient _client;
        protected RestSharp.Deserializers.JsonDeserializer _deserial;

        string _tfsUrl = "http://tfs.superoffice.no/crm/SuperOffice";

        protected void InitClient()
        {
            if (_client == null)
            {
                _client = new RestClient(_tfsUrl);
                _client.Authenticator = new NtlmAuthenticator();
            }
            if (_deserial == null)
                _deserial = new RestSharp.Deserializers.JsonDeserializer();
        }

    }
}
