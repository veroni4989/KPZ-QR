using QRCoder;
using System.Drawing;

namespace KPZ_QR.Styles
{
    public class BlueStyle : IQrStyle
    {
        public Bitmap ApplyStyle(QRCode qrCode)
        {
            return qrCode.GetGraphic(20, Color.Blue, Color.White, true);
        }

        public string ApplyStyle(AsciiQRCode qrCode)
        {
            return qrCode.GetGraphic(1, "█", " ");
        }

        public string ApplyStyle(SvgQRCode qrCode)
        {
            return qrCode.GetGraphic(20, "#0000FF", "#FFFFFF");
        }
    }
}
