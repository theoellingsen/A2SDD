using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace A2SDD
{
    //ESSENTIAL
    enum PublicationLevel { A, B, C, D, E }
    class Staff : Researcher
    {
        
        private PublicationLevel Level { get; set; }


        public static float ThreeYearAverage(int ID)
        {
            //Initiate database object (will be replaced with PublicationController at later date)

            // Create list of publications from given researcher
            DateTime end = new DateTime(2015, 1, 1);
            DateTime start = end.AddYears(-3);
            int publications = Database.LoadPublications3YearAVerage(ID, start, end);

            // Select the publications less than three years old
           

            // Create list pf publications from selected
          

            // Return average over three years
            return publications / 3;
        }

        public static float calcPerformance(Researcher r)
        {
            // Performance is three year average divided by performance level
            if (r.Positions.Count == 0)
			{
                return -1;
			} else
			{
                float value=0;
				switch (r.Positions[0].Level)
				{
                    case EmploymentLevel.A:
					{
                            value = 5;
                            break;
					}
                    case EmploymentLevel.B:
                        {
                            value = 10;
                            break;
                        }
                    case EmploymentLevel.C:
                        {
                            value = 20;
                            break;
                        }
                    case EmploymentLevel.D:
                        {
                            value = 32;
                            break;
                        }
                    case EmploymentLevel.E:
                        {
                            value = 40;
                            break;
                        }
                }
                r.Performance = (ThreeYearAverage(r.ID) / (value / 10))*100;
                return (ThreeYearAverage(r.ID) / (value/10))*100;
            }
        }
    }
}
