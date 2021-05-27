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

    public enum CurrentLevel { A, B, C, D, E }
    public class Researcher : ResearcherController
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

        public CurrentLevel CurrentLevel { get; set; }

        public DateTime CurrentStart { get; set; }

        public List<Position> Positions { get; set; }

        public List<Publication> Publications { get; set; }

        public double Performance { get; set; }

        
        public ObservableCollection<Position> GetPositions()
		{
            ObservableCollection<Position> pos = new ObservableCollection<Position>();
            foreach (Position p in Positions)
			{
                if (Positions.IndexOf(p)+1 != Positions.Count)
				{
                    pos.Add(p);
                }
               
			} 
            return pos;
        }

        public ObservableCollection<Researcher> getReport(String level)
		{
            ObservableCollection<Researcher> ol = Report.OrderByPerformance(level);
            return ol;
		}
        public Position CurrentJob()
        {
            
            return Positions.Last();
        }

        public String CurrentJobTitle()
        {
            if (Positions.Count == 0)
			{

                return "Implement Get Degree";
			} else
			{
                return Positions.Last().Title();
            }
           
        }

        public DateTime CurrentJobStart()
        {
            if (EmployeeType == EmployeeType.Student)
			{
                return Start;
			}
            return Positions.Last().Start;
        }

        public Double Tenure()
        {
            DateTime now = DateTime.Now;
            Double tenure = (DateTime.Today - Start).TotalDays;
            tenure = tenure / 365.2425;
            tenure = Math.Round(tenure, 1);
            return tenure;
        }
    }
}