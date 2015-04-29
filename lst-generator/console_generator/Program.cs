using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace console_generator
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length > 1 || args.Length == 0)
			{
				Console.WriteLine("Incorrect parameters input");
				return;
			}
			var fileExtension = "." + args[0].Trim('.');
			var files = Directory.GetFiles(Environment.CurrentDirectory).Where(x => Path.GetExtension(x) == fileExtension);
			if (!files.Any())
			{
				Console.WriteLine("This directory does not contains files with *.{0} extension", fileExtension);
				return;
			}
			foreach (var file in files)
				if (generate(file, fileExtension))
					Console.WriteLine("{0}{1} generated succesfully", Path.GetFileNameWithoutExtension(file), fileExtension);
				else
					Console.WriteLine("Cannot generate {0}{1}", Path.GetFileNameWithoutExtension(file), fileExtension);
			
		}

		static bool generate(string path,string extension)
		{
			if(!File.Exists(path))
				return false;
			var newPath = Path.ChangeExtension(path, ".txt");
			using(var reader = new StreamReader(File.OpenRead(path)))
			using (var writer = new StreamWriter(File.Open(newPath, FileMode.OpenOrCreate)))
			{
				int lineCounter = 1;
				writer.WriteLine("Generated on {0:F} by {1}", DateTime.Now, Environment.UserName);
				while (!reader.EndOfStream)
					writer.WriteLine("{0,3}) {1}", lineCounter++, reader.ReadLine());
			}
			return true;
		}
	}
}
