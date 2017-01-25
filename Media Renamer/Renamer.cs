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
        string folderPath = "F:\\Videos\\TV Shows\\Archer"; //remember to use \\ instead of \

        public Renamer() { }

        public void run()
        {
            DirectoryInfo d = new DirectoryInfo(folderPath);

            //this is ugly.... find a way to do it all in one GETFILES call
            foreach (FileInfo file in d.GetFiles("*.avi", SearchOption.AllDirectories))
                rename(file);
            foreach (FileInfo file in d.GetFiles("*.mp4", SearchOption.AllDirectories))
                rename(file);
            foreach (FileInfo file in d.GetFiles("*.mkv", SearchOption.AllDirectories))
                rename(file);
            foreach (FileInfo file in d.GetFiles("*.wmv", SearchOption.AllDirectories))
                rename(file);
        }

        private void rename(FileInfo file)
        {
            string fileName = "";
            fileName = correctName(file);
            //Replaces title with filename
            Console.WriteLine(fileName);
           // setFileTitle(file, fileName);

            string newFileName = file.DirectoryName + "\\" + fileName;
            File.Move(file.FullName, newFileName);

        }

        private string correctName(FileInfo file)
        {
            string extension = Path.GetExtension(file.FullName);
            string fileName = Path.GetFileNameWithoutExtension(file.FullName);
            fileName = removeJunk(fileName);

            return fileName + extension;
        }

        private string removeJunk(string input)
        {
            string fileName = input;

            //NEED TO CORRECT THIS
            //handle SXXEXX instead of numbers
            char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int index = fileName.LastIndexOfAny(numbers);
            if (index > 0)
            {
                fileName = fileName.Substring(0, index + 1);
            }

            fileName = fileName.Replace('.', ' ');  //removes fullstops
            return fileName;
        }

        private void setFileTitle(FileInfo file, string fileName)
        {
            try
            {
                var newFile = TagLib.File.Create(file.FullName);
                newFile.Tag.Title = fileName;
                newFile.Save();
            }
            catch (Exception) { }
        }
    }
}