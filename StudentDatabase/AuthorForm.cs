
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AuthorDatabase
{
	public class AuthorForm : Form
	{
		private TextBox NameTextBox;
		private TextBox BirthDateTextBox;
		private Button OKButton;
		private Button CloseButton;
        private Label NameLabel;
        private Label MarkLabel;
        public Author author;
		public Author Author
		{
			set
			{
				this.author = value;
				this.NameTextBox.Text = this.author.Name;
				this.BirthDateTextBox.Text = this.author.BirthDate.ToShortDateString();
			}

			get
			{
				return this.author;
			}
		}

		public AuthorForm()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.MarkLabel = new System.Windows.Forms.Label();
            this.BirthDateTextBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.Location = new System.Drawing.Point(20, 20);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(60, 20);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(109, 20);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(119, 22);
            this.NameTextBox.TabIndex = 1;
            // 
            // MarkLabel
            // 
            this.MarkLabel.Location = new System.Drawing.Point(20, 60);
            this.MarkLabel.Name = "MarkLabel";
            this.MarkLabel.Size = new System.Drawing.Size(70, 20);
            this.MarkLabel.TabIndex = 2;
            this.MarkLabel.Text = "BirthDate:";
            // 
            // MarkTextBox
            // 
            this.BirthDateTextBox.Location = new System.Drawing.Point(109, 58);
            this.BirthDateTextBox.Name = "MarkTextBox";
            this.BirthDateTextBox.Size = new System.Drawing.Size(119, 22);
            this.BirthDateTextBox.TabIndex = 3;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(20, 100);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(85, 37);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(170, 100);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(85, 37);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Cancel";
            // 
            // AuthorForm
            // 
            this.ClientSize = new System.Drawing.Size(279, 149);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.MarkLabel);
            this.Controls.Add(this.BirthDateTextBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AuthorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			if ((Object)this.author == null)
			{
				this.author = new Author();
			}

			this.author.Name = this.NameTextBox.Text;
            this.author.BirthDate = Convert.ToDateTime(this.BirthDateTextBox.Text);

            AuthorList.Instance.FireListChanged();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}