namespace ProxyChecker
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonInput = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.buttonAddViewers = new System.Windows.Forms.Button();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.textBoxChannelName = new System.Windows.Forms.TextBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.buttonStopAddWiewers = new System.Windows.Forms.Button();
            this.labelProxyCheckerd = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(23, 199);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(186, 329);
            this.listBox1.TabIndex = 4;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(225, 199);
            this.listBox2.Name = "listBox2";
            this.listBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox2.Size = new System.Drawing.Size(170, 329);
            this.listBox2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 60);
            this.button1.TabIndex = 6;
            this.button1.Text = "Чекнуть прокси";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonInput
            // 
            this.buttonInput.Location = new System.Drawing.Point(23, 157);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(79, 23);
            this.buttonInput.TabIndex = 7;
            this.buttonInput.Text = "button_Input";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(225, 154);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(89, 23);
            this.buttonOutput.TabIndex = 8;
            this.buttonOutput.Text = "button_Output";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // buttonAddViewers
            // 
            this.buttonAddViewers.Location = new System.Drawing.Point(225, 91);
            this.buttonAddViewers.Name = "buttonAddViewers";
            this.buttonAddViewers.Size = new System.Drawing.Size(89, 60);
            this.buttonAddViewers.TabIndex = 9;
            this.buttonAddViewers.Text = "Начать накрутку из чекнутых";
            this.buttonAddViewers.UseVisualStyleBackColor = true;
            this.buttonAddViewers.Click += new System.EventHandler(this.buttonAddViewers_Click);
            // 
            // labelChannelName
            // 
            this.labelChannelName.AutoSize = true;
            this.labelChannelName.Location = new System.Drawing.Point(20, 21);
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.Size = new System.Drawing.Size(100, 13);
            this.labelChannelName.TabIndex = 10;
            this.labelChannelName.Text = "input channel name";
            // 
            // textBoxChannelName
            // 
            this.textBoxChannelName.Location = new System.Drawing.Point(23, 37);
            this.textBoxChannelName.Name = "textBoxChannelName";
            this.textBoxChannelName.Size = new System.Drawing.Size(100, 20);
            this.textBoxChannelName.TabIndex = 11;
            this.textBoxChannelName.Text = "hibify";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.HorizontalScrollbar = true;
            this.listBox3.Location = new System.Drawing.Point(437, 12);
            this.listBox3.MultiColumn = true;
            this.listBox3.Name = "listBox3";
            this.listBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox3.ScrollAlwaysVisible = true;
            this.listBox3.Size = new System.Drawing.Size(423, 524);
            this.listBox3.TabIndex = 12;
            // 
            // buttonStopAddWiewers
            // 
            this.buttonStopAddWiewers.Location = new System.Drawing.Point(320, 37);
            this.buttonStopAddWiewers.Name = "buttonStopAddWiewers";
            this.buttonStopAddWiewers.Size = new System.Drawing.Size(111, 86);
            this.buttonStopAddWiewers.TabIndex = 13;
            this.buttonStopAddWiewers.Text = "Остановить накрутку";
            this.buttonStopAddWiewers.UseVisualStyleBackColor = true;
            this.buttonStopAddWiewers.Click += new System.EventHandler(this.buttonStopAddWiewers_Click);
            // 
            // labelProxyCheckerd
            // 
            this.labelProxyCheckerd.AutoSize = true;
            this.labelProxyCheckerd.Location = new System.Drawing.Point(174, 21);
            this.labelProxyCheckerd.Name = "labelProxyCheckerd";
            this.labelProxyCheckerd.Size = new System.Drawing.Size(35, 13);
            this.labelProxyCheckerd.TabIndex = 14;
            this.labelProxyCheckerd.Text = "label2";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(225, 534);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "button_Input";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(130, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 36);
            this.button3.TabIndex = 16;
            this.button3.Text = "Clear ListBox";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(320, 157);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(79, 36);
            this.button4.TabIndex = 17;
            this.button4.Text = "Clear ListBox";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 567);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelProxyCheckerd);
            this.Controls.Add(this.buttonStopAddWiewers);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.textBoxChannelName);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.buttonAddViewers);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonInput);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.Button buttonAddViewers;
        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.TextBox textBoxChannelName;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button buttonStopAddWiewers;
        private System.Windows.Forms.Label labelProxyCheckerd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

