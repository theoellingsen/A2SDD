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
			//sorted list of researchers by performance
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
			}



		}
	}
}
