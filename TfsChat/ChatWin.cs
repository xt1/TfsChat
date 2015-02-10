using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TfsChatServices;

namespace TfsChat
{
    public partial class ChatWin : Form
    {
        TfsChatServices.TfsChatService _tfs; 
        public ChatWin()
        {
            InitializeComponent();
            _tfs = new TfsChatServices.TfsChatService();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var rooms = _tfs.GetAllRooms();
            foreach( TfsTeamRoom room in rooms )
            {
                int id = lbRooms.Items.Add(room.Name);
            }
        }
    }
}
