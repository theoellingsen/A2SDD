using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace A2SDD
{
    //ESSENTIAL

    class Staff : Researcher
    {
        private enum PublicationLevel
        {
            A = 5,
            B = 10,
            C = 20,
            D = 32,
            E = 40,
        }

        private PublicationLevel Level { get; set; }


        public static float ThreeYearAverage(int ID)
        {
            //Initiate database object (will be replaced with PublicationController at later date)

            // Create list of publications from given researcher
            DateTime start = new DateTime(2012, 1, 1);
            DateTime end = new DateTime(2015, 1, 1);
            int publications = Database.LoadPublications3YearAVerage(ID, start, end);

            // Select the publications less than three years old
           

            // Create list pf publications from selected
          

            // Return average over three years
            return publications / 3;
        }

        public static float Performance(Researcher r)
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
                    case A2SDD.Level.A:
					{
                            value = 5;
                            break;
					}
                    case A2SDD.Level.B:
                        {
                            value = 10;
                            break;
                        }
                    case A2SDD.Level.C:
                        {
                            value = 20;
                            break;
                        }
                    case A2SDD.Level.D:
                        {
                            value = 32;
                            break;
                        }
                    case A2SDD.Level.E:
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
