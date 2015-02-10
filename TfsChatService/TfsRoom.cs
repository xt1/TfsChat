using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TfsChatServices
{
    public class TfsTeamRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastActivity { get; set; }
        public string CreatorUserTfId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool HasAdminPermissions { get; set; }
        public bool HasReadWritePermissions { get; set; }
    }
}
