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
			//sorted list of researchers by performance
			/* ReportLevel level = ParseEnum<ReportLevel>(ReportLevel);
			List<Researcher> sorted;
			float performance = 0;
			int maxID;
			int maxPerformance = 99999;

			for (int i = 0; i < rl.Count(); i++)
			{

				if (performance < Staff.Performance(rl[i]))
				{
					performance = Staff.Performance(rl[i]);
					maxID = rl[i].ID;
				}
			} */
			return rl;


		}
	}
}
