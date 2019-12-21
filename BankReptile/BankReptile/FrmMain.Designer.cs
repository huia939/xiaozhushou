namespace BankReptile
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.dgvBanks = new System.Windows.Forms.DataGridView();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.clbank_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bank_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBanks
            // 
            this.dgvBanks.AllowUserToAddRows = false;
            this.dgvBanks.AllowUserToDeleteRows = false;
            this.dgvBanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBanks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBanks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clbank_name,
            this.bank_id,
            this.code_type});
            this.dgvBanks.Location = new System.Drawing.Point(0, 0);
            this.dgvBanks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvBanks.MultiSelect = false;
            this.dgvBanks.Name = "dgvBanks";
            this.dgvBanks.ReadOnly = true;
            this.dgvBanks.RowHeadersVisible = false;
            this.dgvBanks.RowTemplate.Height = 30;
            this.dgvBanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBanks.Size = new System.Drawing.Size(651, 293);
            this.dgvBanks.TabIndex = 0;
            this.dgvBanks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBanks_CellClick);
            this.dgvBanks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBanks_CellContentClick);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(0, 297);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(651, 192);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.Text = "";
            // 
            // clbank_name
            // 
            this.clbank_name.DataPropertyName = "bank_name";
            this.clbank_name.HeaderText = "银行名称";
            this.clbank_name.Name = "clbank_name";
            this.clbank_name.ReadOnly = true;
            // 
            // bank_id
            // 
            this.bank_id.DataPropertyName = "bank_id";
            this.bank_id.HeaderText = "银行卡标识";
            this.bank_id.Name = "bank_id";
            this.bank_id.ReadOnly = true;
            // 
            // code_type
            // 
            this.code_type.DataPropertyName = "code_type";
            this.code_type.HeaderText = "程序状态";
            this.code_type.Name = "code_type";
            this.code_type.ReadOnly = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 489);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.dgvBanks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网银小助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBanks;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clbank_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn bank_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn code_type;
    }
}