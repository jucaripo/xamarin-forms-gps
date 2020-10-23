using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace gps.Views
{
    public partial class GeoCodingPage : ContentPage
    {
        CancellationTokenSource cts;

        public GeoCodingPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {


            try
            {

                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                // obtenemos el token de cancelación
                cts = new CancellationTokenSource();
                // y pasamos a pedir que el celular calcule la ubicación 
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                // el objeto location nos entrega los valores para trabajar con ellos.


                if (location != null)
                {



                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        txtAdminArea.Text = placemark.AdminArea;
                        txtCountryCode.Text = placemark.CountryCode;
                        txtCountryName.Text = placemark.CountryName;
                        txtFeatureName.Text = placemark.FeatureName;
                        txtLocality.Text    = placemark.Locality;
                        txtPostalCode.Text  = placemark.PostalCode;
                        txtSubAdminArea.Text = placemark.SubAdminArea;
                        txtSubLocality.Text = placemark.SubLocality;
                        txtSubThoroughfare.Text = placemark.SubThoroughfare;
                        txtThoroughfare.Text = placemark.Thoroughfare;


                        var geocodeAddress =
                            $"AdminArea:       {placemark.AdminArea}\n" +
                            $"CountryCode:     {placemark.CountryCode}\n" +
                            $"CountryName:     {placemark.CountryName}\n" +
                            $"FeatureName:     {placemark.FeatureName}\n" +
                            $"Locality:        {placemark.Locality}\n" +
                            $"PostalCode:      {placemark.PostalCode}\n" +
                            $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                            $"SubLocality:     {placemark.SubLocality}\n" +
                            $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                            $"Thoroughfare:    {placemark.Thoroughfare}\n";

                        Console.WriteLine(geocodeAddress);
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }

        }
    }
}
