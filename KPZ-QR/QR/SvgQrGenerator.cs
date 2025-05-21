using KPZ_QR.Styles;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KPZ_QR.QR
{
    public class SvgQrGenerator : IQrGenerator
    {
        public Bitmap Generate(string text, IQrStyle style)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            SvgQRCode qrCode = new SvgQRCode(qrCodeData);
            string svg = qrCode.GetGraphic(20);

            // Конвертація SVG у Bitmap (через проміжне зображення)
            var svgStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svg));
            var svgDocument = Svg.SvgDocument.Open<Svg.SvgDocument>(svgStream);
            return svgDocument.Draw();


        }
    }
}
