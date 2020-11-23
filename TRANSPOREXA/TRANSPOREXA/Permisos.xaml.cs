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
using System.Net;

namespace TRANSPOREXA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Permisos : ContentPage
	{
		public Permisos(string id)
		{
			InitializeComponent();
			lblID.Text = id;
		}

		private async void btnGuardar_Clicked(object sender, EventArgs e)
		{
			try
			{
				WebClient cliente = new WebClient();
				string fecha2 = fecha.Date.ToString("yyyy-MM-dd");
				
				if (fecha2.Equals("") || justificacion.Text.Equals(""))
				{
					await DisplayAlert("alerta", "Debe seleccionar un fecha y justificar su permiso.", "ok");
				}
				else
				{
					var parametros = new System.Collections.Specialized.NameValueCollection();
					parametros.Add("user", lblID.Text);
					parametros.Add("fecha", fecha2);
					parametros.Add("descripcion", justificacion.Text);
					cliente.UploadValues("http://192.168.0.9/Apiasistencia/Permisos/permisos.php", "POST", parametros);
					await DisplayAlert("alerta",  "Permiso registrado correctamente", "ok");
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