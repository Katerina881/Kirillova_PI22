namespace ZavodConservView
{
    partial class FormClientOrders
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
            this.ButtonToPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // ButtonToPdf
            // 
            this.ButtonToPdf.Location = new System.Drawing.Point(621, 11);
            this.ButtonToPdf.Name = "ButtonToPdf";
            this.ButtonToPdf.Size = new System.Drawing.Size(95, 23);
            this.ButtonToPdf.TabIndex = 3;
            this.ButtonToPdf.Text = "PDF";
            this.ButtonToPdf.UseVisualStyleBackColor = true;
            this.ButtonToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.LocalReport.ReportEmbeddedResource = "ZavodConservView.ReportOrders.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(2, 40);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(796, 408);
            this.reportViewer.TabIndex = 6;
            this.reportViewer.Load += new System.EventHandler(this.reportViewer_Load);
            // 
            // FormClientOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.ButtonToPdf);
            this.Name = "FormClientOrders";
            this.Text = "Заказы клиентов";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ButtonToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}