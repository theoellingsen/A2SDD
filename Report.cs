using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace A2SDD
{
	class Report
	{
		public static void OrderByPerformance(List<Researcher> rl)
		{

			List<Researcher> sorted;

			for (int i = 0; i < rl.Count(); i++)
			{
				int performance = 0;
				int maxID;

				if (performance < Staff.Performance(rl[i]))
				{
					maxID = rl[i].ID;
				}
			}

			
		}
	}
}
