using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    // This class is a simple model that holds data for a single quiz question
    public class QuizQuestion
    {
        // The text of the question itself
        public string QuestionText { get; set; }

        // A list of multiple-choice answers (e.g., A, B, C, D)
        public List<string> Options { get; set; }

        // The correct letter or text string (e.g., "B")
        public string CorrectAnswer { get; set; }

        // The educational explanation shown to the student after they answer
        public string Explanation { get; set; }
    }
}