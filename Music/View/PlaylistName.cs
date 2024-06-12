using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    public partial class PlaylistName : Form
    {
        public PlaylistName()
        {
            InitializeComponent();
        }
        public string playlistName;
        private void btCreatePlaylist_Click(object sender, EventArgs e)
        {
            if (tbxPlaylistName.Text != "")
            {
                playlistName = tbxPlaylistName.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
