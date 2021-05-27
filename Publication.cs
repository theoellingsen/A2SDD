using System;
using System.Collections.Generic;
using System.Text;

namespace A2SDD
{
    //ESSENTIAL

    public enum PublicationType { Conference, Journal, Other }
    public class Publication
    {
        public String DOI { get; set; }
        public String Title { get; set; }
        public String Authors { get; set; }
        public String Year { get; set; }
        public PublicationType Type { get; set; }
        public String CiteAs { get; set; }
        public DateTime Available { get; set; }

        public int Age()
        {
            DateTime Current = DateTime.Now;

            return Current.Year - Available.Year;
        }
    }
}