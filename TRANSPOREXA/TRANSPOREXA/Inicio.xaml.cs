using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRANSPOREXA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
	{
		public Inicio( string id)
		{
			InitializeComponent();
			lblID.Text = id;
		}

		private async void btnAsistencia_Clicked(object sender, EventArgs e)
		{
			string id = lblID.Text;
			await Navigation.PushModalAsync(new Asistencia(id));

		}

		private async void btnHistorial_Clicked(object sender, EventArgs e)
		{
			string id = lblID.Text;
			await Navigation.PushModalAsync(new VerAsistencia(id));
		}

		private async void btnPermiso_Clicked(object sender, EventArgs e)
		{
			string id = lblID.Text;
			await Navigation.PushModalAsync(new Permisos(id));
		}

		private async void btnAtrasos_Clicked(object sender, EventArgs e)
		{
			string id = lblID.Text;
			await Navigation.PushModalAsync(new MainPage());

		}
	}
}