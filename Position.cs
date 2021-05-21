using System;
using System.Collections.Generic;
using System.Text;

namespace A2SDD
{
    enum Level { A, B, C, D, E }
    class Position
    {
        public Level Level { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns
        public String Title(Position p)
        {
            return ToTitle(p.Level);
        }

        /// <summary>
        /// Takes the level of the researcher and returns their title string.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public String ToTitle(Level l)
        {
            if (l.CompareTo("A") == 0)
            {
                return "Postdoc";
            }
            else if (l.CompareTo("B") == 0)
            {
                return "Lecturer";
            }
            else if (l.CompareTo("C") == 0)
            {
                return "Senior Lecturer";
            }
            else if (l.CompareTo("D") == 0)
            {
                return "Associate Professor";
            }
            else if (l.CompareTo("E") == 0)
            {
                return "Professor";
            }
            return "NULL";
        }


    }
}
