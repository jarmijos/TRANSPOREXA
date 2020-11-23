using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRANSPOREXA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Asistencia : ContentPage
	{
		string lati;
		string longi;

		public Asistencia(string id)
		{
			InitializeComponent();
			lblID.Text = id;
			DateTime fecha = DateTime.Now.Date;
			lblFecha.Text = fecha.ToShortDateString(); 
			evento.Items.Add("ENTRADA");
			evento.Items.Add("SALIDA");
		}

		private async void btnGuardar_Clicked(object sender, EventArgs e)
		{
			try {
				//////////GEO LOCALIZACION
				var locator = CrossGeolocator.Current; //Acceso a la API
				locator.DesiredAccuracy = 50; //Precisión (en metros)
				if (locator.IsGeolocationAvailable) //Servicio existente en el dispositivo
				{
					if (locator.IsGeolocationEnabled) //GPS activado en el dispositivo
					{
						if (!locator.IsListening) //Comprueba si el dispositivo escucha al servicio
						{
							await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 5); //Inicio de la escucha
						}
						locator.PositionChanged += (cambio, args) =>
						{
							var loc = args.Position;
							longi  = loc.Longitude.ToString();
						 
							lati = loc.Latitude.ToString();
							 
						};
					}
				}



				///////////////////////////
				WebClient cliente = new WebClient();
				string tipo = evento.SelectedItem.ToString();
				var parametros = new System.Collections.Specialized.NameValueCollection();
				parametros.Add("user", lblID.Text);
				parametros.Add("tipo", tipo);
				parametros.Add("latitud", lati);
				parametros.Add("longitud", longi);
				if (tipo.Equals("")) {
					await DisplayAlert("alerta", "Debe seleccionar un evento para registrar su asistencia", "ok");
				}else{
					cliente.UploadValues("http://192.168.0.9/Apiasistencia/Asistencia/asistencia.php", "POST", parametros);
					await DisplayAlert("alerta", tipo+" registrada correctamente", "ok");
				}
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.ToString(), "Cerrar");
			}
  

		}
 

		private async void btnCancelar_Clicked(object sender, EventArgs e)
		{
			string id = lblID.Text;
			await Navigation.PushModalAsync(new Inicio(id));
		}
	}
}