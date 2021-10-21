using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    public class FileManager : IFileReadWrite
    {
        public FileManager()
        {
          
        }

        public string ReadAllText(string FileName)
        {
            string content = File.ReadAllText(FileName);
            return content;

        }

        public void WriteAllText(string FileName, string InputDetails)
        {
            File.WriteAllText(FileName, InputDetails);
        }
    }
}
