using System;
using System.Collections.Generic;
using System.Text;

namespace A2SDD
{
    public enum EmploymentLevel {Student, A, B, C, D, E }
    public class Position
    {
        public EmploymentLevel Level { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public String StartString { get { return DateFormat(Start); } set { } }
        public String titleName { get { return ToTitle(Level); } set { } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns
        /// 
        public String Title()
        {
            return ToTitle(Level);
        }

        public String DateFormat(DateTime format)
		{
            String dateOnlyString = format.ToShortDateString();
            return dateOnlyString;

        }

        /// <summary>
        /// Takes the level of the researcher and returns their title string.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        String ToTitle(EmploymentLevel l)
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
