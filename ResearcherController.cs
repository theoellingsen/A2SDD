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
        //The example shown here follows the pattern discussed in the Module 6 summary slides,
        //maintaining two collections, a master and a 'viewable' one (which is the 'view model'
        //in Microsoft's Model-View-ViewModel approach to Model-View-Controller)
        private List<Researcher> reseachers;
        public List<Researcher> Researchers { get { return reseachers; } set { } }

        private ObservableCollection<Researcher> viewableResearchers;
        public ObservableCollection<Researcher> ViewableResearchers { get { return viewableResearchers; } set { } }

        public ResearcherController()
        {
            reseachers = Database.LoadReseacherListView();
            viewableResearchers = new ObservableCollection<Researcher>(reseachers);
        }

        public ObservableCollection<Researcher> GetViewableList()
        {
            return ViewableResearchers;
        }

        //The below method was in wk 8 but not wk 10 tutorial, which had the above method - is the one below still needed?
        /// <summary>
        /// Displays the list of employees on the console.
        /// </summary>
        public void Display()
        {
            reseachers.ForEach(Console.WriteLine);
        }

        /// <summary>
        /// Returns the Employee with the given ID, or null if no such employee exists.
        /// </summary>
        public Researcher Use(int id)
        {
            foreach (Researcher e in reseachers)
            {
                if (e.ID == id)
                {
                    return e;
                }
            }
            return null;
            //FYI, if you have an interest in lambda expressions the above could be achieved with:
            //return staff.First(e => e.ID == id);
        }

        //This version of Filter modifies the viewable list instead of returning a new list,
        //but the procedure is almost the same
        public void Filter(EmploymentLevel level)
        {
            var selected = from Researcher e in reseachers
                           where level == EmploymentLevel.A
                           select e;
            viewableResearchers.Clear();
            //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
            selected.ToList().ForEach(viewableResearchers.Add);
        }

    }
}
