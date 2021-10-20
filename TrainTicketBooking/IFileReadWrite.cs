using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    public interface IFileReadWrite
    {
        string ReadAllText(string FileName);
        void WriteAllText(string FileName, string InputDetails);

    }
}
