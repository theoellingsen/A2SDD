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
using System.Windows.Navigation;
using System.Windows.Shapes;
using A2SDDWPF;
using A2SDD;

namespace A2SDDWPF
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private Researcher researcher;
		private const string RESEARCHER_LIST_KEY = "ResearcherListView";
		public MainWindow()
		{
		InitializeComponent();

			researcher = (Researcher)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
		}

		private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ResearcherListView.Items.Add(e);
		}

		private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
		{

		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}
	}
}
