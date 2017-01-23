using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Renamer
{
    class Renamer
    {
        string folderPath =  "NEEDED";

        public Renamer() { }

        public void run()
        {
           DirectoryInfo d = new DirectoryInfo(folderPath);
            
            foreach (var file in d.GetFiles("*.mp4",SearchOption.AllDirectories))
            {
                Console.WriteLine(file.Name);

                //Replaces title with filename
                var newFile = TagLib.File.Create(file.FullName);
                newFile.Tag.Title = file.Name;
                newFile.Save();
            }
        }
    }
}
