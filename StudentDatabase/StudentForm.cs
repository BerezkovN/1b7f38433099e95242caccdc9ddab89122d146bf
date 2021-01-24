
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StudentDatabase
{
	public class StudentForm : Form
	{
		private TextBox NameTextBox;
		private TextBox MarkTextBox;
		private Button OKButton;
		private Button CloseButton;

		public Student student;
		public Student Student
		{
			set
			{
				this.student = value;
				this.NameTextBox.Text = this.student.Name;
				this.MarkTextBox.Text = Convert.ToString(this.student.Mark);
			}

			get
			{
				return this.student;
			}
		}

		public StudentForm()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.Size = new Size(180, 160);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.StartPosition = FormStartPosition.CenterScreen;

			Label NameLabel = new Label();
			NameLabel.Text = "Meno:";
			NameLabel.Size = new Size(60, 20);
			NameLabel.Location = new Point(20, 20);
			this.Controls.Add(NameLabel);

			this.NameTextBox = new TextBox();
			this.NameTextBox.Size = new Size(80, 20);
			this.NameTextBox.Location = new Point(80, 20);
			this.Controls.Add(this.NameTextBox);

			Label MarkLabel = new Label();
			MarkLabel.Text = "Znamka:";
			MarkLabel.Size = new Size(60, 20);
			MarkLabel.Location = new Point(20, 60);
			this.Controls.Add(MarkLabel);

			this.MarkTextBox = new TextBox();
			this.MarkTextBox.Size = new Size(80, 20);
			this.MarkTextBox.Location = new Point(80, 60);
			this.Controls.Add(this.MarkTextBox);

			this.OKButton = new Button();
			this.OKButton.Size = new Size(60, 20);
			this.OKButton.Location = new Point(20, 100);
			this.OKButton.Text = "OK";
			this.OKButton.Click += new EventHandler(this.OKButton_Click);
			this.Controls.Add(this.OKButton);

			this.CloseButton = new Button();
			this.CloseButton.Size = new Size(60, 20);
			this.CloseButton.Location = new Point(100, 100);
			this.CloseButton.Text = "Cancel";
			this.CloseButton.Click += new EventHandler(this.CloseButton_Click);
			this.Controls.Add(this.CloseButton);
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			if (this.student == null)
			{
				this.student = new Student();
			}

			this.student.Name = this.NameTextBox.Text;
			this.student.Mark = Convert.ToInt32(this.MarkTextBox.Text);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}