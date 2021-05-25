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
	/// Interaction logic for ResearcherDetailsView.xaml
	/// </summary>
	public partial class ResearcherDetailsView : Window
	{
        
		public ResearcherDetailsView(Researcher r)
		{
			InitializeComponent();

            if (r.EmployeeType == EmployeeType.Staff)
			{
                Staff researcher = ResearcherController.LoadStaff(r);

                label_title.Content = researcher.Title + " " + researcher.GivenName + " " + researcher.FamilyName;
                label_campus.Content = researcher.Campus;
                label_positionCurrent.Content = researcher.CurrentJobTitle();
                label_school.Content = researcher.Unit;
                PositionorStudent.Content = "Current Position:";

            } else
			{
                Student researcher = ResearcherController.LoadStudent(r);
                label_title.Content = researcher.Title + " " + researcher.GivenName + " " + researcher.FamilyName;
                label_campus.Content = researcher.Campus;
                label_positionCurrent.Content = researcher.Degree;
                label_school.Content = researcher.Unit;
                PositionorStudent.Content = "Current Degree:";
            }

            //Display Photo (WHEN PHOTO IS COLLECTED CORRECTLY FROM DATABASE, THIS SHOULD WORK)
            /*
            String photo = researcher.Photo;

            var image = new Image();
            var fullFilePath = @photo;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            image.Source = bitmap;
            pic.Children.Add(image);
            */ 
        }
    }
}
