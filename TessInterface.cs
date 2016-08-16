using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLabs.Platform.Services.Media;

namespace TesseractDemo.ClassAndInterface
{
    public interface TessInterface
    {
        void GetTextFromImage(MediaFile mediafile);
    }
}
