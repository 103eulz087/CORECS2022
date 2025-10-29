using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.SalesModel
{
    public class TerminalDto
    {
        //public long Id { get; set; }                     // bigint
        public long MerchantID { get; set; }            // bigint
        public string TerminalID { get; set; }          // varchar(100)
        public string TerminalName { get; set; }        // varchar(100)
        public DateTime DateAdded { get; set; }         // date
        public TimeSpan TimeAdded { get; set; }         // time
        public DateTime DateTimeAdded { get; set; }     // datetime
        public DateTime DateUpdated { get; set; }       // date
        public TimeSpan TimeUpdated { get; set; }       // time
        public DateTime DateTimeUpdated { get; set; }   // datetime
        public byte Status { get; set; }                // tinyint
        public string UATKeys { get; set; }             // nvarchar
        public string PRODKeys { get; set; }            // nvarchar
    }
}
