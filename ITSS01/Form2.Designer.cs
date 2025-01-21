namespace ITSS01
{
    partial class Form2
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_sup = new System.Windows.Forms.ComboBox();
            this.cbb_wh = new System.Windows.Forms.ComboBox();
            this.date_dtp = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_bat = new System.Windows.Forms.TextBox();
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
            this.btn_sub = new System.Windows.Forms.Button();
            this.btn_can = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_partlist)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Supplier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(736, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Warehouse";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date";
            // 
            // cbb_sup
            // 
            this.cbb_sup.FormattingEnabled = true;
            this.cbb_sup.Location = new System.Drawing.Point(126, 45);
            this.cbb_sup.Name = "cbb_sup";
            this.cbb_sup.Size = new System.Drawing.Size(246, 28);
            this.cbb_sup.TabIndex = 3;
            // 
            // cbb_wh
            // 
            this.cbb_wh.FormattingEnabled = true;
            this.cbb_wh.Location = new System.Drawing.Point(833, 42);
            this.cbb_wh.Name = "cbb_wh";
            this.cbb_wh.Size = new System.Drawing.Size(246, 28);
            this.cbb_wh.TabIndex = 4;
            // 
            // date_dtp
            // 
            this.date_dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_dtp.Location = new System.Drawing.Point(126, 108);
            this.date_dtp.Name = "date_dtp";
            this.date_dtp.Size = new System.Drawing.Size(199, 26);
            this.date_dtp.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_bat);
            this.groupBox1.Controls.Add(this.txt_am);
            this.groupBox1.Controls.Add(this.cbb_pn);
            this.groupBox1.Controls.Add(this.dgv_partlist);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(57, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1072, 387);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Part list";
            // 
            // txt_bat
            // 
            this.txt_bat.Location = new System.Drawing.Point(462, 37);
            this.txt_bat.Name = "txt_bat";
            this.txt_bat.Size = new System.Drawing.Size(149, 26);
            this.txt_bat.TabIndex = 9;
            // 
            // txt_am
            // 
            this.txt_am.Location = new System.Drawing.Point(754, 39);
            this.txt_am.Name = "txt_am";
            this.txt_am.Size = new System.Drawing.Size(100, 26);
            this.txt_am.TabIndex = 7;
            // 
            // cbb_pn
            // 
            this.cbb_pn.FormattingEnabled = true;
            this.cbb_pn.Location = new System.Drawing.Point(134, 37);
            this.cbb_pn.Name = "cbb_pn";
            this.cbb_pn.Size = new System.Drawing.Size(149, 28);
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
            this.dgv_partlist.Location = new System.Drawing.Point(69, 90);
            this.dgv_partlist.Name = "dgv_partlist";
            this.dgv_partlist.RowHeadersWidth = 62;
            this.dgv_partlist.RowTemplate.Height = 28;
            this.dgv_partlist.Size = new System.Drawing.Size(917, 279);
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
            this.btn_add.Location = new System.Drawing.Point(893, 37);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(104, 34);
            this.btn_add.TabIndex = 3;
            this.btn_add.Text = "+ Add list";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(679, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Amount:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(341, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Batch Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Part name:";
            // 
            // btn_sub
            // 
            this.btn_sub.Location = new System.Drawing.Point(446, 565);
            this.btn_sub.Name = "btn_sub";
            this.btn_sub.Size = new System.Drawing.Size(78, 38);
            this.btn_sub.TabIndex = 7;
            this.btn_sub.Text = "Submit";
            this.btn_sub.UseVisualStyleBackColor = true;
            this.btn_sub.Click += new System.EventHandler(this.btn_sub_Click);
            // 
            // btn_can
            // 
            this.btn_can.Location = new System.Drawing.Point(688, 565);
            this.btn_can.Name = "btn_can";
            this.btn_can.Size = new System.Drawing.Size(78, 38);
            this.btn_can.TabIndex = 8;
            this.btn_can.Text = "Cancel";
            this.btn_can.UseVisualStyleBackColor = true;
            this.btn_can.Click += new System.EventHandler(this.btn_can_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 644);
            this.Controls.Add(this.btn_can);
            this.Controls.Add(this.btn_sub);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.date_dtp);
            this.Controls.Add(this.cbb_wh);
            this.Controls.Add(this.cbb_sup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_partlist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_sup;
        private System.Windows.Forms.ComboBox cbb_wh;
        private System.Windows.Forms.DateTimePicker date_dtp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_partlist;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_am;
        private System.Windows.Forms.ComboBox cbb_pn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button btn_sub;
        private System.Windows.Forms.Button btn_can;
        private System.Windows.Forms.TextBox txt_bat;
    }
}