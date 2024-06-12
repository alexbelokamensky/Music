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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaylistName));
            tbxPlaylistName = new Guna.UI2.WinForms.Guna2TextBox();
            btCreatePlaylist = new Guna.UI2.WinForms.Guna2Button();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            btClose = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // tbxPlaylistName
            // 
            tbxPlaylistName.BorderRadius = 20;
            tbxPlaylistName.CustomizableEdges = customizableEdges1;
            tbxPlaylistName.DefaultText = "";
            tbxPlaylistName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbxPlaylistName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbxPlaylistName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbxPlaylistName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbxPlaylistName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxPlaylistName.Font = new Font("Segoe UI", 9F);
            tbxPlaylistName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxPlaylistName.Location = new Point(12, 55);
            tbxPlaylistName.Margin = new Padding(3, 4, 3, 4);
            tbxPlaylistName.Name = "tbxPlaylistName";
            tbxPlaylistName.PasswordChar = '\0';
            tbxPlaylistName.PlaceholderText = "Playlist's name";
            tbxPlaylistName.SelectedText = "";
            tbxPlaylistName.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tbxPlaylistName.Size = new Size(353, 60);
            tbxPlaylistName.TabIndex = 0;
            // 
            // btCreatePlaylist
            // 
            btCreatePlaylist.BorderRadius = 20;
            btCreatePlaylist.CustomizableEdges = customizableEdges3;
            btCreatePlaylist.DisabledState.BorderColor = Color.DarkGray;
            btCreatePlaylist.DisabledState.CustomBorderColor = Color.DarkGray;
            btCreatePlaylist.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btCreatePlaylist.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btCreatePlaylist.FillColor = Color.FromArgb(15, 15, 15);
            btCreatePlaylist.Font = new Font("Stencil", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btCreatePlaylist.ForeColor = Color.White;
            btCreatePlaylist.HoverState.FillColor = Color.FromArgb(26, 26, 26);
            btCreatePlaylist.HoverState.ForeColor = SystemColors.ScrollBar;
            btCreatePlaylist.Location = new Point(12, 133);
            btCreatePlaylist.Name = "btCreatePlaylist";
            btCreatePlaylist.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btCreatePlaylist.Size = new Size(353, 56);
            btCreatePlaylist.TabIndex = 1;
            btCreatePlaylist.Text = "Confirm";
            btCreatePlaylist.Click += btCreatePlaylist_Click;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 30;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // btClose
            // 
            btClose.BorderRadius = 15;
            customizableEdges5.BottomLeft = false;
            customizableEdges5.BottomRight = false;
            customizableEdges5.TopLeft = false;
            btClose.CustomizableEdges = customizableEdges5;
            btClose.DisabledState.BorderColor = Color.DarkGray;
            btClose.DisabledState.CustomBorderColor = Color.DarkGray;
            btClose.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btClose.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btClose.FillColor = Color.FromArgb(15, 15, 15);
            btClose.Font = new Font("Segoe UI", 9F);
            btClose.ForeColor = Color.White;
            btClose.HoverState.FillColor = Color.Red;
            btClose.Location = new Point(313, 0);
            btClose.Name = "btClose";
            btClose.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btClose.Size = new Size(63, 33);
            btClose.TabIndex = 7;
            btClose.Text = "x";
            btClose.Click += btClose_Click;
            // 
            // PlaylistName
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(377, 224);
            Controls.Add(btClose);
            Controls.Add(btCreatePlaylist);
            Controls.Add(tbxPlaylistName);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PlaylistName";
            Text = "PlaylistName";
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox tbxPlaylistName;
        private Guna.UI2.WinForms.Guna2Button btCreatePlaylist;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Button btClose;
    }
}