using ChatBotGui;
using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;

namespace ChatBotGui
{
    public partial class MainWindow : Window
    {
        private ChatBot botProcessor; 

        public MainWindow()
        {
            InitializeComponent();
            botProcessor = new ChatBot();

            PlayVoiceGreeting();
            LoadAsciiArt();

            
            tbChatDisplay.Text += botProcessor.GetGreeting() + "\n\n";
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(audioPath))
                {
                    SoundPlayer audioEngine = new SoundPlayer(audioPath);
                    audioEngine.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio Notice: " + ex.Message);
            }
        }

       
        private void LoadAsciiArt()
        {
           
            tbAsciiCanvas.Text =
                "          .----------.          \n" +
                "         /  STAY SAFE \\         \n" +
                "        |    ______    |        \n" +
                "        |   /      \\   |        \n" +
                "        |  |  [@@]  |  |        \n" +
                "        |   \\______/   |        \n" +
                "         \\            /         \n" +
                "          \\    __    /          \n" +
                "           \\  /  \\  /           \n" +
                "            \\/____\\/            \n" +
                " ==============================\n" +
                " [       CYBER AWARENESS           ]";
        }
        

        private void ExecuteCommunicationCycle()
        {
            string rawPrompt = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(rawPrompt)) return;

            tbChatDisplay.Text += $"You: {rawPrompt}\n";
            txtUserInput.Clear();

            
            string cleanResponse = botProcessor.ProcessInput(rawPrompt);

            
            tbChatDisplay.Text += $"{cleanResponse}\n\n";
            chatScroller.ScrollToBottom();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommunicationCycle();
        }

        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExecuteCommunicationCycle();
            }
        }
    }
}