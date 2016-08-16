using System;
using System.Threading.Tasks;
using System.IO;
using Android.Content.Res;
using XLabs.Platform.Services.Media;
using Xamarin.Forms;
//using Foundation;
//using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(TesseractDemo.Droid.DService))]
namespace TesseractDemo.Droid
{
    public class DService : IDService
	{
        string textResult;
		public DService ()
		{
		}
		public async Task<String> GetText(MediaFile photo)
		{
            
            var imageBytes = new byte[photo.Source.Length];
            photo.Source.Position = 0;
            photo.Source.Read(imageBytes, 0, (int)photo.Source.Length);
            photo.Source.Position = 0;

            bool success = await MainActivity.api.SetImage(imageBytes);
			if (success)
			{
				textResult = MainActivity.api.Text;
			}
			return textResult;
		}
       

    }
}

