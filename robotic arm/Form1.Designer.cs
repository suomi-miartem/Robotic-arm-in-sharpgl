namespace robotic_arm
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.findContoursBN = new System.Windows.Forms.Button();
            this.comChosenContBN = new System.Windows.Forms.Button();
            this.filePathCB = new System.Windows.Forms.ComboBox();
            this.numThresholdMin = new System.Windows.Forms.NumericUpDown();
            this.numThresholdMax = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numOfContCB = new System.Windows.Forms.ComboBox();
            this.dividerCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.infoLBL = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numThresholdMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThresholdMax)).BeginInit();
            this.SuspendLayout();
            // 
            // findContoursBN
            // 
            this.findContoursBN.Location = new System.Drawing.Point(11, 39);
            this.findContoursBN.Name = "findContoursBN";
            this.findContoursBN.Size = new System.Drawing.Size(156, 23);
            this.findContoursBN.TabIndex = 0;
            this.findContoursBN.Text = "Find contours";
            this.findContoursBN.UseVisualStyleBackColor = true;
            this.findContoursBN.Click += new System.EventHandler(this.findContoursBN_Click);
            // 
            // comChosenContBN
            // 
            this.comChosenContBN.Enabled = false;
            this.comChosenContBN.Location = new System.Drawing.Point(11, 68);
            this.comChosenContBN.Name = "comChosenContBN";
            this.comChosenContBN.Size = new System.Drawing.Size(155, 23);
            this.comChosenContBN.TabIndex = 1;
            this.comChosenContBN.Text = "Compute chosen contour";
            this.comChosenContBN.UseVisualStyleBackColor = true;
            this.comChosenContBN.Click += new System.EventHandler(this.comChosenContBN_Click);
            // 
            // filePathCB
            // 
            this.filePathCB.FormattingEnabled = true;
            this.filePathCB.Location = new System.Drawing.Point(11, 12);
            this.filePathCB.Name = "filePathCB";
            this.filePathCB.Size = new System.Drawing.Size(155, 21);
            this.filePathCB.TabIndex = 3;
            this.filePathCB.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.filePathCB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // numThresholdMin
            // 
            this.numThresholdMin.Enabled = false;
            this.numThresholdMin.Location = new System.Drawing.Point(172, 12);
            this.numThresholdMin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numThresholdMin.Name = "numThresholdMin";
            this.numThresholdMin.Size = new System.Drawing.Size(39, 20);
            this.numThresholdMin.TabIndex = 4;
            this.numThresholdMin.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numThresholdMin.ValueChanged += new System.EventHandler(this.numThresholdMin_ValueChanged);
            // 
            // numThresholdMax
            // 
            this.numThresholdMax.Enabled = false;
            this.numThresholdMax.Location = new System.Drawing.Point(217, 12);
            this.numThresholdMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numThresholdMax.Name = "numThresholdMax";
            this.numThresholdMax.Size = new System.Drawing.Size(37, 20);
            this.numThresholdMax.TabIndex = 5;
            this.numThresholdMax.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numThresholdMax.ValueChanged += new System.EventHandler(this.numThresholdMax_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(99, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Object parameters:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Threshold";
            // 
            // numOfContCB
            // 
            this.numOfContCB.Enabled = false;
            this.numOfContCB.FormattingEnabled = true;
            this.numOfContCB.Location = new System.Drawing.Point(172, 38);
            this.numOfContCB.Name = "numOfContCB";
            this.numOfContCB.Size = new System.Drawing.Size(53, 21);
            this.numOfContCB.TabIndex = 12;
            this.numOfContCB.SelectedIndexChanged += new System.EventHandler(this.numOfContCB_SelectedIndexChanged);
            // 
            // dividerCB
            // 
            this.dividerCB.Enabled = false;
            this.dividerCB.FormattingEnabled = true;
            this.dividerCB.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16"});
            this.dividerCB.Location = new System.Drawing.Point(172, 65);
            this.dividerCB.Name = "dividerCB";
            this.dividerCB.Size = new System.Drawing.Size(53, 21);
            this.dividerCB.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Number of contour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Divider";
            // 
            // infoLBL
            // 
            this.infoLBL.AutoSize = true;
            this.infoLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLBL.Location = new System.Drawing.Point(12, 114);
            this.infoLBL.Name = "infoLBL";
            this.infoLBL.Size = new System.Drawing.Size(41, 20);
            this.infoLBL.TabIndex = 16;
            this.infoLBL.Text = "Info:\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 266);
            this.Controls.Add(this.infoLBL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dividerCB);
            this.Controls.Add(this.numOfContCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numThresholdMax);
            this.Controls.Add(this.numThresholdMin);
            this.Controls.Add(this.filePathCB);
            this.Controls.Add(this.comChosenContBN);
            this.Controls.Add(this.findContoursBN);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numThresholdMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThresholdMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findContoursBN;
        private System.Windows.Forms.Button comChosenContBN;
        private System.Windows.Forms.ComboBox filePathCB;
        private System.Windows.Forms.NumericUpDown numThresholdMin;
        private System.Windows.Forms.NumericUpDown numThresholdMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox numOfContCB;
        private System.Windows.Forms.ComboBox dividerCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label infoLBL;
    }
}

