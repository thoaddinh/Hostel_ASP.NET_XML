namespace VectorHostel_xml
{
    partial class FormManHinh
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
            this.btnPhong = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPhieuThue = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPhieuNuoc = new System.Windows.Forms.Button();
            this.btn_phieudien = new System.Windows.Forms.Button();
            this.btnPTT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPhong
            // 
            this.btnPhong.Location = new System.Drawing.Point(111, 160);
            this.btnPhong.Name = "btnPhong";
            this.btnPhong.Size = new System.Drawing.Size(174, 70);
            this.btnPhong.TabIndex = 0;
            this.btnPhong.Text = "Quản Lý phòng";
            this.btnPhong.UseVisualStyleBackColor = true;
            this.btnPhong.Click += new System.EventHandler(this.btnPhong_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn chức năng:";
            // 
            // btnPhieuThue
            // 
            this.btnPhieuThue.Location = new System.Drawing.Point(491, 160);
            this.btnPhieuThue.Name = "btnPhieuThue";
            this.btnPhieuThue.Size = new System.Drawing.Size(174, 70);
            this.btnPhieuThue.TabIndex = 0;
            this.btnPhieuThue.Text = "Quản lý Phiếu thuê phòng";
            this.btnPhieuThue.UseVisualStyleBackColor = true;
            this.btnPhieuThue.Click += new System.EventHandler(this.btnPhieuThue_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(207, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(355, 46);
            this.label2.TabIndex = 2;
            this.label2.Text = "Quản lý Phòng Trọ";
            // 
            // btnPhieuNuoc
            // 
            this.btnPhieuNuoc.Location = new System.Drawing.Point(111, 257);
            this.btnPhieuNuoc.Name = "btnPhieuNuoc";
            this.btnPhieuNuoc.Size = new System.Drawing.Size(174, 70);
            this.btnPhieuNuoc.TabIndex = 0;
            this.btnPhieuNuoc.Text = "Phiếu Nước";
            this.btnPhieuNuoc.UseVisualStyleBackColor = true;
            this.btnPhieuNuoc.Click += new System.EventHandler(this.btnPhieuNuoc_Click);
            // 
            // btn_phieudien
            // 
            this.btn_phieudien.Location = new System.Drawing.Point(491, 257);
            this.btn_phieudien.Name = "btn_phieudien";
            this.btn_phieudien.Size = new System.Drawing.Size(174, 70);
            this.btn_phieudien.TabIndex = 0;
            this.btn_phieudien.Text = "Phiếu Điện";
            this.btn_phieudien.UseVisualStyleBackColor = true;
            this.btn_phieudien.Click += new System.EventHandler(this.btn_phieudien_Click);
            // 
            // btnPTT
            // 
            this.btnPTT.Location = new System.Drawing.Point(300, 208);
            this.btnPTT.Name = "btnPTT";
            this.btnPTT.Size = new System.Drawing.Size(174, 70);
            this.btnPTT.TabIndex = 0;
            this.btnPTT.Text = "Phiếu Thanh Toán";
            this.btnPTT.UseVisualStyleBackColor = true;
            this.btnPTT.Click += new System.EventHandler(this.btnPTT_Click);
            // 
            // FormManHinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(757, 363);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPhieuThue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_phieudien);
            this.Controls.Add(this.btnPhieuNuoc);
            this.Controls.Add(this.btnPTT);
            this.Controls.Add(this.btnPhong);
            this.Name = "FormManHinh";
            this.Text = "FormManHinh";
            this.Load += new System.EventHandler(this.FormManHinh_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPhong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPhieuThue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPhieuNuoc;
        private System.Windows.Forms.Button btn_phieudien;
        private System.Windows.Forms.Button btnPTT;
    }
}