using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace StudentDatabase
{
	class StudentList
	{
		private static StudentList instance = new StudentList();
		public static StudentList Instance { get { return instance; } }

		private List<Student> Students = new List<Student>();
		public event EventHandler ListChanged;

		public Student this[int index]
		{
			get
			{
				return Students[index];
			}
			set
			{
				Students[index] = value;
				FireStudentListChanged();
			}
		}

		public int Count { get { return Students.Count; } }

		public void FireStudentListChanged()
		{
			if (ListChanged != null)
				ListChanged(this, null);
		}

		public void Add(Student student)
		{
			Students.Add(student);
			FireStudentListChanged();
		}

		public void Remove(Student student)
		{
			Students.Remove(student);
			FireStudentListChanged();
		}

		readonly public static string DefaultFileName = @"studenti.xml";

		public void SaveToFile(String fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
			StreamWriter writer = new StreamWriter(fileName);
			serializer.Serialize(writer, this.Students);
			writer.Close();
		}

		public void LoadFromFile(String fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
			StreamReader reader = new StreamReader(fileName);
			this.Students = (List<Student>)serializer.Deserialize(reader);
			reader.Close();

			FireStudentListChanged();
		}

		public List<Student> StudentsWithMark(int mark)
		{
			return this.Students.Where(s => s.Mark == mark).ToList();
		}
	}
}
