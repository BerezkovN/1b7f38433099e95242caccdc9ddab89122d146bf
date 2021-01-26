using System;
using System.Windows.Forms;
using AuthorDatabase;

public class Program
{
	[STAThread]
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new MainForm());
	}
}
