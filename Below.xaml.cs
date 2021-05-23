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
	/// Interaction logic for Below.xaml
	/// </summary>
	public partial class Below : Window
	{
		private Researcher r;
		private const string BELOW_LIST_KEY = "BelowListView";
		public Below()
		{
			InitializeComponent();
			r = (Researcher)(Application.Current.FindResource(BELOW_LIST_KEY) as ObjectDataProvider).ObjectInstance;
		}
		private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			BelowListView.Items.Add(e);
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

		private void BelowListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
