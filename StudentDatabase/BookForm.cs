
using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AuthorDatabase
{
    public class BookForm : Form
    {
        private TextBox NameTextBox;
        private Button OKButton;
        private Button CloseButton;
        private Label NameLabel;
        private Label AuthorLable;
        private ComboBox comboBox1;
        private Label label2;
        private Label authorsLabel;
        private List<Author> AddedAuthors = new List<Author>();
        private bool isEdit = false;
        private Book book;
        public Book Book
        {
            set
            {
                this.book = value;
                this.NameTextBox.Text = this.book.Name;
                this.AddedAuthors = new List<Author>(book.Authors);
                this.book.Authors.Clear();
                this.isEdit = true;
                SetAuthorsLabel();
            }

            get
            {
                return this.book;
            }
        }

        public BookForm()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComponent()
        {
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.AuthorLable = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.authorsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.Location = new System.Drawing.Point(77, 25);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(60, 20);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(156, 22);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(121, 22);
            this.NameTextBox.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(16, 151);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(85, 37);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(241, 151);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(85, 37);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Cancel";
            // 
            // AuthorLable
            // 
            this.AuthorLable.Location = new System.Drawing.Point(52, 110);
            this.AuthorLable.Name = "AuthorLable";
            this.AuthorLable.Size = new System.Drawing.Size(85, 20);
            this.AuthorLable.TabIndex = 6;
            this.AuthorLable.Text = "Add author";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(156, 107);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Authors:";
            // 
            // authorsLabel
            // 
            this.authorsLabel.AutoSize = true;
            this.authorsLabel.Location = new System.Drawing.Point(156, 68);
            this.authorsLabel.Name = "authorsLabel";
            this.authorsLabel.Size = new System.Drawing.Size(0, 17);
            this.authorsLabel.TabIndex = 9;
            // 
            // BookForm
            // 
            this.ClientSize = new System.Drawing.Size(349, 200);
            this.Controls.Add(this.authorsLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.AuthorLable);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add book";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeComboBox()
        {
            foreach (var author in AuthorList.Instance.Authors)
            {
                comboBox1.Items.Add(author);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (this.book == null)
            {
                this.book = new Book();
            }

            if (this.NameTextBox.Text == "" )
            {
                return;
            }
            
            this.book.Name = this.NameTextBox.Text;
            this.book.Authors.AddRange(AddedAuthors);

            if (!isEdit)
            {
                foreach (var author in AddedAuthors)
                {
                    author.Books.Add(this.book);
                }
            }
            else
            {
                BookList.Instance.SaveToFile(BookList.DefaultFileName);
                BookList.Instance.LoadFromFile(BookList.DefaultFileName);
                AuthorList.Instance.LoadFromFile(AuthorList.DefaultFileName);
            }

            BookList.Instance.FireListChanged();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Author selected = (Author)comboBox1.SelectedItem;

            if (AddedAuthors.IndexOf(selected) == -1)
                AddedAuthors.Add(selected);
            else
                AddedAuthors.Remove(selected);

            SetAuthorsLabel();
        }

        private void SetAuthorsLabel()
        {
            string result = "";

            foreach (var author in AddedAuthors)
            {
                result += author.Name + ", ";
            }

            if (result.Length - 2 > 0)
                authorsLabel.Text = result.Substring(0, result.Length - 2);
            else
                authorsLabel.Text = "";
        }
    }
}