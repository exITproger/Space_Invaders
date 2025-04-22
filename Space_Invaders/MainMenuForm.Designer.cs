namespace Space_Invaders
{
    partial class MainMenuForm
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
            this.btnPlayDesk = new System.Windows.Forms.Button();
            this.btnShop = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPlayMob = new System.Windows.Forms.Button();
            this.picFon = new System.Windows.Forms.PictureBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picFon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // play Desktop
            // 
            this.btnPlayDesk.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.btnPlayDesk.FlatAppearance.BorderSize = 10;
            this.btnPlayDesk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Olive;
            this.btnPlayDesk.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayDesk.ForeColor = System.Drawing.Color.Red;
            this.btnPlayDesk.Location = new System.Drawing.Point(707, 429);
            this.btnPlayDesk.Name = "button1";
            this.btnPlayDesk.Size = new System.Drawing.Size(254, 63);
            this.btnPlayDesk.TabIndex = 0;
            this.btnPlayDesk.Text = "Играть ПК";
            this.btnPlayDesk.UseVisualStyleBackColor = true;
            this.btnPlayDesk.Click += new System.EventHandler(this.PlayDesktopButton_Click);
            // 
            // btnShop
            // 
            this.btnShop.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold);
            this.btnShop.ForeColor = System.Drawing.Color.Red;
            this.btnShop.Location = new System.Drawing.Point(872, 515);
            this.btnShop.Name = "btnShop";
            this.btnShop.Size = new System.Drawing.Size(218, 64);
            this.btnShop.TabIndex = 1;
            this.btnShop.Text = "Магазин";
            this.btnShop.UseVisualStyleBackColor = true;
            this.btnShop.Click += new System.EventHandler(this.OpenShopButton_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(872, 605);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(218, 60);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // play mobile
            // 
            this.btnPlayMob.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayMob.ForeColor = System.Drawing.Color.Red;
            this.btnPlayMob.Location = new System.Drawing.Point(1009, 429);
            this.btnPlayMob.Name = "button2";
            this.btnPlayMob.Size = new System.Drawing.Size(243, 63);
            this.btnPlayMob.TabIndex = 3;
            this.btnPlayMob.Text = "Играть Моб";
            this.btnPlayMob.UseVisualStyleBackColor = true;
            this.btnPlayMob.Click += new System.EventHandler(this.PlayMobileButton_Click);
            // 
            // picFon
            // 
            this.picFon.BackColor = System.Drawing.Color.Black;
            this.picFon.BackgroundImage = global::Space_Invaders.Properties.Resources.фонДД;
            this.picFon.Image = global::Space_Invaders.Properties.Resources.image_Photoroom;
            this.picFon.Location = new System.Drawing.Point(2, 122);
            this.picFon.Name = "pictureBox1";
            this.picFon.Size = new System.Drawing.Size(170, 201);
            this.picFon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFon.TabIndex = 4;
            this.picFon.TabStop = false;
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImage = global::Space_Invaders.Properties.Resources.фонДД;
            this.picLogo.Image = global::Space_Invaders.Properties.Resources.logo_Photoroom;
            this.picLogo.Location = new System.Drawing.Point(1725, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(179, 167);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Space_Invaders.Properties.Resources.pr;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.picFon);
            this.Controls.Add(this.btnPlayMob);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnShop);
            this.Controls.Add(this.btnPlayDesk);
            this.Name = "MainMenuForm";
            this.Text = "MainMenuForm";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlayDesk;
        private System.Windows.Forms.Button btnShop;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPlayMob;
        private System.Windows.Forms.PictureBox picFon;
        private System.Windows.Forms.PictureBox picLogo;
    }
}