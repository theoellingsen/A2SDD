using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2SDD;

namespace A2SDDWPF
{
    public class ResearcherController
    {
        public static ObservableCollection<Student> GetSupervisions(Staff s)
        {
            return new ObservableCollection<Student>(Database.LoadSupervisions(s));
        }

        public static int CountPublications(Researcher r)
        {
            return Database.LoadCountPublications(r.ID);
        }

        public static Student LoadStudent(Researcher r)
		{
            return Database.LoadStudent(r);
		}

        public static Staff LoadStaff(Researcher r)
        {
            return Database.LoadStaff(r);
        }

        public static ObservableCollection<Researcher> LoadResearchers()
        {
            var newList = Database.LoadReseacherListView();
            ObservableCollection<Researcher> rList = new ObservableCollection<Researcher>(newList); 
            return rList;
        }

        public static ObservableCollection<Staff> LoadStaff()
        {
            var baseList = Database.LoadReseacherListView();
            var staffList = from some in baseList
                            where some.EmployeeType == EmployeeType.Staff
                            select some;
            ObservableCollection<Staff> returnList = new ObservableCollection<Staff>();
            foreach(var staffMember in staffList)
            {
                returnList.Add(Database.LoadStaff(staffMember));
                returnList.Last().Positions = Database.LoadPosition(staffMember);
            }

            return returnList;
        }

        public static ObservableCollection<Researcher> FilterBy(EmploymentLevel e)
        {
            var baseList = LoadStaff();
            var selected = from some in baseList
                           where some.CurrentJob().Level == e
                           select some;
            return new ObservableCollection<Researcher>(selected);
        }

        public static ObservableCollection<Researcher> Students()
		{
            var baseList = LoadResearchers();

            var selected = from some in baseList
                           where some.Positions.Count == 0
                           select some;
            return new ObservableCollection<Researcher>(selected);
		}

        public static ObservableCollection<Researcher> FilterSearch(String search)
        {
            var baseList = LoadResearchers();

            var selected1 = from some in baseList
                           where string.Equals(search.ToLower(), some.FamilyName.ToLower())
                            select some;

            var selected2 = from some in baseList
                       where string.Equals(search.ToLower(), some.GivenName.ToLower())
                       select some;

            var selected3 = from some in baseList
                       where string.Equals(search.ToLower(), some.Title.ToLower())
                       select some;

            var merge = selected1.Concat(selected2)
                                 .Concat(selected3)
                                 .ToList();

            return new ObservableCollection<Researcher>(merge);

        }

        public static String ParseCampus(Campus c)
        {
            if(c == Campus.CradleCoast)
            {
                return "Cradle Coast";
            }
            return c.ToString();
        }
    }
}
