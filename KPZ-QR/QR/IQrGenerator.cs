using System.Drawing;
using KPZ_QR.Styles;
namespace KPZ_QR.QR
{
    public interface IQrGenerator
    {
        Bitmap Generate(string text, IQrStyle style);

    }
}
