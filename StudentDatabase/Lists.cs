using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace AuthorDatabase
{
	class Listable
    {
		public event EventHandler ListChanged;

		public void FireListChanged()
		{
			if (ListChanged != null)
				ListChanged(this, null);
		}
	}

	class AuthorList : Listable
	{
		private static AuthorList instance = new AuthorList();
		public static AuthorList Instance { get { return instance; } set { } }

		public List<Author> Authors = new List<Author>();
		

		public Author this[int index]
		{
			get
			{
				return Authors[index];
			}
			set
			{
				Authors[index] = value;
				FireListChanged();
			}
		}

		public int Count { get { return Authors.Count; } }

		public void Add(Author Author)
		{
			Authors.Add(Author);
			FireListChanged();
		}

		public void Remove(Author author)
		{
            foreach (var book in BookList.Instance.Books.ToList())
            {
				book.Authors.Remove(author);
				if (book.Authors.Count == 0)
					BookList.Instance.Books.Remove(book);
            }

			Authors.Remove(author);
			FireListChanged();
		}

		readonly public static string DefaultFileName = @"authors.xml";

		public void SaveToFile(String fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Author>));
			StreamWriter writer = new StreamWriter(fileName);
			serializer.Serialize(writer, this.Authors);
			writer.Close();
		}

		public void LoadFromFile(String fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Author>));
			StreamReader reader = new StreamReader(fileName);
			this.Authors = (List<Author>)serializer.Deserialize(reader);
			reader.Close();

			InisitializeAuthor();

			FireListChanged();
		}

		public void CreateFile(String fileName)
		{
			using (FileStream fs = File.Create(fileName))
			{

			}
		}

		private void InisitializeAuthor()
        {
            for (int i = 0; i < BookList.Instance.Books.Count; i++)
            {
                for (int b = 0; b < AuthorList.Instance.Authors.Count; b++)
                {
                    for (int c = 0; c < BookList.Instance.Books[i].Authors.Count; c++)
                    {
						if (AuthorList.Instance.Authors[b] == BookList.Instance.Books[i].Authors[c])
                        {
							AuthorList.Instance.Authors[b].Books.Add(BookList.Instance.Books[i]);
						}
                    }
				}
			}
            
        }
	}

	class BookList : Listable
	{
		private static BookList instance = new BookList();
		public static BookList Instance { get { return instance; } set { } }

		public List<Book> Books = new List<Book>();


		public Book this[int index]
		{
			get
			{
				return Books[index];
			}
			set
			{
				Books[index] = value;
				FireListChanged();
			}
		}

		public int Count { get { return Books.Count; } }



		public void Add(Book Book)
		{
			Books.Add(Book);
			FireListChanged();
		}

		public void Remove(Book book)
		{
            foreach (var author in AuthorList.Instance.Authors)
            {
				author.Books.Remove(book);
            }
			Books.Remove(book);
			FireListChanged();
		}

		readonly public static string DefaultFileName = @"books.xml";

		public void SaveToFile(String fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
			StreamWriter writer = new StreamWriter(fileName);
			serializer.Serialize(writer, this.Books);
			writer.Close();
		}

		public void LoadFromFile(String fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
			StreamReader reader = new StreamReader(fileName);
			this.Books = (List<Book>)serializer.Deserialize(reader);
			reader.Close();

			FireListChanged();
		}

		public void CreateFile(String fileName)
        {
			using (FileStream fs = File.Create(fileName)) { 
			}
		}

		public void SetCorrectAuthors()
        {
			for (int i = 0; i < this.Books.Count; i++)
			{
				for (int a = 0; a < this.Books[i].Authors.Count; a++)
				{
					foreach (var author in AuthorList.Instance.Authors)
					{
						if (author == this.Books[i].Authors[a])
							this.Books[i].Authors[a] = author;

					}
				}
			}
		}
	}
}
