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
using System.Collections.ObjectModel;

namespace A2SDDWPF
{
	/// <summary>
	/// Interaction logic for PoorWindow.xaml
	/// </summary>
	public partial class PoorWindow : Window
	{
		private const string REPORT_LIST_KEY = "PoorListView";
		private Researcher researcher;
		public PoorWindow()
		{
			InitializeComponent();
			researcher = (Researcher)(Application.Current.FindResource(REPORT_LIST_KEY) as ObjectDataProvider).ObjectInstance;
		}

		private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			PoorListView.Items.Add(e);
		}

		private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
		{

		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
