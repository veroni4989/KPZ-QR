using QRCoder;
using System;
using System.Drawing;
using System.Windows.Forms;
using KPZ_QR.QR;
using KPZ_QR.Styles;

namespace KPZ_QR
{
    public partial class Form1 : Form
    {
        private TextBox inputTextBox;
        private Button generateButton;
        private PictureBox qrPictureBox;
        private IQrGenerator qrGenerator;
        private ComboBox generatorComboBox;
        private ComboBox styleComboBox;


        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            qrGenerator = QrGeneratorFactory.Create(QrGeneratorType.Png);
        }

        private void InitializeCustomComponents()
        {
            this.Text = "KPZ-QR Generator";
            this.Size = new Size(500, 500);

            inputTextBox = new TextBox
            {
                Location = new Point(20, 20),
                Width = 300,
                Height = 30
            };

            generateButton = new Button
            {
                Text = "Згенерувати QR",
                Location = new Point(20, 100),
                Width = 140,
                Height = 30
            };
            generateButton.Click += GenerateButton_Click;

            generatorComboBox = new ComboBox
            {
                Location = new Point(230, 60),
                Width = 90,
                Height = 30,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            generatorComboBox.Items.Add("PNG");
            generatorComboBox.Items.Add("SVG");
            generatorComboBox.Items.Add("ASCII");
            generatorComboBox.SelectedIndex = 0;
            generatorComboBox.SelectedIndexChanged += GeneratorComboBox_SelectedIndexChanged;

            qrPictureBox = new PictureBox
            {
                Location = new Point(20, 140),
                Size = new Size(300, 300),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            var saveButton = new Button
            {
                Text = "Зберегти QR",
                Location = new Point(170, 100),
                Width = 150,
                Height = 30
            };
            saveButton.Click += SaveButton_Click;

            var showHistoryButton = new Button
            {
                Text = "Показати історію",
                Location = new Point(340, 20),
                Width = 140,
                Height = 60
            };
            showHistoryButton.Click += ShowHistoryButton_Click;
           
            styleComboBox = new ComboBox
            {
                Location = new Point(20, 60),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            styleComboBox.Items.AddRange(new[] { "Default", "Blue", "Red" });
            styleComboBox.SelectedIndex = 0;

            this.Controls.Add(styleComboBox);
            this.Controls.Add(showHistoryButton);
            this.Controls.Add(saveButton);
            this.Controls.Add(inputTextBox);
            this.Controls.Add(generateButton);
            this.Controls.Add(qrPictureBox);
            this.Controls.Add(generatorComboBox);
        }

        private void GeneratorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (generatorComboBox.SelectedItem.ToString())
            {
                case "PNG":
                    qrGenerator = QrGeneratorFactory.Create(QrGeneratorType.Png);
                    break;
                case "SVG":
                    qrGenerator = QrGeneratorFactory.Create(QrGeneratorType.Svg);
                    break;
                case "ASCII":
                    qrGenerator = QrGeneratorFactory.Create(QrGeneratorType.Ascii);
                    break;
            }
        }


        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string text = inputTextBox.Text;
            string selectedGenerator = generatorComboBox.SelectedItem?.ToString();
            string selectedStyle = styleComboBox.SelectedItem?.ToString() ?? "Default";

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Введіть текст для генерації QR-коду.");
                return;
            }

            IQrStyle qrStyle = (selectedStyle) switch
            {
                "Blue" => new BlueStyle(),
                "Red" => new RedStyle(),
                _ => new DefaultStyle()
            };

            Bitmap qrCodeImage = qrGenerator.Generate(text, qrStyle);

            qrPictureBox.Image = qrCodeImage;
            HistoryManager.Instance.AddEntry(text, selectedGenerator);
        }


        private void SaveButton_Click(object? sender, EventArgs e)
        {
            if (qrPictureBox.Image == null)
            {
                MessageBox.Show("QR-код ще не згенеровано.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                Title = "Збереження QR-коду"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                qrPictureBox.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("QR-код збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowHistoryButton_Click(object sender, EventArgs e)
        {
            HistoryForm historyForm = new HistoryForm();
            historyForm.Show();
        }

    }
}
