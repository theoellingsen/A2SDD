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
using A2SDD;

namespace A2SDDWPF
{
    /// <summary>
    /// Interaction logic for Supervisions.xaml
    /// </summary>
    public partial class Supervisions : Window
    {
        public Supervisions(Staff s)
        {
            InitializeComponent();
            SupervisiorsList.ItemsSource = ResearcherController.GetSupervisions(s);
        }
    }
}
