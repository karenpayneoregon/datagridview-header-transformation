namespace DataGridViewSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            dataGridView1 = new DataGridView();
            GetCurrentFromTableButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Image = Properties.Resources.Column_16x;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(16, 209);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(173, 31);
            button1.TabIndex = 0;
            button1.Text = "Set descriptions";
            button1.UseVisualStyleBackColor = true;
            button1.Click += SetDescriptionsButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 9);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(958, 192);
            dataGridView1.TabIndex = 1;
            // 
            // GetCurrentFromTableButton
            // 
            GetCurrentFromTableButton.Image = (Image)resources.GetObject("GetCurrentFromTableButton.Image");
            GetCurrentFromTableButton.ImageAlign = ContentAlignment.MiddleLeft;
            GetCurrentFromTableButton.Location = new Point(224, 209);
            GetCurrentFromTableButton.Margin = new Padding(3, 4, 3, 4);
            GetCurrentFromTableButton.Name = "GetCurrentFromTableButton";
            GetCurrentFromTableButton.Size = new Size(173, 31);
            GetCurrentFromTableButton.TabIndex = 2;
            GetCurrentFromTableButton.Text = "Current";
            GetCurrentFromTableButton.UseVisualStyleBackColor = true;
            GetCurrentFromTableButton.Click += GetCurrentFromTableButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(986, 253);
            Controls.Add(GetCurrentFromTableButton);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Karen Payne code sample";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private DataGridView dataGridView1;
        private Button GetCurrentFromTableButton;
    }
}