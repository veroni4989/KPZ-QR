using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace KPZ_QR
{
    public class HistoryForm : Form
    {
        private ListView historyListView;

        public HistoryForm()
        {
            this.Text = "Історія генерацій";
            this.Size = new Size(600, 500);
            InitializeComponents();
            LoadHistory();
        }

        private void InitializeComponents()
        {
            historyListView = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Size = new Size(560, 420),
                Location = new Point(10, 10)
            };

            historyListView.Columns.Add("Час", 150);
            historyListView.Columns.Add("Текст", 250);
            historyListView.Columns.Add("Генератор", 150);

            this.Controls.Add(historyListView);
        }

        private void LoadHistory()
        {
            string dbPath = "Data Source=history.db;";
            using var connection = new SQLiteConnection(dbPath);
            connection.Open();

            var command = new SQLiteCommand("SELECT CreatedAt, Text, Generator FROM History ORDER BY CreatedAt DESC", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string timestamp = reader.IsDBNull(0) ? "" : reader.GetString(0);
                string text = reader.GetString(1);
                string generator = reader.GetString(2);

                var item = new ListViewItem(new[] { timestamp, text, generator });
                historyListView.Items.Add(item);
            }
        }

    }
}
