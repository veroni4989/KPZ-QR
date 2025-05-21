using QRCoder;
using System;
using System.Drawing;
using KPZ_QR.Styles;

namespace KPZ_QR.QR
{
    public class AsciiQrGenerator : IQrGenerator
    {
        public Bitmap Generate(string text, IQrStyle style)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            // ОБОВ’ЯЗКОВО додаємо це:
            AsciiQRCode asciiQr = new AsciiQRCode(qrCodeData);

            // Далі можемо вже:
            string ascii = style.ApplyStyle(asciiQr);

            // Створимо зображення з ASCII
            Font font = new Font("Consolas", 10);
            SizeF size;

            using (var tempImg = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(tempImg))
            {
                size = g.MeasureString(ascii, font);
            }

            Bitmap bmp = new Bitmap((int)size.Width, (int)size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.DrawString(ascii, font, Brushes.Black, new PointF(0, 0));
            }

            return bmp;
        }
    }
}
