using System;
using System.Collections.Generic;
using System.Text;
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
	/// Interaction logic for Poor.xaml
	/// </summary>
	public partial class Poor : Window
	{
		private Researcher researcher;
		private const string RESEARCHER_LIST_KEY = "PoorListView";
		public Poor()
		{
			InitializeComponent();
			researcher = (Researcher)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
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

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void PoorListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
