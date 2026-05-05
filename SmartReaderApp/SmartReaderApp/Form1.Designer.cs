namespace SmartReaderApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxInput = new TextBox();
            dataGridViewResults = new DataGridView();
            buttonAnalyze = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            SuspendLayout();
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 12);
            textBoxInput.Multiline = true;
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(404, 422);
            textBoxInput.TabIndex = 0;
            // 
            // dataGridViewResults
            // 
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResults.Location = new Point(521, 22);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.Size = new Size(309, 338);
            dataGridViewResults.TabIndex = 1;
            // 
            // buttonAnalyze
            // 
            buttonAnalyze.Location = new Point(422, 185);
            buttonAnalyze.Name = "buttonAnalyze";
            buttonAnalyze.Size = new Size(93, 39);
            buttonAnalyze.TabIndex = 2;
            buttonAnalyze.Text = "Проаналізувати терміни";
            buttonAnalyze.UseVisualStyleBackColor = true;
            buttonAnalyze.Click += buttonAnalyze_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 471);
            Controls.Add(buttonAnalyze);
            Controls.Add(dataGridViewResults);
            Controls.Add(textBoxInput);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxInput;
        private DataGridView dataGridViewResults;
        private Button buttonAnalyze;
    }
}
