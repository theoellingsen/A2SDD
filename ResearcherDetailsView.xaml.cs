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
        Researcher researcher;
		public ResearcherDetailsView(Researcher r)
		{
			InitializeComponent();

            researcher = ResearcherController.LoadDetails(r);

            //Title
            label_title.Content = researcher.Title + " " + researcher.GivenName + " " + researcher.FamilyName;

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
