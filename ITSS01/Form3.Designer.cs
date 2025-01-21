namespace ITSS01
{
    partial class Form3
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
            this.btn_can = new System.Windows.Forms.Button();
            this.btn_sub = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_bn = new System.Windows.Forms.TextBox();
            this.txt_am = new System.Windows.Forms.TextBox();
            this.cbb_pn = new System.Windows.Forms.ComboBox();
            this.dgv_partlist = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_add = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.cbb_wh = new System.Windows.Forms.ComboBox();
            this.cbb_sup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_partlist)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_can
            // 
            this.btn_can.Location = new System.Drawing.Point(525, 385);
            this.btn_can.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_can.Name = "btn_can";
            this.btn_can.Size = new System.Drawing.Size(52, 25);
            this.btn_can.TabIndex = 17;
            this.btn_can.Text = "Cancel";
            this.btn_can.UseVisualStyleBackColor = true;
            this.btn_can.Click += new System.EventHandler(this.btn_can_Click);
            // 
            // btn_sub
            // 
            this.btn_sub.Location = new System.Drawing.Point(364, 385);
            this.btn_sub.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_sub.Name = "btn_sub";
            this.btn_sub.Size = new System.Drawing.Size(52, 25);
            this.btn_sub.TabIndex = 16;
            this.btn_sub.Text = "Submit";
            this.btn_sub.UseVisualStyleBackColor = true;
            this.btn_sub.Click += new System.EventHandler(this.btn_sub_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_bn);
            this.groupBox1.Controls.Add(this.txt_am);
            this.groupBox1.Controls.Add(this.cbb_pn);
            this.groupBox1.Controls.Add(this.dgv_partlist);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(105, 129);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(715, 252);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Part list";
            // 
            // txt_bn
            // 
            this.txt_bn.Location = new System.Drawing.Point(308, 24);
            this.txt_bn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_bn.Name = "txt_bn";
            this.txt_bn.Size = new System.Drawing.Size(101, 20);
            this.txt_bn.TabIndex = 18;
            // 
            // txt_am
            // 
            this.txt_am.Location = new System.Drawing.Point(503, 25);
            this.txt_am.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_am.Name = "txt_am";
            this.txt_am.Size = new System.Drawing.Size(68, 20);
            this.txt_am.TabIndex = 7;
            // 
            // cbb_pn
            // 
            this.cbb_pn.FormattingEnabled = true;
            this.cbb_pn.Location = new System.Drawing.Point(89, 24);
            this.cbb_pn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbb_pn.Name = "cbb_pn";
            this.cbb_pn.Size = new System.Drawing.Size(101, 21);
            this.cbb_pn.TabIndex = 5;
            // 
            // dgv_partlist
            // 
            this.dgv_partlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_partlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv_partlist.Location = new System.Drawing.Point(46, 58);
            this.dgv_partlist.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgv_partlist.Name = "dgv_partlist";
            this.dgv_partlist.RowHeadersWidth = 62;
            this.dgv_partlist.RowTemplate.Height = 28;
            this.dgv_partlist.Size = new System.Drawing.Size(611, 181);
            this.dgv_partlist.TabIndex = 4;
            this.dgv_partlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_partlist_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Part Name";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.Width = 250;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Batch Number";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.Width = 250;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Amount";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Action";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.Width = 150;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(595, 24);
            this.btn_add.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(69, 22);
            this.btn_add.TabIndex = 3;
            this.btn_add.Text = "+ Add list";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(453, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Amount:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Batch Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Part name:";
            // 
            // dtp_date
            // 
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_date.Location = new System.Drawing.Point(151, 88);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(134, 20);
            this.dtp_date.TabIndex = 14;
            // 
            // cbb_wh
            // 
            this.cbb_wh.FormattingEnabled = true;
            this.cbb_wh.Location = new System.Drawing.Point(622, 46);
            this.cbb_wh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbb_wh.Name = "cbb_wh";
            this.cbb_wh.Size = new System.Drawing.Size(165, 21);
            this.cbb_wh.TabIndex = 13;
            // 
            // cbb_sup
            // 
            this.cbb_sup.FormattingEnabled = true;
            this.cbb_sup.Location = new System.Drawing.Point(151, 47);
            this.cbb_sup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbb_sup.Name = "cbb_sup";
            this.cbb_sup.Size = new System.Drawing.Size(165, 21);
            this.cbb_sup.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(557, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Warehouse";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Supplier";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 514);
            this.Controls.Add(this.btn_can);
            this.Controls.Add(this.btn_sub);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.cbb_wh);
            this.Controls.Add(this.cbb_sup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_partlist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_can;
        private System.Windows.Forms.Button btn_sub;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_am;
        private System.Windows.Forms.ComboBox cbb_pn;
        private System.Windows.Forms.DataGridView dgv_partlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.ComboBox cbb_wh;
        private System.Windows.Forms.ComboBox cbb_sup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_bn;
    }
}