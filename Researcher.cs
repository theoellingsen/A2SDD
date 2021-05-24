using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using A2SDDWPF;

namespace A2SDD
{

    public enum Campus { Hobart, Launceston, CradleCoast }

    public enum EmployeeType { Staff, Student }
    class Researcher : ResearcherController
    {
        public int ID { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public String GivenName { get; set; }

        public String FamilyName { get; set; }

        public String Title { get; set; }

        public String Unit { get; set; }

        public Campus Campus { get; set; }

        public String Email { get; set; }

        public String Photo { get; set; }

        public DateTime Start { get; set; }

        public DateTime CurrentStart { get; set; }

        public List<Position> Positions { get; set; }

        public List<Publication> Publications { get; set; }

        public float Performance { get; set; }

        

       
        public ObservableCollection<Researcher> getReport(String level)
		{
            ObservableCollection<Researcher> ol = Report.OrderByPerformance(level);
            return ol;
		}
       /*public ObservableCollection<Researcher> GetViewableList()
		{
            List<Researcher> rl = Database.LoadReseacherListView();
            ObservableCollection<Researcher> oc = new ObservableCollection<Researcher>(rl);
            return oc;
		}*/

        public Position CurrentJob()
        {
            return Positions.Last();
        }

        public String CurrentJobTitle()
        {
            return Positions.Last().Title();
        }

        public DateTime CurrentJobStart()
        {
            return Positions.Last().Start;
        }

        public Position GetEarliestJob()
        {
            return Positions.Last();
        }

        public DateTime EarliestStart()
        {
            Position p = GetEarliestJob();
            return p.Start;
        }

        public float Tenure()
        {
            DateTime tenure = EarliestStart();
            return tenure.CompareTo(DateTime.Now);
        }
        public int PublicationsCount()
        {
            return Publications.Count;
        }
    }
}