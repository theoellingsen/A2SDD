using System;
using System.Collections.Generic;
using System.Text;

namespace A2SDD
{
    public enum Campus { Hobart, Launceston, CradleCoast }
    class Researcher
    {
        public int ID { get; set; }

        public Type Type { get; set; }

        public String GivenName { get; set; }

        public String FamilyName { get; set; }

        public String Title { get; set; }

        public String School { get; set; }

        public String Unit { get; set; }

        public Campus Campus { get; set; }

        public String Email { get; set; }

        public String Photo { get; set; }

        public List<Position> Positions { get; set; }

        public List<Publication> Publications { get; set; }



        Position CurrentJob(Researcher r)
        {
            return r.Positions[0];
        }

        String CurrentJobTitle(Researcher r)
        {
            return r.Positions[0].Title(Positions[0]);
        }

        DateTime CurrentJobStart(Researcher r)
        {
            return r.Positions[0].Start;
        }

        Position GetEarliestJob(Researcher r)
        {
            int length;
            length = Positions.Count;

            return r.Positions[length];
        }

        DateTime EarliestStart(Researcher r)
        {
            Position p = GetEarliestJob(r);
            return p.Start;
        }

        float Tenure(Researcher r)
        {
            DateTime tenure = EarliestStart(r);
            return tenure.CompareTo(DateTime.Now);
        }
        int PublicationsCount(Researcher r)
        {
            return r.Publications.Count;
        }
    }
}