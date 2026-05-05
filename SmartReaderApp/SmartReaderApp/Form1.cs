using System;
using System.Windows.Forms;
using Azure;
using Azure.AI.TextAnalytics;
using System.Threading.Tasks;

namespace SmartReaderApp
{
    public partial class Form1 : Form
    {
        // Встав сюди свої ключі з порталу Azure
        private readonly string endpoint = "https://labwork4.cognitiveservices.azure.com/";
        private readonly string apiKey = "ADCcdDRiaBuGRNvi26qo3kH671HqzG1UoWiQ0WL2Ot12WYdAVRZoJQQJ99CEACi5YpzXJ3w3AAAaACOGquUR";
        private TextAnalyticsClient client;

        public Form1()
        {
            InitializeComponent();

            client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridViewResults.Columns.Clear();
            dataGridViewResults.Columns.Add("Entity", "Термін / Сутність");
            dataGridViewResults.Columns.Add("Category", "Джерело");
            dataGridViewResults.Columns.Add("URL", "Посилання на статтю");

            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void buttonAnalyze_Click(object sender, EventArgs e)
        {
            string text = textBoxInput.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Будь ласка, введіть текст конспекту для аналізу.");
                return;
            }

            buttonAnalyze.Enabled = false;
            buttonAnalyze.Text = "Шукаю терміни...";
            dataGridViewResults.Rows.Clear();

            try
            {
                var response = await client.RecognizeLinkedEntitiesAsync(text);

                foreach (LinkedEntity entity in response.Value)
                {
                    dataGridViewResults.Rows.Add(entity.Name, entity.DataSource, entity.Url);
                }

                if (dataGridViewResults.Rows.Count == 0)
                {
                    MessageBox.Show("Цікавих термінів не знайдено. Спробуйте інший текст.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка з'єднання: {ex.Message}");
            }
            finally
            {
                buttonAnalyze.Enabled = true;
                buttonAnalyze.Text = "Проаналізувати терміни";
            }
        }
    }
}