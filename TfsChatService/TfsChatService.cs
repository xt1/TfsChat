using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace TfsChatServices
{
    public class TfsChatService : TfsRestServiceBase
    {
        internal TfsUserProfileService _userService = new TfsUserProfileService();

        public IEnumerable<TfsTeamRoom> GetAllRooms()
        {
            InitClient();
            var req = new RestRequest("_apis/Chat/Rooms", Method.GET);
            req.RequestFormat = DataFormat.Json;
            var res = _client.Execute(req);
            var info = _deserial.Deserialize<TfsTeamRoomList>(res);

            return info.Value;
        }

        public void Join(TfsTeamRoom room)
        {
            InitClient();
            var identity = _userService.GetIdentity();

            var request = new RestRequest("/_apis/chat/rooms/{RoomId}/users/{Identity}", Method.PUT);
            request.AddUrlSegment("RoomId", room.Id.ToString());
            request.AddUrlSegment("Identity", identity.TeamFoundationId);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { userId = identity.TeamFoundationId });
            var res = _client.Execute(request);
        }

        public void Leave(TfsTeamRoom room)
        {
            InitClient();
            var identity = _userService.GetIdentity();

            var request = new RestRequest("/_apis/chat/rooms/{RoomId}/users/{Identity}", Method.DELETE);
            request.AddUrlSegment("RoomId", room.Id.ToString());
            request.AddUrlSegment("Identity", identity.TeamFoundationId);
            request.RequestFormat = DataFormat.Json;
            var res = _client.Execute(request);
        }


        public void Write(TfsTeamRoom room, string message)
        {
            var request = new RestRequest("/_apis/chat/rooms/{RoomId}/messages", Method.POST);
            request.AddUrlSegment("RoomId", room.Id.ToString());
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { content = message });
            var res = _client.Execute(request);
        }

        public IEnumerable<TfsTeamRoomMessage> Messages(TfsTeamRoom room, string filter = null)
        {
            var request = new RestRequest("/_apis/chat/rooms/{RoomId}/messages", Method.GET);
            request.AddUrlSegment("RoomId", room.Id.ToString());
            request.RequestFormat = DataFormat.Json;

            if (filter != null)
            {
                request.AddParameter("$filter", filter);
            }
            var res = _client.Execute(request);
            TfsTeamMessageList messages = _deserial.Deserialize<TfsTeamMessageList>(res);
            if (messages == null)
                return new TfsTeamRoomMessage[0];
            return messages.Value;
        }


        private class TfsTeamRoomList
        {
            public int Count { get; set; }
            public List<TfsTeamRoom> Value { get; set; }
        }

        private class TfsTeamMessageList
        {
            public int Count { get; set; }
            public List<TfsTeamRoomMessage> Value { get; set; }
        }

    }
}
