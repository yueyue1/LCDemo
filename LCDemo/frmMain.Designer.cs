namespace LCDemo
{
    partial class frmMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.picVideo = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.ptzCtrl7 = new System.Windows.Forms.Label();
            this.ptzCtrl5 = new System.Windows.Forms.Label();
            this.ptzCtrl4 = new System.Windows.Forms.Label();
            this.ptzCtrl1 = new System.Windows.Forms.Label();
            this.ptzCtrl2 = new System.Windows.Forms.Label();
            this.ptzCtrl3 = new System.Windows.Forms.Label();
            this.ptzCtrl6 = new System.Windows.Forms.Label();
            this.ptzCtrl0 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "获取设备";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(228, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "实时视频";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // picVideo
            // 
            this.picVideo.BackColor = System.Drawing.Color.Black;
            this.picVideo.Location = new System.Drawing.Point(332, 12);
            this.picVideo.Name = "picVideo";
            this.picVideo.Size = new System.Drawing.Size(600, 437);
            this.picVideo.TabIndex = 2;
            this.picVideo.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(32, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(271, 88);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(32, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "开始录像";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(32, 196);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "开始对讲";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(32, 239);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 7;
            this.button6.Text = "录像回放";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(108, 367);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "云台";
            // 
            // ptzCtrl7
            // 
            this.ptzCtrl7.AutoSize = true;
            this.ptzCtrl7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl7.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl7.Location = new System.Drawing.Point(148, 398);
            this.ptzCtrl7.Name = "ptzCtrl7";
            this.ptzCtrl7.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl7.TabIndex = 15;
            this.ptzCtrl7.Text = "↘";
            this.ptzCtrl7.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // ptzCtrl5
            // 
            this.ptzCtrl5.AutoSize = true;
            this.ptzCtrl5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl5.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl5.Location = new System.Drawing.Point(81, 395);
            this.ptzCtrl5.Name = "ptzCtrl5";
            this.ptzCtrl5.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl5.TabIndex = 16;
            this.ptzCtrl5.Text = "↙";
            this.ptzCtrl5.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // ptzCtrl4
            // 
            this.ptzCtrl4.AutoSize = true;
            this.ptzCtrl4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl4.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl4.Location = new System.Drawing.Point(84, 341);
            this.ptzCtrl4.Name = "ptzCtrl4";
            this.ptzCtrl4.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl4.TabIndex = 17;
            this.ptzCtrl4.Text = "↖";
            this.ptzCtrl4.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // ptzCtrl1
            // 
            this.ptzCtrl1.AutoSize = true;
            this.ptzCtrl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl1.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl1.Location = new System.Drawing.Point(70, 364);
            this.ptzCtrl1.Name = "ptzCtrl1";
            this.ptzCtrl1.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl1.TabIndex = 13;
            this.ptzCtrl1.Text = "←";
            this.ptzCtrl1.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // ptzCtrl2
            // 
            this.ptzCtrl2.AutoSize = true;
            this.ptzCtrl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl2.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl2.Location = new System.Drawing.Point(114, 410);
            this.ptzCtrl2.Name = "ptzCtrl2";
            this.ptzCtrl2.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl2.TabIndex = 12;
            this.ptzCtrl2.Text = "↓";
            this.ptzCtrl2.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // ptzCtrl3
            // 
            this.ptzCtrl3.AutoSize = true;
            this.ptzCtrl3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl3.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl3.Location = new System.Drawing.Point(162, 367);
            this.ptzCtrl3.Name = "ptzCtrl3";
            this.ptzCtrl3.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl3.TabIndex = 11;
            this.ptzCtrl3.Text = "→";
            this.ptzCtrl3.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // ptzCtrl6
            // 
            this.ptzCtrl6.AutoSize = true;
            this.ptzCtrl6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl6.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl6.Location = new System.Drawing.Point(145, 342);
            this.ptzCtrl6.Name = "ptzCtrl6";
            this.ptzCtrl6.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl6.TabIndex = 14;
            this.ptzCtrl6.Text = "↗";
            this.ptzCtrl6.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // ptzCtrl0
            // 
            this.ptzCtrl0.AutoSize = true;
            this.ptzCtrl0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptzCtrl0.ForeColor = System.Drawing.Color.Blue;
            this.ptzCtrl0.Location = new System.Drawing.Point(114, 327);
            this.ptzCtrl0.Name = "ptzCtrl0";
            this.ptzCtrl0.Size = new System.Drawing.Size(17, 12);
            this.ptzCtrl0.TabIndex = 10;
            this.ptzCtrl0.Text = "↑";
            this.ptzCtrl0.Click += new System.EventHandler(this.ptzCtrl_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(123, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "获取分享设备";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 461);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ptzCtrl7);
            this.Controls.Add(this.ptzCtrl5);
            this.Controls.Add(this.ptzCtrl4);
            this.Controls.Add(this.ptzCtrl1);
            this.Controls.Add(this.ptzCtrl2);
            this.Controls.Add(this.ptzCtrl3);
            this.Controls.Add(this.ptzCtrl6);
            this.Controls.Add(this.ptzCtrl0);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.picVideo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox picVideo;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label ptzCtrl7;
        private System.Windows.Forms.Label ptzCtrl5;
        private System.Windows.Forms.Label ptzCtrl4;
        private System.Windows.Forms.Label ptzCtrl1;
        private System.Windows.Forms.Label ptzCtrl2;
        private System.Windows.Forms.Label ptzCtrl3;
        private System.Windows.Forms.Label ptzCtrl6;
        private System.Windows.Forms.Label ptzCtrl0;
        private System.Windows.Forms.Button button5;
    }
}