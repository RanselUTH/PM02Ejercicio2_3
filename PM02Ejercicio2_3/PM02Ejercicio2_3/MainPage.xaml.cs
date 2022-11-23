using Plugin.AudioRecorder;
using PM02Ejercicio2_3.Models;
using PM02Ejercicio2_3.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM02Ejercicio2_3
{
    public partial class MainPage : ContentPage
    {
        private  AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        public string pathaudio, filename;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void B_Grabar_Clicked(object sender, EventArgs e)
        {
            var permiso = await Permissions.RequestAsync<Permissions.Microphone>();
            var permiso1 = await Permissions.RequestAsync<Permissions.StorageRead>();
            var permiso2 = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (string.IsNullOrEmpty(txtdescripcion.Text))
            {

                await DisplayAlert("Mensage", "Descripcion Vacia ", "Ok");
                return;
            }


            if (permiso != PermissionStatus.Granted & permiso1 != PermissionStatus.Granted & permiso2 != PermissionStatus.Granted)
            {
                return;

            }




            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();
                audioPlayer.Play(audioRecorderService.GetAudioFilePath());
              
            }
            else
            {
                await audioRecorderService.StartRecording();
            }
        }


        private async void B_Pausa_Clicked(object sender, EventArgs e)
        {

            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();
                

                if (await DisplayAlert("Lista Audio", "Desea Reproducir Audio", "Si", "No"))
                {
                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());
                }

            }
            else
            {
                await audioRecorderService.StartRecording();
            }
        }

        private async void B_Ver_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }


        private void btnguardar_Clicked(object sender, EventArgs e)
        {
            Guardar();
        }

        private async void Guardar()
        {
            if (audioRecorderService.GetAudioFilePath() == null)
            {
                await DisplayAlert("¡Alerta!", "Por Favor Grabar Audio", "OK");
                return;
            }
            if (audioRecorderService.FilePath != null)
            {

                var storage = audioRecorderService.GetAudioFileStream();

                filename = Path.Combine(FileSystem.CacheDirectory, DateTime.Now.ToString("ddMMyyyymmss") + "_VoiceNote.wav");

                using (var fileStoage = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    storage.CopyTo(fileStoage);
                }

                pathaudio = filename;

            }
            Audios audios = new Audios();
            audios.url = pathaudio;
            audios.descripcion = txtdescripcion.Text;
            audios.fecha = DateTime.Now;


            var record = await App.BDAudios.GrabarAudio(audios);
            if (record == 1)
            {
                await DisplayAlert("", " Audio " + audios.descripcion + " Guardado en File " + audios.url, "Ok");
                txtdescripcion.Text = "";
                audioRecorderService = new AudioRecorderService();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo guardar", "Ok");
            }

        }


    }
}
