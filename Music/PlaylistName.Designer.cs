namespace Music
{
    partial class PlaylistName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            tbxPlaylistName = new Guna.UI2.WinForms.Guna2TextBox();
            btCreatePlaylist = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // tbxPlaylistName
            // 
            tbxPlaylistName.CustomizableEdges = customizableEdges5;
            tbxPlaylistName.DefaultText = "";
            tbxPlaylistName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbxPlaylistName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbxPlaylistName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbxPlaylistName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbxPlaylistName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxPlaylistName.Font = new Font("Segoe UI", 9F);
            tbxPlaylistName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxPlaylistName.Location = new Point(12, 89);
            tbxPlaylistName.Margin = new Padding(3, 4, 3, 4);
            tbxPlaylistName.Name = "tbxPlaylistName";
            tbxPlaylistName.PasswordChar = '\0';
            tbxPlaylistName.PlaceholderText = "";
            tbxPlaylistName.SelectedText = "";
            tbxPlaylistName.ShadowDecoration.CustomizableEdges = customizableEdges6;
            tbxPlaylistName.Size = new Size(353, 60);
            tbxPlaylistName.TabIndex = 0;
            // 
            // btCreatePlaylist
            // 
            btCreatePlaylist.CustomizableEdges = customizableEdges7;
            btCreatePlaylist.DisabledState.BorderColor = Color.DarkGray;
            btCreatePlaylist.DisabledState.CustomBorderColor = Color.DarkGray;
            btCreatePlaylist.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btCreatePlaylist.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btCreatePlaylist.Font = new Font("Segoe UI", 9F);
            btCreatePlaylist.ForeColor = Color.White;
            btCreatePlaylist.Location = new Point(75, 156);
            btCreatePlaylist.Name = "btCreatePlaylist";
            btCreatePlaylist.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btCreatePlaylist.Size = new Size(225, 56);
            btCreatePlaylist.TabIndex = 1;
            btCreatePlaylist.Text = "Confirm";
            btCreatePlaylist.Click += btCreatePlaylist_Click;
            // 
            // PlaylistName
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(377, 224);
            Controls.Add(btCreatePlaylist);
            Controls.Add(tbxPlaylistName);
            Name = "PlaylistName";
            Text = "PlaylistName";
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox tbxPlaylistName;
        private Guna.UI2.WinForms.Guna2Button btCreatePlaylist;
    }
}