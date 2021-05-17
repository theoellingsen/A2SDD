using System;
using System.Collections.Generic;
using System.Text;

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
           /* // Initiate database object (will be replaced with PublicationController at later date)
            Database db = new Database();

            // Create list of publications from given researcher
            List<Publication> publications = new List<Publication>(db.LoadPublications(ID));

            // Select the publications less than three years old
            var selected = from publications
                            where (DateTime.Today - selected.Year).Years <= 3
                            select selected;

            // Create list pf publications from selected
            List<Publication> lastThree = new List<Publication>(selected);
            
            // Return average over three years
            return lastThree.Count() / 3; */
        }

        public static float Performance(Researcher r)
        {
            // Performance is three year average divided by performance level
            return ThreeYearAverage(r.ID) /  r.Positions[0].Level * 10;
        }
        
    }
}
