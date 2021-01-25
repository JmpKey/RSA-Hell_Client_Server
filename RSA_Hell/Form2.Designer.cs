
namespace RSA_Hell
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
            Form2.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            Form2.listBox1.FormattingEnabled = true;
            Form2.listBox1.Location = new System.Drawing.Point(12, 12);
            Form2.listBox1.Name = "listBox1";
            Form2.listBox1.Size = new System.Drawing.Size(382, 329);
            Form2.listBox1.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 351);
            this.Controls.Add(Form2.listBox1);
            this.Name = "Form2";
            this.Text = "RSA & Hell - Server";
            this.ResumeLayout(false);

        }

        #endregion

        internal static System.Windows.Forms.ListBox listBox1;
    }
}