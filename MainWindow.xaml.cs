using ChatBotGui;
using CybersecurityChatbot;
using System;
using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;


namespace ChatBotGui
{
    public partial class MainWindow : Window
    {
        // Connection to the main chatbot logic class 
        private ChatBot botProcessor;

        // Connection to the task manager logic class 
        private TaskManager taskEngine;

        public MainWindow()
        {
            // Set up the UI elements defined in XAML 
            InitializeComponent();

            // Create a new instance of our chatbot engine 
            botProcessor = new ChatBot();

            // Create a new instance of our task manager layer 
            taskEngine = new TaskManager();

            // Run startup tasks: play audio and show the logo 
            PlayVoiceGreeting();
            LoadAsciiArt();

            // Sync the DataGrid view with our saved JSON tasks on application launch 
            RefreshTasksGrid();

            // Show the bot's first message (asking for name) in the chat box 
            tbChatDisplay.Text += botProcessor.GetGreeting() + "\n\n";
        }

        // Method to play the welcome sound file 
        private void PlayVoiceGreeting()
        {
            try
            {
                // Find the audio file in the folder where the app is running 
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sage.wav");

                // If the file exists, play it using the system player 
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

        // Displays the shield logo in the header area 
        private void LoadAsciiArt()
        {
            tbAsciiCanvas.Text =
                "          .----------.          \n" +
                "         /  STAY SAFE \\          \n" +
                "        |    ______    |        \n" +
                "        |   /      \\   |        \n" +
                "        |  |  [@@]  |  |        \n" +
                "        |   \\______/   |        \n" +
                "         \\            /         \n" +
                "          \\    __    /          \n" +
                "           \\  /  \\  /           \n" +
                "            \\/____\\/            \n" +
                " ==============================\n" +
                " [        CYBER AWARENESS      ]";
        }

        // The main loop that handles sending and receiving messages 
        private void ExecuteCommunicationCycle()
        {
            // Get text from input box and remove extra spaces 
            string rawPrompt = txtUserInput.Text.Trim();

            // Don't do anything if the box is empty (Input Validation) 
            if (string.IsNullOrEmpty(rawPrompt)) return;

            // Show what the user typed in the chat window
            tbChatDisplay.Text += $"You: {rawPrompt}\n";

            // Clear the input box for the next message
            txtUserInput.Clear();

            // Send the message to the ChatBot class to get a response 
            string cleanResponse = botProcessor.ProcessInput(rawPrompt);

            // Show the bot's response and scroll to the newest message 
            tbChatDisplay.Text += $"{cleanResponse}\n\n";
            chatScroller.ScrollToBottom();
        }

        // Runs the message cycle when the SEND button is clicked 
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommunicationCycle();
        }

       // Allows the user to press 'Enter' on the keyboard to send a message 
        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExecuteCommunicationCycle();
            }
        }         

        
        // Reads the updated list from the manager and binds it to our UI display grid 
                    
        private void RefreshTasksGrid()
        {
            // Break old references to ensure an absolute clean UI redraw map layer
            DgdTasks.ItemsSource = null;

            // Ask the business manager engine to supply the latest list records 
            DgdTasks.ItemsSource = taskEngine.GetAllTasks();
        }

        
        // Action handler triggered when the user clicks the "Add Task" button 
                    
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            // Gather structural literal inputs from the user entry components 
            string title = TxtTaskTitle.Text.Trim();
            string desc = TxtTaskDesc.Text.Trim();
            string reminder = TxtTaskReminder.Text.Trim();

            // Simple validation check: ensure core identity fields are populated safely
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(desc))
            {
                MessageBox.Show("Please fill out both the Task Title and Description fields.", "Data Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Route the data payload through our management logic controller 
            string successPrompt = taskEngine.AddTask(title, desc, reminder);

            // Wipe input boxes completely clean for subsequent user inputs
            TxtTaskTitle.Clear();
            TxtTaskDesc.Clear();
            TxtTaskReminder.Clear();

            // Force the DataGrid view interface component to fetch latest entries 
            RefreshTasksGrid();

            // Render an informative confirmation message layout alert window
            MessageBox.Show(successPrompt, "Operation Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
        // Action handler triggered when a task row is selected and completed 
                    
        private void BtnMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            // Map the highlighted grid collection line onto a CyberTask layout model context 
            CyberTask selectedTask = DgdTasks.SelectedItem as CyberTask;

            if (selectedTask != null)
            {
                // Run the completed flag rewrite statement processing rule 
                taskEngine.MarkAsComplete(selectedTask.Id);

                // Refresh the visible display grid layout table view immediately 
                RefreshTasksGrid();
            }
            else
            {
                MessageBox.Show("Please click on a specific task record row within the list view first.", "No Item Highlighted", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Action handler triggered when a task row is selected and deleted completely
                    
        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Cast the highlighted selection reference onto our entity model object 
            CyberTask selectedTask = DgdTasks.SelectedItem as CyberTask;

            if (selectedTask != null)
            {
                // Trigger the direct file extraction statement sequence link 
                taskEngine.DeleteTask(selectedTask.Id);

                // Re-read storage logs and adjust the visual components dynamically 
                RefreshTasksGrid();
            }
            else
            {
                MessageBox.Show("Please click on a specific task record row within the list view first.", "No Item Highlighted", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}