using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TRANSPOREXA
{
	public partial class MainPage : ContentPage
	{
		//private const string url = "http://192.168.0.9/Apiasistencia/Login/login.php";
		private readonly HttpClient client = new HttpClient(); 
		public MainPage()
		{
			InitializeComponent();
		}

		private async void btnIngresar_Clicked(object sender, EventArgs e)
		{
			
			try {
				string url = "";
				if (txtPass.Text != "" && txtUser.Text != "")
				{
					url = "http://192.168.0.9/Apiasistencia/Login/login.php?user=" + txtUser.Text + "& pass=" + txtPass.Text;
					HttpClient client = new HttpClient();
					var response = await client.GetAsync(url);
					var json = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<TRANSPOREXA.Datos>(json);

					if (result != null)
					{
						string id = $"{result.id}";
						await Navigation.PushModalAsync(new Inicio(id));
					}
					else
					{
						valor.Text = "Usuario/Contraseña incorrecto";
					}
				}
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.ToString(), "Cerrar");
			}
 
		}
	}
}
