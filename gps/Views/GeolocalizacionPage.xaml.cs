using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace gps.Views
{
    public partial class GeolocalizacionPage : ContentPage
    {
        CancellationTokenSource cts;

        public GeolocalizacionPage()
        {
            InitializeComponent();
        }

        async void GpsCache_Clicked(System.Object sender, System.EventArgs e)
        {

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    txtLatCache.Text = location.Latitude.ToString();
                    txtLongCache.Text = location.Longitude.ToString();
                    txtAltitudCache.Text = location.Altitude.ToString();
                    txtVelocidadCache.Text = location.Speed.ToString();

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await DisplayAlert("Mi App", "Mi dispositivo no soporta GPS","Continuar");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await DisplayAlert("Mi App", "Mi dispositivo genero un error", "Continuar");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await DisplayAlert("Mi App", "Mi dispositivo no tiene permiso para el gps", "Continuar");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await DisplayAlert("Mi App", "Mi dispositivo fallo al traer mi ubicación", "Continuar");
            }
        }


        async void Gps_Clicked(System.Object sender, System.EventArgs e)
        {
            
            try
            {
                // configuramos el nivel de precisión y el tiempo de espera de respuesta
                // es importante considerar que a mayor precisión  se requiere mas tiempo de calculo.
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                // obtenemos el token de cancelación
                cts = new CancellationTokenSource();
                // y pasamos a pedir que el celular calcule la ubicación 
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                // el objeto location nos entrega los valores para trabajar con ellos.

                // location.Accuracy :  precisión horizontal
                // location.VerticalAccuracy : precisión vertical
                // location.Timestamp : fecha hora de la lectura por el celular 
                // location.Speed :  velocidad de desplazamiento entre diferentes lecturas, da el promedio de velocidad entre puntos
                // location.Course : número de grados con respecto al norte
                // location.Altitude : altitude  que reporta el celular cuando este puede dar la altitud da null o 0 si no hay dato
                // location.OpenMapsAsync  : abre mapa con las coordenadas
                
                if (location != null)
                {
                    txtLat.Text = location.Latitude.ToString();
                    txtLong.Text = location.Longitude.ToString();
                    txtAltitud.Text = location.Altitude.ToString();
                    txtVelocidad.Text = location.Speed.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await DisplayAlert("Mi App", "Mi dispositivo no soporta GPS", "Continuar");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await DisplayAlert("Mi App", "Mi dispositivo genero un error", "Continuar");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await DisplayAlert("Mi App", "Mi dispositivo no tiene permiso para el gps", "Continuar");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await DisplayAlert("Mi App", "Mi dispositivo fallo al traer mi ubicación", "Continuar");
            }
        }

    }
}
