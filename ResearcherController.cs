using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2SDD;

namespace A2SDDWPF
{
    class ResearcherController
    {

        public static ObservableCollection<Researcher> LoadResearchers()
        {
            var newList = Database.LoadReseacherListView();
            ObservableCollection<Researcher> rList = new ObservableCollection<Researcher>(newList);
            return rList;
        }

    }
}
