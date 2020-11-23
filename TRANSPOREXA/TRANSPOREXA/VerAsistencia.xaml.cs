using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace TRANSPOREXA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerAsistencia : ContentPage
	{
		private ObservableCollection<TRANSPOREXA.DatosAsistencia> _posts;
		public VerAsistencia(string id)
		{
			InitializeComponent();
			lblID.Text = id;
		}

	 

		private async void btnBuscar_Clicked(object sender, EventArgs e)
		{
			try
			{
				string url = ""; 
				string fecha3= fecha.Date.ToString("yyyy-MM-dd");

				if (fecha3 != "" )
				{
					url = "http://192.168.0.9/Apiasistencia/Asistencia/verasistencia.php?fecha="+fecha3+"&user="+lblID.Text ;
					HttpClient client = new HttpClient();
					var response = await client.GetStringAsync(url); 
					List<TRANSPOREXA.DatosAsistencia> posts = JsonConvert.DeserializeObject<List<TRANSPOREXA.DatosAsistencia>>(response);
					_posts = new ObservableCollection<DatosAsistencia>(posts);

					if (_posts != null)
					{
						lblResultado.Text = "Resultados";
						datosAsistencia.ItemsSource = _posts;

					}
					else
					{
						//valor.Text = "Usuario/Contraseña incorrecto";
					}
				}
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.ToString(), "Cerrar");
			}
		}

		private async  void btnCancelar_Clicked_1(object sender, EventArgs e)
		{
			string id = lblID.Text;
			await Navigation.PushModalAsync(new Inicio(id));

		}
	}
}