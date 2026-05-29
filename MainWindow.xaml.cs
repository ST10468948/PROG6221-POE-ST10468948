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
        // Connection to the main chatbot logic class [cite: 250]
        private ChatBot botProcessor;

        public MainWindow()
        {
            // Set up the UI elements defined in XAML [cite: 170]
            InitializeComponent();

            // Create a new instance of our chatbot engine [cite: 251]
            botProcessor = new ChatBot();

            // Run startup tasks: play audio and show the logo [cite: 171, 172]
            PlayVoiceGreeting();
            LoadAsciiArt();

            // Show the bot's first message (asking for name) in the chat box [cite: 173]
            tbChatDisplay.Text += botProcessor.GetGreeting() + "\n\n";
        }

        // Method to play the welcome sound file [cite: 171]
        private void PlayVoiceGreeting()
        {
            try
            {
                // Find the audio file in the folder where the app is running [cite: 145]
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sage.wav");

                // If the file exists, play it using the system player [cite: 147]
                if (File.Exists(audioPath))
                {
                    SoundPlayer audioEngine = new SoundPlayer(audioPath);
                    audioEngine.Play();
                }
            }
            catch (Exception ex)
            {
                // If the sound fails, show an error message instead of crashing
                MessageBox.Show("Audio Notice: " + ex.Message);
            }
        }

        // Displays the shield logo in the header area [cite: 172]
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
                " [       CYBER AWARENESS      ]";
        }

       // The main loop that handles sending and receiving messages [cite: 254]
        private void ExecuteCommunicationCycle()
        {
            // Get text from input box and remove extra spaces [cite: 254]
            string rawPrompt = txtUserInput.Text.Trim();

            // Don't do anything if the box is empty (Input Validation) [cite: 280]
            if (string.IsNullOrEmpty(rawPrompt)) return;

            // Show what the user typed in the chat window
            tbChatDisplay.Text += $"You: {rawPrompt}\n";

            // Clear the input box for the next message
            txtUserInput.Clear();

            // Send the message to the ChatBot class to get a response [cite: 226, 254]
            string cleanResponse = botProcessor.ProcessInput(rawPrompt);

            // Show the bot's response and scroll to the newest message [cite: 255, 256]
            tbChatDisplay.Text += $"{cleanResponse}\n\n";
            chatScroller.ScrollToBottom();
        }

        // Runs the message cycle when the SEND button is clicked [cite: 252]
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommunicationCycle();
        }

        // Allows the user to press 'Enter' on the keyboard to send a message [cite: 165, 253]
        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExecuteCommunicationCycle();
            }
        }
    }
}