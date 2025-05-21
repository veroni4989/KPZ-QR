using QRCoder;
using System.Drawing;

namespace KPZ_QR.Styles
{
    public class DefaultStyle : IQrStyle
    {
        public Bitmap ApplyStyle(QRCode qrCode)
        {
            return qrCode.GetGraphic(20, Color.Black, Color.White, true);
        }

        public string ApplyStyle(SvgQRCode qrCode)
        {
            return qrCode.GetGraphic(20, "#000000", "#FFFFFF", true);
        }

        public string ApplyStyle(AsciiQRCode qrCode)
        {
            return qrCode.GetGraphic(1, "█", " ");
        }
    }
}
