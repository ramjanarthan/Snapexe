using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesseractDemo.ClassAndInterface;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace TesseractDemo.Pages
{
    public partial class CameraPage : ContentPage
    {
        public IMediaPicker _mediaPicker;
        public CameraPage()
        {
            InitializeComponent();
            _mediaPicker = DependencyService.Get<IMediaPicker>();
            tick.Source = ImageSource.FromFile("greenCheckmark.png");
            BusyIndicator busy = new BusyIndicator();
            active.IsVisible = false;
            active.IsRunning = false;

           
            button1.Clicked += TakePic;
            
        }

        async void TakePic(Object s, EventArgs ea)
        {
            var photo = await TakePicture();
            
            if (photo != null)
            {
               
                active.IsRunning = true;
                active.IsVisible = true;

                string text = await DependencyService.Get<IDService>().GetText(photo);
                

                if (text.Equals(null))
                {
                    _recognizedTextLabel.Text = "Please take a better image";

                }else
                {
                    active.IsVisible = false;
                    active.IsRunning = false;

                  
                    MessagingCenter.Send<CameraPage, string>(this, "textDecoded", text);
              
                    tick.IsVisible = true;
                    await ((View)tick).FadeTo(1, 1, Easing.Linear);
                    await ((View)tick).FadeTo(0, 2000, Easing.Linear);
                    
                    
                }

               
            }
        }

        private async Task<MediaFile> TakePicture()
        {
            //Setup();

            
            MediaFile mediaFile = null;

            await this._mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions
            { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var s = t.Exception.InnerException.ToString();
                }
                else if (t.IsCanceled)
                {
                    var canceled = true;
                }
                else
                {
                    mediaFile = t.Result;

                   
                }
            });

            return mediaFile;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }


    }
}
