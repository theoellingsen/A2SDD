using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace A2SDD
{
	enum ReportLevel {Poor, BelowExpectations, MeetingMinimum, StarPerformer}


	class Report
	{
		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}
		public static List<Researcher> OrderByPerformance(List<Researcher> rl, String ReportLevel)
		{
			
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
			return filtered;
			
			//return rl;

		}
	}
}
