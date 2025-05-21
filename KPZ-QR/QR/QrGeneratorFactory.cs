using System;
namespace KPZ_QR.QR
{
    public enum QrGeneratorType
    {
        Png,
        Svg,
        Ascii
    }

    public static class QrGeneratorFactory
    {
        public static IQrGenerator Create(QrGeneratorType type)
        {
            return type switch
            {
                QrGeneratorType.Png => new PngQrGenerator(),
                QrGeneratorType.Svg => new SvgQrGenerator(),
                QrGeneratorType.Ascii => new AsciiQrGenerator(),
                _ => throw new ArgumentException("Невідомий тип генератора"),
            };
        }
    }
}
