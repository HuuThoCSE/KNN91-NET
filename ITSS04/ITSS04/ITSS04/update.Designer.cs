namespace ITSS04
{
    partial class update
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
            this.cbb_dw = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_can = new System.Windows.Forms.Button();
            this.cbb_batnum = new System.Windows.Forms.ComboBox();
            this.bt_add = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbb_pn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.bt_sub = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_sw = new System.Windows.Forms.ComboBox();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_partlist = new System.Windows.Forms.DataGridView();
            this.txt_am = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_partlist)).BeginInit();
            this.SuspendLayout();
            // 
            // cbb_dw
            // 
            this.cbb_dw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_dw.FormattingEnabled = true;
            this.cbb_dw.Location = new System.Drawing.Point(745, 51);
            this.cbb_dw.Name = "cbb_dw";
            this.cbb_dw.Size = new System.Drawing.Size(281, 26);
            this.cbb_dw.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "Date:";
            // 
            // bt_can
            // 
            this.bt_can.BackColor = System.Drawing.Color.Red;
            this.bt_can.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_can.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bt_can.Location = new System.Drawing.Point(562, 519);
            this.bt_can.Name = "bt_can";
            this.bt_can.Size = new System.Drawing.Size(128, 35);
            this.bt_can.TabIndex = 15;
            this.bt_can.Text = "Cancel";
            this.bt_can.UseVisualStyleBackColor = false;
            this.bt_can.Click += new System.EventHandler(this.bt_can_Click);
            // 
            // cbb_batnum
            // 
            this.cbb_batnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_batnum.FormattingEnabled = true;
            this.cbb_batnum.Location = new System.Drawing.Point(478, 41);
            this.cbb_batnum.Name = "cbb_batnum";
            this.cbb_batnum.Size = new System.Drawing.Size(158, 26);
            this.cbb_batnum.TabIndex = 10;
            // 
            // bt_add
            // 
            this.bt_add.BackColor = System.Drawing.Color.Lime;
            this.bt_add.Location = new System.Drawing.Point(947, 35);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(128, 35);
            this.bt_add.TabIndex = 0;
            this.bt_add.Text = "+ Add to list";
            this.bt_add.UseVisualStyleBackColor = false;
            this.bt_add.Click += new System.EventHandler(this.bt_add_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(352, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Batch Number:";
            // 
            // cbb_pn
            // 
            this.cbb_pn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_pn.FormattingEnabled = true;
            this.cbb_pn.Location = new System.Drawing.Point(150, 40);
            this.cbb_pn.Name = "cbb_pn";
            this.cbb_pn.Size = new System.Drawing.Size(166, 26);
            this.cbb_pn.TabIndex = 2;
            this.cbb_pn.SelectionChangeCommitted += new System.EventHandler(this.cbb_pn_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Part Name:";
            // 
            // dtp
            // 
            this.dtp.CustomFormat = "yyyy/MM/dd";
            this.dtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp.Location = new System.Drawing.Point(116, 123);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(224, 24);
            this.dtp.TabIndex = 22;
            // 
            // bt_sub
            // 
            this.bt_sub.BackColor = System.Drawing.Color.Blue;
            this.bt_sub.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_sub.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bt_sub.Location = new System.Drawing.Point(410, 519);
            this.bt_sub.Name = "bt_sub";
            this.bt_sub.Size = new System.Drawing.Size(128, 35);
            this.bt_sub.TabIndex = 16;
            this.bt_sub.Text = "Submit";
            this.bt_sub.UseVisualStyleBackColor = false;
            this.bt_sub.Click += new System.EventHandler(this.bt_sub_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(666, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "Amount:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(744, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Destination Warehouse:";
            // 
            // cbb_sw
            // 
            this.cbb_sw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_sw.FormattingEnabled = true;
            this.cbb_sw.Location = new System.Drawing.Point(65, 51);
            this.cbb_sw.Name = "cbb_sw";
            this.cbb_sw.Size = new System.Drawing.Size(281, 26);
            this.cbb_sw.TabIndex = 20;
            this.cbb_sw.SelectedIndexChanged += new System.EventHandler(this.cbb_sw_SelectedIndexChanged);
            // 
            // Action
            // 
            this.Action.HeaderText = "Action";
            this.Action.MinimumWidth = 6;
            this.Action.Name = "Action";
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.MinimumWidth = 6;
            this.Amount.Name = "Amount";
            // 
            // BatchNumber
            // 
            this.BatchNumber.HeaderText = "Batch Number";
            this.BatchNumber.MinimumWidth = 6;
            this.BatchNumber.Name = "BatchNumber";
            // 
            // PartName
            // 
            this.PartName.HeaderText = "Part Name";
            this.PartName.MinimumWidth = 6;
            this.PartName.Name = "PartName";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbb_batnum);
            this.groupBox1.Controls.Add(this.dgv_partlist);
            this.groupBox1.Controls.Add(this.txt_am);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.bt_add);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbb_pn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(21, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1154, 310);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parts List";
            // 
            // dgv_partlist
            // 
            this.dgv_partlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_partlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_partlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartName,
            this.BatchNumber,
            this.Amount,
            this.Action});
            this.dgv_partlist.Location = new System.Drawing.Point(7, 84);
            this.dgv_partlist.Name = "dgv_partlist";
            this.dgv_partlist.RowHeadersWidth = 51;
            this.dgv_partlist.RowTemplate.Height = 24;
            this.dgv_partlist.Size = new System.Drawing.Size(1135, 210);
            this.dgv_partlist.TabIndex = 4;
            this.dgv_partlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_partlist_CellContentClick);
            // 
            // txt_am
            // 
            this.txt_am.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_am.Location = new System.Drawing.Point(742, 43);
            this.txt_am.Name = "txt_am";
            this.txt_am.Size = new System.Drawing.Size(132, 24);
            this.txt_am.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "Source Wasehouse";
            // 
            // update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 575);
            this.Controls.Add(this.cbb_dw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bt_can);
            this.Controls.Add(this.dtp);
            this.Controls.Add(this.bt_sub);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbb_sw);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "update";
            this.Text = "update";
            this.Load += new System.EventHandler(this.update_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_partlist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbb_dw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_can;
        private System.Windows.Forms.ComboBox cbb_batnum;
        private System.Windows.Forms.Button bt_add;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbb_pn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Button bt_sub;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_sw;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_partlist;
        private System.Windows.Forms.TextBox txt_am;
        private System.Windows.Forms.Label label1;
    }
}