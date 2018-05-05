namespace AgeingHaresSimulator
{
    partial class ExceptionViewForm
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
            this.stackTraceRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // stackTraceRichTextBox
            // 
            this.stackTraceRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackTraceRichTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stackTraceRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.stackTraceRichTextBox.Name = "stackTraceRichTextBox";
            this.stackTraceRichTextBox.ReadOnly = true;
            this.stackTraceRichTextBox.Size = new System.Drawing.Size(766, 558);
            this.stackTraceRichTextBox.TabIndex = 0;
            this.stackTraceRichTextBox.Text = "";
            // 
            // ExceptionViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 558);
            this.Controls.Add(this.stackTraceRichTextBox);
            this.Name = "ExceptionViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подробности ошибки";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ExceptionViewForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox stackTraceRichTextBox;
    }
}