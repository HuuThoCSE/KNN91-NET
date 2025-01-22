namespace LeNguyenQuangBinh_21022010
{
    partial class EmergencyMaintenancesManagement
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarrantyDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssetSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastClosedEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberofEMs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.WarrantyDate,
            this.AssetSN,
            this.AssetName,
            this.LastClosedEM,
            this.NumberofEMs});
            this.dataGridView.Location = new System.Drawing.Point(12, 26);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(644, 150);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Available Assets";
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(12, 182);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(236, 36);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "Send Emergency Maintenances Request";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // WarrantyDate
            // 
            this.WarrantyDate.HeaderText = "WarrantyDate";
            this.WarrantyDate.Name = "WarrantyDate";
            this.WarrantyDate.ReadOnly = true;
            this.WarrantyDate.Visible = false;
            // 
            // AssetSN
            // 
            this.AssetSN.HeaderText = "Asset SN";
            this.AssetSN.Name = "AssetSN";
            this.AssetSN.ReadOnly = true;
            // 
            // AssetName
            // 
            this.AssetName.HeaderText = "Asset Name";
            this.AssetName.Name = "AssetName";
            this.AssetName.ReadOnly = true;
            this.AssetName.Width = 200;
            // 
            // LastClosedEM
            // 
            this.LastClosedEM.HeaderText = "Last Closed EM";
            this.LastClosedEM.Name = "LastClosedEM";
            this.LastClosedEM.ReadOnly = true;
            this.LastClosedEM.Width = 170;
            // 
            // NumberofEMs
            // 
            this.NumberofEMs.HeaderText = "Number of EMs";
            this.NumberofEMs.Name = "NumberofEMs";
            this.NumberofEMs.ReadOnly = true;
            this.NumberofEMs.Width = 170;
            // 
            // EmergencyMaintenancesManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 230);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Name = "EmergencyMaintenancesManagement";
            this.Text = "Emergency Maintenances Management";
            this.Load += new System.EventHandler(this.EmergencyMaintenancesManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarrantyDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssetSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastClosedEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberofEMs;
    }
}