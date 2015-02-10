using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace TfsChatServices
{
    public class TfsUserProfileService : TfsRestServiceBase
    {
        public TfsUserIdentity GetIdentity()
        {
            InitClient();
            var req = new RestRequest("/_api/_common/GetUserProfile");
            req.RequestFormat = DataFormat.Json;
            var res = _client.Execute(req);
            var info = _deserial.Deserialize<TfsUserIdentityContainer>(res);
            return info.Identity;
        }

        public TfsUserIdentity GetIdentity(string tfUserId)
        {
            InitClient();
            var req = new RestRequest("/_api/_common/GetUserProfile/{TfUserId}");
            req.AddUrlSegment("TfUserId", tfUserId);
            req.RequestFormat = DataFormat.Json;
            var res = _client.Execute(req);
            var info = _deserial.Deserialize<TfsUserIdentityContainer>(res);
            return info.Identity;
        }

        internal class TfsUserIdentityContainer
        {
            public TfsUserIdentity Identity { get; set; }
        }

    }
}
