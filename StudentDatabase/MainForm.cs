
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace StudentDatabase
{
	public class MainForm : Form
	{
		public ListBox StudentListBox;
		public Button AddButton;
		public Button EditButton;
		public Button DeleteButton;

		public MainForm()
		{
			InitializeComponent();
			StudentList.Instance.ListChanged += new EventHandler(this.OnStudentListChanged);
			Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
		}

		private void InitializeComponent()
		{
			this.Size = new Size(300, 250);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Load += new EventHandler(this.OnFormLoad);
			this.Closed += new EventHandler(this.OnFormClosed);

			this.StudentListBox = new ListBox();
			this.StudentListBox.Size = new System.Drawing.Size(200, 180);
			this.StudentListBox.Location = new System.Drawing.Point(10, 10);
			this.StudentListBox.SelectedIndexChanged += new EventHandler(this.OnStudentListBoxSelectedIndexChanged);
			this.Controls.Add(StudentListBox);

			this.AddButton = new Button();
			this.AddButton.Size = new Size(60, 20);
			this.AddButton.Location = new Point(220, 10);
			this.AddButton.Text = "Pridat";
			this.AddButton.Click += new EventHandler(this.OnAddButtonClick);
			this.Controls.Add(AddButton);

			this.EditButton = new Button();
			this.EditButton.Size = new Size(60, 20);
			this.EditButton.Location = new Point(220, 50);
			this.EditButton.Text = "Editovat";
			this.EditButton.Enabled = false;
			this.EditButton.Click += new EventHandler(this.OnEditButtonClick);
			this.Controls.Add(EditButton);

			this.DeleteButton = new Button();
			this.DeleteButton.Size = new Size(60, 20);
			this.DeleteButton.Location = new Point(220, 90);
			this.DeleteButton.Text = "Vymazat";
			this.DeleteButton.Click += new EventHandler(this.OnDeleteButtonClick);
			this.Controls.Add(DeleteButton);

			//main menu
			MainMenu mainMenu = new MainMenu();

			MenuItem fileMenuItem = mainMenu.MenuItems.Add("&File");
			MenuItem editMenuItem = mainMenu.MenuItems.Add("&Edit");
			MenuItem filterMenuItem = mainMenu.MenuItems.Add("&Vyhladaj");

			fileMenuItem.MenuItems.Add("&New");
			fileMenuItem.MenuItems.Add("&Open", new EventHandler(this.OnOpenMenuItemClick));
			fileMenuItem.MenuItems.Add("&Save");

			editMenuItem.MenuItems.Add("&New");
			editMenuItem.MenuItems.Add("&Edit");
			editMenuItem.MenuItems.Add("&Remove");

			filterMenuItem.MenuItems.Add("&Najlepsi", new EventHandler(this.OnBestMenuItemClick));
			filterMenuItem.MenuItems.Add("&Najhorsi", new EventHandler(this.OnWorstMenuItemClick));

			this.Menu = mainMenu;
		}

		// obsluhy udalosti
		private void OnStudentListChanged(object sender, EventArgs e)
		{
			this.EditButton.Enabled = false;

			this.StudentListBox.Items.Clear();
			for (int i = 0; i < StudentList.Instance.Count; i++)
			{
				this.StudentListBox.Items.Add(StudentList.Instance[i]);
			}
		}

		private void OnAddButtonClick(object sender, EventArgs e)
		{
			StudentForm studentForm = new StudentForm();
			if (studentForm.ShowDialog() == DialogResult.OK)
			{
				StudentList.Instance.Add(studentForm.Student);
			}
		}

		private void OnEditButtonClick(object sender, EventArgs e)
		{
			if (this.StudentListBox.SelectedItem == null) return;

			StudentForm studentForm = new StudentForm();
			studentForm.Student = this.StudentListBox.SelectedItem as Student;
			studentForm.ShowDialog();
		}

		private void OnDeleteButtonClick(object sender, EventArgs e)
		{
			if (this.StudentListBox.SelectedItem == null) return;
			StudentList.Instance.Remove(this.StudentListBox.SelectedItem as Student);
		}

		private void OnStudentListBoxSelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool selected = this.StudentListBox.SelectedItem != null;
			this.EditButton.Enabled = selected;
		}

		private void OnFormLoad(object sender, System.EventArgs e)
		{
			try
			{
				StudentList.Instance.LoadFromFile(StudentList.DefaultFileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Chyba pri nacitani zo suboru: " + ex.ToString());
			}
		}

		private void OnFormClosed(object sender, System.EventArgs e)
		{
		}

		private void OnApplicationExit(object sender, EventArgs e)
		{
			try
			{
				StudentList.Instance.SaveToFile(StudentList.DefaultFileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Chyba pri ukladani do suboru: " + ex.ToString());
			}
		}

		private void OnBestMenuItemClick(object sender, System.EventArgs e)
		{
			List<Student> students = StudentList.Instance.StudentsWithMark(1);
			if (students.Count == 0)
				MessageBox.Show("Nenasiel sa ani jeden vyborny student");
			else
				MessageBox.Show("Vyborni studenti: " + string.Join(", ", students.Select(s => s.Name).ToArray()));
		}

		private void OnWorstMenuItemClick(object sender, System.EventArgs e)
		{

		}

		private void OnOpenMenuItemClick(object sender, System.EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = ".";
				openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
				openFileDialog.FilterIndex = 0;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() != DialogResult.OK) return;

				try
				{
					StudentList.Instance.LoadFromFile(openFileDialog.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Chyba pri nacitani zo suboru: " + ex.ToString());
				}
			}
		}

		// pomocne funkcie

	}
}
