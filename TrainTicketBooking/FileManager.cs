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
            string content = null; 
            try
            {
                content = File.ReadAllText(FileName);
                //return content;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"error in file! {ex.Message}");
            }

            return content;

        }

        public void WriteAllText(string FileName, string InputDetails)
        {
            try
            {
                File.WriteAllText(FileName, InputDetails);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"error in file! {ex.Message}");
            }

        }
    }
}
