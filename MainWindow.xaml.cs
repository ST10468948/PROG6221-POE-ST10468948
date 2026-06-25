using ChatBotGui;
using CybersecurityChatbot;
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

        // Connection to the quiz engine logic class
        private QuizManager quizEngine;

        public MainWindow()
        {
            // Set up the UI elements defined in XAML 
            InitializeComponent();

            // Create a new instance of our chatbot engine 
            botProcessor = new ChatBot();

            // Create a new instance of our task manager layer 
            taskEngine = new TaskManager();

            // Create a new instance of our quiz engine layer
            quizEngine = new QuizManager();

            // Run startup tasks: play audio and show the logo 
            PlayVoiceGreeting();
            LoadAsciiArt();

            // Sync the DataGrid view with our saved JSON tasks on application launch 
            RefreshTasksGrid();

            // Load the first quiz question onto the screen automatically on launch
            DisplayNextQuestion();

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

        // =========================================================================
        // CYBER QUIZ FRONTEND WIRING METHODS
        // =========================================================================

        // Pulls the current active question details from the quiz engine and displays them on screen
        private void DisplayNextQuestion()
        {
            // Uncheck all answer choice radio options to start fresh
            RadOptA.IsChecked = false;
            RadOptB.IsChecked = false;
            RadOptC.IsChecked = false;
            RadOptD.IsChecked = false;

            // Retrieve the active question data package from the engine card deck
            QuizQuestion activeQuestion = quizEngine.GetCurrentQuestion();

            // Assign the text values directly to our XAML text boxes and radio inputs
            TxtQuestionBox.Text = activeQuestion.QuestionText;
            RadOptA.Content = activeQuestion.Options[0];
            RadOptB.Content = activeQuestion.Options[1];

            // If the active card contains multiple choice items, keep them visible
            if (activeQuestion.Options.Count > 2)
            {
                RadOptC.Visibility = Visibility.Visible;
                RadOptD.Visibility = Visibility.Visible;
                RadOptC.Content = activeQuestion.Options[2];
                RadOptD.Content = activeQuestion.Options[3];
            }
            else
            {
                // If it is just a True/False question, hide the extra option boxes out of sight
                RadOptC.Visibility = Visibility.Collapsed;
                RadOptD.Visibility = Visibility.Collapsed;
            }

            // Toggle panels so the question is open and the old feedback is hidden
            PnlQuizActive.Visibility = Visibility.Visible;
            PnlFeedback.Visibility = Visibility.Collapsed;
        }

        // Action handler triggered when the student saves their answer selection choice
        private void BtnSubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            // Identify which specific letter selection has been highlighted by the cursor track
            string selectedLetter = "";
            if (RadOptA.IsChecked == true) selectedLetter = "A";
            else if (RadOptB.IsChecked == true) selectedLetter = "B";
            else if (RadOptC.IsChecked == true) selectedLetter = "C";
            else if (RadOptD.IsChecked == true) selectedLetter = "D";

            // Input Validation: Ensure the user picked an answer before continuing
            if (string.IsNullOrEmpty(selectedLetter))
            {
                MessageBox.Show("Please select an answer option choice before clicking submit.", "Selection Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // If this is the very first question being submitted, log that the quiz attempt has officially started
            if (quizEngine.GetScore() == 0 && quizEngine.IsFinished() == false)
            {
                ActivityLogger.Log("Quiz started");
            }

            // Hide the question options container pane to prevent accidental duplicate submission entries
            PnlQuizActive.Visibility = Visibility.Collapsed;

            // Pull active reference metrics and pass the answer check tracking command
            QuizQuestion answeredQuestion = quizEngine.GetCurrentQuestion();
            bool isCorrect = quizEngine.SubmitAnswer(selectedLetter);

            // Instantly update our live score counter widget on the dashboard
            LblLiveScore.Text = $"{quizEngine.GetScore()}/{quizEngine.GetTotalQuestions()}";

            // Format our visual alert borders and feedback text matching the success state
            if (isCorrect)
            {
                TxtFeedbackStatus.Text = "Correct!";
                TxtFeedbackStatus.Foreground = System.Windows.Media.Brushes.Green;
                BdrFeedbackStatus.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(240, 253, 244)); // Light Green Hex
                BdrFeedbackStatus.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            else
            {
                TxtFeedbackStatus.Text = $"Incorrect! (The right answer was: {answeredQuestion.CorrectAnswer})";
                TxtFeedbackStatus.Foreground = System.Windows.Media.Brushes.Red;
                BdrFeedbackStatus.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(254, 242, 242)); // Light Red Hex
                BdrFeedbackStatus.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            // Display the explanation text and open the feedback container visibility block
            TxtExplanation.Text = answeredQuestion.Explanation;
            PnlFeedback.Visibility = Visibility.Visible;
        }

        // Action handler triggered when advancing past feedback towards sequential cards
        private void BtnNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            // If we have just processed the final card, shut down core panels and show the results summary
            if (quizEngine.IsFinished())
            {
                PnlQuizActive.Visibility = Visibility.Collapsed;
                PnlFeedback.Visibility = Visibility.Collapsed;

                TxtFinalScoreReport.Text = $"You scored {quizEngine.GetScore()} out of {quizEngine.GetTotalQuestions()} questions correctly.";
                TxtFinalFeedbackMsg.Text = quizEngine.GetFinalMessage();
                PnlQuizResults.Visibility = Visibility.Visible;

                // Add an entry to our activity log history when the quiz is fully completed
                ActivityLogger.Log($"Quiz completed - score: {quizEngine.GetScore()} out of {quizEngine.GetTotalQuestions()}");
            }
            else
            {
                // Otherwise, keep the cycle rolling forward cleanly
                DisplayNextQuestion();
            }
        }

        // Action handler triggered when resetting score structures to retry the game loop from scratch
        private void BtnRestartQuiz_Click(object sender, RoutedEventArgs e)
        {
            // Wipe internal metrics counters completely clean
            quizEngine.ResetQuiz();

            // Sync dashboard summary data parameters
            LblLiveScore.Text = $"0/{quizEngine.GetTotalQuestions()}";
            PnlQuizResults.Visibility = Visibility.Collapsed;

            // Add an entry to our activity log history when the quiz is restarted
            ActivityLogger.Log("Quiz restarted");

            // Redraw question tracking interfaces securely
            DisplayNextQuestion();
        }
    }
}