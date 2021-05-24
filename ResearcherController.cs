﻿using System;
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

        public static ObservableCollection<Researcher> FilterSearch(String entered)
        {
            var baseList = LoadResearchers();
            String search = entered.ToLower();

            var selected1 = from some in baseList
                           where string.Equals(search, some.FamilyName)
                            select some;

            var selected2 = from some in baseList
                       where string.Equals(search, some.GivenName)
                       select some;

            var selected3 = from some in baseList
                       where string.Equals(search, some.Title)
                       select some;

            var merge = selected1.Concat(selected2)
                                 .Concat(selected3)
                                 .ToList();

            return new ObservableCollection<Researcher>(merge);
        }
    }
}
