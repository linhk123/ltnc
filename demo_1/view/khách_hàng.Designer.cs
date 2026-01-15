namespace demo_1
{
    partial class khách_hàng
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            btn_Ten_kh = new TextBox();
            btn_SDT = new TextBox();
            label3 = new Label();
            btn_Lưu = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 21);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 78);
            label2.Name = "label2";
            label2.Size = new Size(97, 20);
            label2.TabIndex = 1;
            label2.Text = "Số điện thoại";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // btn_Ten_kh
            // 
            btn_Ten_kh.Location = new Point(153, 18);
            btn_Ten_kh.Name = "btn_Ten_kh";
            btn_Ten_kh.Size = new Size(201, 27);
            btn_Ten_kh.TabIndex = 3;
            btn_Ten_kh.TextChanged += btn_Ten_kh_TextChanged;
            // 
            // btn_SDT
            // 
            btn_SDT.Location = new Point(153, 75);
            btn_SDT.Name = "btn_SDT";
            btn_SDT.Size = new Size(201, 27);
            btn_SDT.TabIndex = 4;
            btn_SDT.TextChanged += btn_SDT_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 21);
            label3.Name = "label3";
            label3.Size = new Size(109, 20);
            label3.TabIndex = 5;
            label3.Text = "tên khách hàng";
            // 
            // btn_Lưu
            // 
            btn_Lưu.Location = new Point(260, 155);
            btn_Lưu.Name = "btn_Lưu";
            btn_Lưu.Size = new Size(94, 29);
            btn_Lưu.TabIndex = 6;
            btn_Lưu.Text = "Thanh toán";
            btn_Lưu.UseVisualStyleBackColor = true;
            btn_Lưu.Click += btn_Lưu_Click;
            // 
            // khách_hàng
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_Lưu);
            Controls.Add(label3);
            Controls.Add(btn_SDT);
            Controls.Add(btn_Ten_kh);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "khách_hàng";
            Text = "khách_hàng";
            Load += khách_hàng_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox btn_Ten_kh;
        private TextBox btn_SDT;
        private Label label3;
        private Button btn_Lưu;
    }
}