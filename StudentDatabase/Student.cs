using System;

namespace StudentDatabase
{
	public class Student
	{
		public String Name { get; set; }
		public int Mark { get; set; }
		public String Note { get; set; }

		public override String ToString()
		{
			String note = String.IsNullOrEmpty(Note) ? Note : "n/a";
			return String.Format("{0}: {1} ({2})", Name, Mark, note);
		}
	}
}
