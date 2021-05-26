using A2SDD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2SDDWPF
{
    class PublicationsController
    {
        public static ObservableCollection<Publication> LoadPublicationsFor(Researcher r)
        {
            return new ObservableCollection<Publication>(Database.LoadPublications(r));
        }
    }
}
