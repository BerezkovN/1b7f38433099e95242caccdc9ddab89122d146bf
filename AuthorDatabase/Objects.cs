using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace AuthorDatabase
{
	public class Author
	{
		public String Name { get; set; }
		public DateTime BirthDate;
		[XmlIgnore]
		public List<Book> Books;

		public Author() {
			Books = new List<Book>();
		}

		public static bool operator== (Author a1, Author a2)
        {
			if (a1.Name == a2.Name && a1.BirthDate == a2.BirthDate)
            {
				return true;
            }
			return false;
        }

		public static bool operator!=(Author a1, Author a2)
		{
			if (a1.Name != a2.Name || a1.BirthDate != a2.BirthDate)
			{
				return true;
			}
			return false;
		}

		public override String ToString()
		{
			return Name;
		}
	}

	public class Book
    {
		public String Name { get; set; }
		public List<Author> Authors = new List<Author>();

		public string StringAuthors
        {
			get
            {
				string result = "";
				foreach (Author author in Authors)
				{
					result += author.Name + ", ";
				}

				return result.Substring(0, result.Length - 2);
			}
        }

        public override string ToString()
        {
			return Name + " by " + StringAuthors;
        }
    }
}
