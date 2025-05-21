using QRCoder;
using System.Drawing;

namespace KPZ_QR.Styles
{
    public class RedStyle : IQrStyle
    {
        public Bitmap ApplyStyle(QRCode qrCode)
        {
            return qrCode.GetGraphic(20, Color.Red, Color.White, true);
        }

        public string ApplyStyle(AsciiQRCode qrCode)
        {
            return qrCode.GetGraphic(1, "▓", " ");
        }

        public string ApplyStyle(SvgQRCode qrCode)
        {
            return qrCode.GetGraphic(20, "#FF0000", "#FFFFFF");
        }
    }
}
