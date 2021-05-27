using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using A2SDD;


namespace A2SDDWPF
{
    /// <summary>
    /// Interaction logic for PublicationsDetailsView.xaml
    /// </summary>
    public partial class PublicationsDetailsView : Window
    {
        public PublicationsDetailsView(Publication p)
        {
            InitializeComponent();
            publication_title.Text = p.Title;
            publication_authors.Content = p.Authors;
            publication_type.Content = PublicationsController.ParsePublicationType(p.Type);
            publication_cite.Text = p.CiteAs;
            publication_available.Content = p.Available.ToString("dd/MM/yyyy");
            publication_age.Content = p.Age() + " year(s) old";
        }
    }
}
