using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace A2SDD
{
    //ESSENTIAL
    enum PublicationLevel { A, B, C, D, E }
    public class Staff : Researcher
    {
        public List<Student> Supervisions { get; set; }

        public static double ThreeYearAverage(int ID)
        {
            //Initiate database object (will be replaced with PublicationController at later date)

            // Create list of publications from given researcher
            DateTime end = new DateTime (2016, 1, 1);
            DateTime start = end.AddYears(-3);
            int publications = Database.LoadPublications3YearAVerage(ID, start, end);
          

            // Return average over three years
            return publications / 3;
        }

        public static float SupervisionsCount(int ID)
        {
            int supervisions = Database.LoadSupervisionsCount(ID);

            return supervisions;
        }

        public static double calcPerformance(Staff s)
        {
            // Performance is three year average divided by performance level
            if (s.Positions.Count == 0)
			{
                return -1;
			} else
			{
                double value=0;
				switch (s.CurrentLevel)
				{
                    case CurrentLevel.A:
					{
                            value = 0.5;
                            break;
					}
                    case CurrentLevel.B:
                    {
                            value = 1;
                            break;
                    }
                    case CurrentLevel.C:
                    {
                            value = 2;
                            break;
                    }
                    case CurrentLevel.D:
                    {
                            value = 3.2;
                            break;
                    }
                    case CurrentLevel.E:
                    {
                            value = 4;
                            break;
                    }
                }
                s.Performance = (ThreeYearAverage(s.ID) / value );
                return value;
            }
        }
    }
}
