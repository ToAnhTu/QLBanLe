namespace AppQLBanLe
{
    partial class dgvRules
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lstCart;
        private System.Windows.Forms.Button btnRecommend;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnClearCart;

        /// <summary>
        /// Dọn tài nguyên
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code thiết kế Form

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dgvRules));
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstCart = new System.Windows.Forms.ListBox();
            this.btnRecommend = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.dataGridViewTrain = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrain)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            resources.ApplyResources(this.txtInput, "txtInput");
            this.txtInput.Name = "txtInput";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstCart
            // 
            this.lstCart.FormattingEnabled = true;
            resources.ApplyResources(this.lstCart, "lstCart");
            this.lstCart.Name = "lstCart";
            // 
            // btnRecommend
            // 
            resources.ApplyResources(this.btnRecommend, "btnRecommend");
            this.btnRecommend.Name = "btnRecommend";
            this.btnRecommend.UseVisualStyleBackColor = true;
            this.btnRecommend.Click += new System.EventHandler(this.btnRecommend_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            resources.ApplyResources(this.lstOutput, "lstOutput");
            this.lstOutput.Name = "lstOutput";
            // 
            // btnLoadData
            // 
            resources.ApplyResources(this.btnLoadData, "btnLoadData");
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnTrain
            // 
            resources.ApplyResources(this.btnTrain, "btnTrain");
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnClearCart
            // 
            resources.ApplyResources(this.btnClearCart, "btnClearCart");
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            // 
            // dataGridViewTrain
            // 
            this.dataGridViewTrain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewTrain, "dataGridViewTrain");
            this.dataGridViewTrain.Name = "dataGridViewTrain";
            // 
            // dgvRules
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dataGridViewTrain);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstCart);
            this.Controls.Add(this.btnRecommend);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.btnClearCart);
            this.Name = "dgvRules";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTrain;
    }
}

