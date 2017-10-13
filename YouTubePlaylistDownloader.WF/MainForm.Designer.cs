namespace YouTubePlaylistDownloader.WF
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_fetch = new System.Windows.Forms.Button();
            this.cb_playlists = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_convert = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.btn_selectall = new System.Windows.Forms.Button();
            this.btn_deselectall = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_browse = new System.Windows.Forms.Button();
            this.tb_path = new System.Windows.Forms.TextBox();
            this.lbl_path = new System.Windows.Forms.Label();
            this.pnl_verify = new System.Windows.Forms.Panel();
            this.btn_verify = new System.Windows.Forms.Button();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnl_verify.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_fetch);
            this.panel1.Controls.Add(this.cb_playlists);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(497, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 62);
            this.panel1.TabIndex = 8;
            // 
            // btn_fetch
            // 
            this.btn_fetch.Location = new System.Drawing.Point(296, 17);
            this.btn_fetch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_fetch.Name = "btn_fetch";
            this.btn_fetch.Size = new System.Drawing.Size(100, 28);
            this.btn_fetch.TabIndex = 13;
            this.btn_fetch.Text = "Fetch";
            this.btn_fetch.UseVisualStyleBackColor = true;
            this.btn_fetch.Click += new System.EventHandler(this.btn_fetch_Click);
            // 
            // cb_playlists
            // 
            this.cb_playlists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_playlists.FormattingEnabled = true;
            this.cb_playlists.Location = new System.Drawing.Point(121, 20);
            this.cb_playlists.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_playlists.Name = "cb_playlists";
            this.cb_playlists.Size = new System.Drawing.Size(165, 24);
            this.cb_playlists.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Playlist Name:";
            // 
            // lbl_username
            // 
            this.lbl_username.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_username.Location = new System.Drawing.Point(43, 15);
            this.lbl_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(267, 62);
            this.lbl_username.TabIndex = 9;
            this.lbl_username.Text = "Username Here";
            this.lbl_username.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_convert);
            this.panel2.Controls.Add(this.btn_download);
            this.panel2.Controls.Add(this.btn_selectall);
            this.panel2.Controls.Add(this.btn_deselectall);
            this.panel2.Location = new System.Drawing.Point(933, 82);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 505);
            this.panel2.TabIndex = 10;
            // 
            // btn_convert
            // 
            this.btn_convert.Location = new System.Drawing.Point(0, 400);
            this.btn_convert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(100, 43);
            this.btn_convert.TabIndex = 3;
            this.btn_convert.Text = "Download & Convert";
            this.btn_convert.UseMnemonic = false;
            this.btn_convert.UseVisualStyleBackColor = true;
            this.btn_convert.Click += new System.EventHandler(this.btn_convert_Click);
            // 
            // btn_download
            // 
            this.btn_download.Location = new System.Drawing.Point(0, 289);
            this.btn_download.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(100, 43);
            this.btn_download.TabIndex = 2;
            this.btn_download.Text = "Download";
            this.btn_download.UseMnemonic = false;
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // btn_selectall
            // 
            this.btn_selectall.Location = new System.Drawing.Point(0, 68);
            this.btn_selectall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_selectall.Name = "btn_selectall";
            this.btn_selectall.Size = new System.Drawing.Size(100, 43);
            this.btn_selectall.TabIndex = 0;
            this.btn_selectall.Text = "Select All";
            this.btn_selectall.UseMnemonic = false;
            this.btn_selectall.UseVisualStyleBackColor = true;
            this.btn_selectall.Click += new System.EventHandler(this.btn_selectall_Click);
            // 
            // btn_deselectall
            // 
            this.btn_deselectall.Location = new System.Drawing.Point(0, 178);
            this.btn_deselectall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_deselectall.Name = "btn_deselectall";
            this.btn_deselectall.Size = new System.Drawing.Size(100, 43);
            this.btn_deselectall.TabIndex = 1;
            this.btn_deselectall.Text = "Deselect All";
            this.btn_deselectall.UseMnemonic = false;
            this.btn_deselectall.UseVisualStyleBackColor = true;
            this.btn_deselectall.Click += new System.EventHandler(this.btn_deselectall_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_browse);
            this.panel3.Controls.Add(this.tb_path);
            this.panel3.Controls.Add(this.lbl_path);
            this.panel3.Location = new System.Drawing.Point(43, 604);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(883, 63);
            this.panel3.TabIndex = 11;
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(708, 15);
            this.btn_browse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(100, 28);
            this.btn_browse.TabIndex = 2;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // tb_path
            // 
            this.tb_path.Location = new System.Drawing.Point(104, 17);
            this.tb_path.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_path.Name = "tb_path";
            this.tb_path.Size = new System.Drawing.Size(595, 22);
            this.tb_path.TabIndex = 1;
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(28, 21);
            this.lbl_path.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(67, 17);
            this.lbl_path.TabIndex = 0;
            this.lbl_path.Text = "File Path:";
            // 
            // pnl_verify
            // 
            this.pnl_verify.Controls.Add(this.btn_verify);
            this.pnl_verify.Controls.Add(this.tb_email);
            this.pnl_verify.Location = new System.Drawing.Point(43, 15);
            this.pnl_verify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_verify.Name = "pnl_verify";
            this.pnl_verify.Size = new System.Drawing.Size(333, 62);
            this.pnl_verify.TabIndex = 12;
            // 
            // btn_verify
            // 
            this.btn_verify.Location = new System.Drawing.Point(229, 17);
            this.btn_verify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_verify.Name = "btn_verify";
            this.btn_verify.Size = new System.Drawing.Size(100, 28);
            this.btn_verify.TabIndex = 1;
            this.btn_verify.Text = "Verify";
            this.btn_verify.UseVisualStyleBackColor = true;
            this.btn_verify.Click += new System.EventHandler(this.btn_verify_Click);
            // 
            // tb_email
            // 
            this.tb_email.Location = new System.Drawing.Point(8, 20);
            this.tb_email.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(212, 22);
            this.tb_email.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 690);
            this.Controls.Add(this.pnl_verify);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "YouTube Playlist Downloader";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnl_verify.ResumeLayout(false);
            this.pnl_verify.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_deselectall;
        private System.Windows.Forms.Button btn_selectall;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.ComboBox cb_playlists;
        private System.Windows.Forms.Panel pnl_verify;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.Button btn_verify;
        private System.Windows.Forms.Button btn_fetch;
        private System.Windows.Forms.Button btn_convert;
    }
}

