using System;
using System.Collections.Generic;
using System.Text;

namespace A2SDD
{
    public class Student : Researcher
    {
        public string Degree {  get; set; }

        public int Supervisor_ID { get; set; }
    }
}
