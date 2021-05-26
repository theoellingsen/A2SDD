﻿using System;
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
        PublicationType Type { get; set; }
        public String CiteAs { get; set; }
        public DateTime Available { get; set; }

        int Age(Publication p)
        {
            DateTime Current = DateTime.Now;

            return DateTime.Compare(Current, p.Available);
        }
    }
}