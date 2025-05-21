# KPZ-QR

## Лабораторна робота №2 з курсу "Комплексне проєктування програмного забезпечення"

### Тема: Шаблони проєктування

#### Мета роботи
Навчитись використовувати шаблони проєктування у реальному застосунку. Реалізувати генерацію QR-кодів різними способами з можливістю стилізації та збереженням історії.

---

## Функціональність

- Генерація QR-кодів у форматах PNG, SVG, ASCII.
- Підтримка стилів: за замовчуванням, червоний, синій.
- Збереження QR-коду у файл.
- Збереження історії генерацій (формується у ListBox).
- Зручний UI з Windows Forms.

---

## Шаблони проєктування

- **Strategy** — для різних способів генерації QR (SVG, PNG, ASCII).
- **Factory** (опціонально) — для створення генераторів або стилів.
- **Dependency Injection** (через передачу стилів у метод `Generate`).

---

## Інтерфейси

```csharp
public interface IQrGenerator
{
    Bitmap Generate(string text, IQrStyle style);
}

public interface IQrStyle
{
    Bitmap ApplyStyle(QRCode qrCode);
    string ApplyStyle(SvgQRCode svgQrCode);
    string ApplyStyle(AsciiQRCode asciiQrCode);
}

## Вигляд форми
(/img/form.png)
(/img/formhistory.png)

## Автор
 - Казмірчук Вероніка Володимирівна
 - ЗІПЗ 23-1
