namespace AppQLBanLe
{
    partial class Form1
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstCart = new System.Windows.Forms.ListBox();
            this.btnRecommend = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(100, 20);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(200, 20);
            this.txtInput.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(310, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 26);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstCart
            // 
            this.lstCart.FormattingEnabled = true;
            this.lstCart.Location = new System.Drawing.Point(100, 60);
            this.lstCart.Name = "lstCart";
            this.lstCart.Size = new System.Drawing.Size(200, 95);
            this.lstCart.TabIndex = 2;
            // 
            // btnRecommend
            // 
            this.btnRecommend.Location = new System.Drawing.Point(100, 170);
            this.btnRecommend.Name = "btnRecommend";
            this.btnRecommend.Size = new System.Drawing.Size(150, 30);
            this.btnRecommend.TabIndex = 3;
            this.btnRecommend.Text = "Khuyến nghị";
            this.btnRecommend.UseVisualStyleBackColor = true;
            this.btnRecommend.Click += new System.EventHandler(this.btnRecommend_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(100, 220);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(200, 95);
            this.lstOutput.TabIndex = 4;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(391, 18);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(95, 26);
            this.btnLoadData.TabIndex = 5;
            this.btnLoadData.Text = "Tải dữ liệu";
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(492, 18);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(96, 26);
            this.btnTrain.TabIndex = 6;
            this.btnTrain.Text = "Huấn luyện";
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.Location = new System.Drawing.Point(310, 85);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(100, 30);
            this.btnClearCart.TabIndex = 7;
            this.btnClearCart.Text = "Xóa giỏ hàng";
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(742, 358);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstCart);
            this.Controls.Add(this.btnRecommend);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.btnClearCart);
            this.Name = "Form1";
            this.Text = "Hệ thống khuyến nghị sản phẩm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

