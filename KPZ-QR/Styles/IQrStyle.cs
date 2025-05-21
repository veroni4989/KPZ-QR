using QRCoder;
using System.Drawing;

namespace KPZ_QR.Styles
{
    public interface IQrStyle
    {
        Bitmap ApplyStyle(QRCode qrCode);             // для PNG
        string ApplyStyle(SvgQRCode qrCode);          // для SVG
        string ApplyStyle(AsciiQRCode qrCode);        // для ASCII
    }
}
