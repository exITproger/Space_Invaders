namespace Space_Invaders
{
    partial class ShopForm
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
            this.btnBack = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnBuy = new System.Windows.Forms.Button();
            this.btnBuySpeed = new System.Windows.Forms.Button();
            this.btnResetSpeed = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.PicPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.PicPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(2, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(276, 22);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(182, 13);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Добро пожаловать в наш магазин";
            // 
            // btnBuy
            // 
            this.btnBuy.Location = new System.Drawing.Point(67, 197);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(115, 60);
            this.btnBuy.TabIndex = 2;
            this.btnBuy.Text = "Купить";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnBuySpeed
            // 
            this.btnBuySpeed.Location = new System.Drawing.Point(131, 75);
            this.btnBuySpeed.Name = "btnBuySpeed";
            this.btnBuySpeed.Size = new System.Drawing.Size(105, 56);
            this.btnBuySpeed.TabIndex = 3;
            this.btnBuySpeed.Text = "speed";
            this.btnBuySpeed.UseVisualStyleBackColor = true;
            this.btnBuySpeed.Click += new System.EventHandler(this.btnBuySpeed_Click);
            // 
            // btnResetSpeed
            // 
            this.btnResetSpeed.Location = new System.Drawing.Point(295, 75);
            this.btnResetSpeed.Name = "btnResetSpeed";
            this.btnResetSpeed.Size = new System.Drawing.Size(94, 56);
            this.btnResetSpeed.TabIndex = 4;
            this.btnResetSpeed.Text = "reset";
            this.btnResetSpeed.UseVisualStyleBackColor = true;
            this.btnResetSpeed.Click += new System.EventHandler(this.btnResetSpeed_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Space_Invaders.Properties.Resources.Пули;
            this.pictureBox1.Location = new System.Drawing.Point(964, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Space_Invaders.Properties.Resources.Корабль;
            this.pictureBox2.Location = new System.Drawing.Point(769, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 124);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(-23, -46);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 50);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Space_Invaders.Properties.Resources.Bullet2_Skin_32n32;
            this.pictureBox4.Location = new System.Drawing.Point(1658, 49);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(133, 124);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Space_Invaders.Properties.Resources.Bullet4_Skin_32n32;
            this.pictureBox5.Location = new System.Drawing.Point(1464, 49);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(139, 124);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Space_Invaders.Properties.Resources.Bullet3_Skin_32n32;
            this.pictureBox6.Location = new System.Drawing.Point(1269, 49);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(142, 124);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 10;
            this.pictureBox6.TabStop = false;
            // 
            // PicPanel
            // 
            this.PicPanel.Controls.Add(this.label1);
            this.PicPanel.Controls.Add(this.pictureBox8);
            this.PicPanel.Controls.Add(this.pictureBox7);
            this.PicPanel.Controls.Add(this.pictureBox2);
            this.PicPanel.Controls.Add(this.pictureBox4);
            this.PicPanel.Controls.Add(this.pictureBox5);
            this.PicPanel.Controls.Add(this.pictureBox6);
            this.PicPanel.Controls.Add(this.pictureBox1);
            this.PicPanel.Location = new System.Drawing.Point(23, 304);
            this.PicPanel.Name = "PicPanel";
            this.PicPanel.Size = new System.Drawing.Size(1847, 321);
            this.PicPanel.TabIndex = 11;
            this.PicPanel.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(74, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 35);
            this.label1.TabIndex = 13;
            this.label1.Text = "Враг";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::Space_Invaders.Properties.Resources.Enemy2_64;
            this.pictureBox8.Location = new System.Drawing.Point(44, 49);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(135, 124);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 12;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::Space_Invaders.Properties.Resources.Enemy2Bullet14na38;
            this.pictureBox7.Location = new System.Drawing.Point(1153, 49);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(67, 124);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 11;
            this.pictureBox7.TabStop = false;
            // 
            // ShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.PicPanel);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnResetSpeed);
            this.Controls.Add(this.btnBuySpeed);
            this.Controls.Add(this.btnBuy);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnBack);
            this.Name = "ShopForm";
            this.Text = "ShopForm";
            this.Load += new System.EventHandler(this.ShopForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.PicPanel.ResumeLayout(false);
            this.PicPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Button btnBuySpeed;
        private System.Windows.Forms.Button btnResetSpeed;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel PicPanel;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label1;
    }
}