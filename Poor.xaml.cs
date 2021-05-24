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
		private Researcher r;
		private const string POOR_LIST_KEY = "PoorListView";
		public Poor()
		{
			InitializeComponent();
			r = (Researcher)(Application.Current.FindResource(POOR_LIST_KEY) as ObjectDataProvider).ObjectInstance;
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
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var filtered = Report.OrderByPerformance("Poor");
			String emails = "";

			for (int i = 0; i < filtered.Count; i++)
			{
				if (i == filtered.Count - 1)
				{
					emails += filtered[i].Email;
				}
				else
				{
					emails += filtered[i].Email + ", ";
				}
			}

			System.Windows.Clipboard.SetData(DataFormats.Text, emails);

			MessageBox.Show("Emails Copied!");
		}
	}
}
