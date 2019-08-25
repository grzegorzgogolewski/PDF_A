namespace PDF_A
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.buttonInputDir = new System.Windows.Forms.Button();
            this.textBoxInputDir = new System.Windows.Forms.TextBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.buttonOutputDir = new System.Windows.Forms.Button();
            this.textBoxOutputDir = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.buttonInputDir);
            this.groupBoxInput.Controls.Add(this.textBoxInputDir);
            this.groupBoxInput.Location = new System.Drawing.Point(13, 13);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(415, 78);
            this.groupBoxInput.TabIndex = 0;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Folder wejściowy";
            // 
            // buttonInputDir
            // 
            this.buttonInputDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInputDir.Location = new System.Drawing.Point(309, 46);
            this.buttonInputDir.Name = "buttonInputDir";
            this.buttonInputDir.Size = new System.Drawing.Size(100, 23);
            this.buttonInputDir.TabIndex = 1;
            this.buttonInputDir.Text = "Wybierz folder";
            this.buttonInputDir.UseVisualStyleBackColor = true;
            this.buttonInputDir.Click += new System.EventHandler(this.ButtonInputDir_Click);
            // 
            // textBoxInputDir
            // 
            this.textBoxInputDir.Location = new System.Drawing.Point(7, 20);
            this.textBoxInputDir.Name = "textBoxInputDir";
            this.textBoxInputDir.Size = new System.Drawing.Size(402, 20);
            this.textBoxInputDir.TabIndex = 0;
            this.textBoxInputDir.TextChanged += new System.EventHandler(this.TextBoxInputDir_TextChanged);
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.buttonOutputDir);
            this.groupBoxOutput.Controls.Add(this.textBoxOutputDir);
            this.groupBoxOutput.Location = new System.Drawing.Point(13, 97);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(415, 78);
            this.groupBoxOutput.TabIndex = 2;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Folder wynikowy";
            // 
            // buttonOutputDir
            // 
            this.buttonOutputDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOutputDir.Location = new System.Drawing.Point(309, 46);
            this.buttonOutputDir.Name = "buttonOutputDir";
            this.buttonOutputDir.Size = new System.Drawing.Size(100, 23);
            this.buttonOutputDir.TabIndex = 1;
            this.buttonOutputDir.Text = "Wybierz folder";
            this.buttonOutputDir.UseVisualStyleBackColor = true;
            this.buttonOutputDir.Click += new System.EventHandler(this.ButtonOutputDir_Click);
            // 
            // textBoxOutputDir
            // 
            this.textBoxOutputDir.Location = new System.Drawing.Point(7, 20);
            this.textBoxOutputDir.Name = "textBoxOutputDir";
            this.textBoxOutputDir.Size = new System.Drawing.Size(402, 20);
            this.textBoxOutputDir.TabIndex = 0;
            this.textBoxOutputDir.TextChanged += new System.EventHandler(this.TextBoxOutputDir_TextChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStart.Location = new System.Drawing.Point(13, 181);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(415, 30);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "START";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 224);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "PDF/A - Convert";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Button buttonInputDir;
        private System.Windows.Forms.TextBox textBoxInputDir;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.Button buttonOutputDir;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.Button buttonStart;
    }
}

