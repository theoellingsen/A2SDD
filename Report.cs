using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace A2SDD
{
	enum ReportLevel {Poor, BelowExpectations, MeetingMinimum, StarPerformer}

	/*
	 *  <ObjectDataProvider ObjectType="{x:Type local:Researcher}" 
                      MethodName="OrderByPerformance" x:Key="poorListView">
            <ObjectDataProvider.MethodParameters>
                <sys:String>Poor</sys:String>
            </ObjectDataProvider.MethodParameters>
        </ObjectDataProvider>
	*/
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
				if (Staff.Performance(r) > lowcutoff && Staff.Performance(r) <= highcutoff)
				{
					filtered.Add(r);
				}
			}

			var ordered = from r in filtered
								orderby r.Performance
								select r; ;

			ObservableCollection<Researcher> ol = CreateObservable<Researcher>(ordered);
			return ol;
		}
	}
}
