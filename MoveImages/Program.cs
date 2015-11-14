using System;
using System.IO;

namespace MoveImages
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Moving files...");
      foreach (var file in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.jpg"))
      {
        var filename = Path.GetFileNameWithoutExtension(file);
        if (filename.IndexOf('_') >= 3)
        {
          string dirName = Path.Combine(Directory.GetCurrentDirectory(), filename.Substring(0, 3));
          Directory.CreateDirectory(dirName);
          File.Move(file, Path.Combine(dirName, Path.GetFileName(file)));
        }
      }
      Console.WriteLine("Done");
      Console.ReadLine();
    }
  }
}
