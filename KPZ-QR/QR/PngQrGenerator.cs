using QRCoder;
using System.Drawing;
using KPZ_QR.Styles;

namespace KPZ_QR.QR
{
    public class PngQrGenerator : IQrGenerator
    {
        public Bitmap Generate(string text, IQrStyle style)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            return style.ApplyStyle(qrCode);
        }

    }
}
