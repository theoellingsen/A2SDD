using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace A2SDD
{
	enum ReportLevel {Poor, BelowExpectations, MeetingMinimum, StarPerformer}
	class Report : Researcher
	{
		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}

		private static ObservableCollection<Researcher> CreateObservable<Researcher>(IEnumerable<Researcher> enumerable)
		{
			return new ObservableCollection<Researcher>(enumerable);
		}


		public static ObservableCollection<Researcher> OrderByPerformance(String ReportLevel)
		{
			List<Researcher> rl = Database.LoadReseacherListView();
			List<Researcher> filtered = new List<Researcher>();
			int lowcutoff = 0;
			int highcutoff = 0;

			ReportLevel l = ParseEnum<ReportLevel>(ReportLevel);

			switch (l)
			{
				case A2SDD.ReportLevel.Poor:
					{
						lowcutoff = -1;
						highcutoff = 70;
						break;
					}
				case A2SDD.ReportLevel.BelowExpectations:
					{
						lowcutoff = 70;
						highcutoff = 110;
						break;
					}
				case A2SDD.ReportLevel.MeetingMinimum:
					{
						lowcutoff = 110;
						highcutoff = 200;
						break;
					}
				case A2SDD.ReportLevel.StarPerformer:
					{
						lowcutoff = 200;
						highcutoff = int.MaxValue;
						break;
					}
			}

			foreach(Researcher r in rl)
			{
				if (Staff.calcPerformance(Database.LoadStaff(r)) > lowcutoff && Staff.calcPerformance(Database.LoadStaff(r)) <= highcutoff)
				{
					filtered.Add(r);
				}
			}

			IOrderedEnumerable<Researcher> x;
			if ((l == A2SDD.ReportLevel.BelowExpectations) || (l == A2SDD.ReportLevel.Poor))
			{
				 var ordered = from r in filtered
							  orderby r.Performance
							  select r; ;
				x = ordered;
			} else
			{
				 var ordered = from r in filtered
							  orderby r.Performance descending
							  select r; ;
				x = ordered;
			}
			

			ObservableCollection<Researcher> ol = CreateObservable<Researcher>(x);
			return ol;
		}
	}
}
