using System;
using System.Collections.Generic;
using System.Text;

namespace A2SDD
{
    enum EmploymentLevel { Student, A, B, C, D, E }
    class Position
    {
        public EmploymentLevel Level { get; set; }
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
        public String ToTitle(EmploymentLevel l)
        {
            String levelName = l.ToString();

            switch (levelName)
            {
                case "Student":
                    return "Student";
                case "A":
                    return "Postdoc";

                case "B":
                    return "Lecturer";

                case "C":
                    return "Senior Lecturer";

                case "D":
                    return "Associate Professor";

                case "E":
                    return "Professor";

                default:
                    return "No employment level";

            }
        }


    }
}
