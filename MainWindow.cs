using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace MatheQuiz
{
   
    public partial class MainWindow : Window
    {
        // Init variables
        private int userAnswer;
        private int currentQuestion;
        private int numberOne, numberTwo;
        private int correctAnswer;
        private int totalCorrect;
        private int totalWrong;
        private int highNumber;

        Random rnd = new();

        public int UserAnswer { get => userAnswer; set => userAnswer = value; }
        public int CurrentQuestion { get => currentQuestion; set => currentQuestion = value; }
        public int NumberOne { get => numberOne; set => numberOne = value; }
        public int NumberTwo { get => numberTwo; set => numberTwo = value; }
        public int ChooseOperator { get; set; }
        public int CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
        public int TotalCorrect { get => totalCorrect; set => totalCorrect = value; }
        public int TotalWrong { get => totalWrong; set => totalWrong = value; }
        public int MaxQuestions { get; set; }
        public int HighNumber { get => highNumber; set => highNumber = value; }
        public Random Rnd { get => rnd; set => rnd = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get difficulty level from radio buttons
            if (rbEasy.IsChecked.Value)
            {
                HighNumber = 10;
            }
            if (rbMedium.IsChecked.Value)
            {
                HighNumber = 25;
            }
            if (rbHard.IsChecked.Value)
            {
                HighNumber = 50;
            }
            // Get number of questions from drop down list 
            MaxQuestions = Convert.ToInt32(comboBox1.Text);
            RevealQuiz();
            AskQuestion();
            HideChoices();
        }
        private void AskQuestion()
        {
            NumberOne = Rnd.Next(1, HighNumber);
            NumberTwo = Rnd.Next(1, HighNumber);
            ChooseOperator = Rnd.Next(1, 1000);
            _ = Keyboard.Focus(tbAnswer);
            lbQuizStatus.Content = $"Question {Convert.ToString(CurrentQuestion)} of {Convert.ToString(MaxQuestions)}";
            if (ChooseOperator % 2 == 0) // Even, +
            {
                lblQuestion.Content = $"{Convert.ToString(NumberOne)} + {Convert.ToString(NumberTwo)}";
                CorrectAnswer = Addition(NumberOne, NumberTwo);
            }
            else // Odd, *
            {
                lblQuestion.Content = $"{Convert.ToString(NumberOne)} * {Convert.ToString(NumberTwo)}";
                CorrectAnswer = Multiplication(NumberOne, NumberTwo);
            }
        }
        private int Addition(int num1, int num2)
        {
            int answer = num1 + num2;
            return answer;
        }
        private int Multiplication(int num1, int num2)
        {
            int answer = num1 * num2;
            return answer;
        }
        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (tbAnswer.Text != "")
            {
            }
            else
            {
                _ = MessageBox.Show("You must enter a number.");
                tbAnswer.Text = "0";
            }
            //check answer and ask next question if needed
            UserAnswer = Convert.ToInt32(tbAnswer.Text);
            //int.TryParse(tbAnswer.Text, out userAnswer);

            if (UserAnswer == CorrectAnswer)
            {
                TotalCorrect += 1;
                lblNumberCorrect.Content = $"Correct {Convert.ToString(TotalCorrect)}";
                if (CurrentQuestion < MaxQuestions)
                {
                    CurrentQuestion =+ 1;
                    tbAnswer.Text = "";
                    AskQuestion();

                }
                else
                {
                    gameOver();
                }
            }
            else
            {
                TotalWrong += 1;
                lblNumberWrong.Content = $"Wrong {Convert.ToString(TotalWrong)}";
                if (CurrentQuestion < MaxQuestions)
                {
                    CurrentQuestion += 1;
                    tbAnswer.Text = "";
                    AskQuestion();
                }
                else
                {
                    gameOver();
                }
            }
        }
        private void gameOver()
        {
            _ = MessageBox.Show("Game Over!");
            btnPlayAgain.Visibility = Visibility.Visible;
        }
        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
        private void ResetGame()
        {
            ShowChoices();
            UserAnswer = 1;
            CurrentQuestion = 1;
            NumberOne = 0;
            NumberTwo = 0;
            ChooseOperator = 0;
            CorrectAnswer = 1;
            TotalCorrect = 0;
            TotalWrong = 0;
            MaxQuestions = 0;
            HighNumber = 0;
            NewMethod();
            if (!rbEasy.IsChecked.Value)
            {
            }
            else
            {
                HighNumber = 10;
            }
            if (!rbMedium.IsChecked.Value)
            {
            }
            else
            {
                HighNumber = 25;
            }
            if (!rbHard.IsChecked.Value)
            {
            }
            else
            {
                HighNumber = 50;
            }
            // Get number of questions from drop down list
            MaxQuestions = Convert.ToInt32(comboBox1.Text);
            tbAnswer.Text = "";
            lblNumberCorrect.Content = "Correct 0";
            lblNumberWrong.Content = "Wrong 0";
        }
        private void NewMethod()
        {
            lblQuestion.Visibility = Visibility.Hidden;
            lbQuizStatus.Visibility = Visibility.Hidden;
            lblNumberCorrect.Visibility = Visibility.Hidden;
            lblNumberWrong.Visibility = Visibility.Hidden;
            tbAnswer.Visibility = Visibility.Hidden;
            btnAnswer.Visibility = Visibility.Hidden;
            btnPlayAgain.Visibility = Visibility.Hidden;
            Random rnd = new();
            // Get difficulty level from radio buttons
        }
        private void ShowChoices()
        {
            groupBox.Visibility = Visibility.Visible;
            rbEasy.Visibility = Visibility.Visible;
            rbMedium.Visibility = Visibility.Visible;
            rbHard.Visibility = Visibility.Visible;
            comboBox1.Visibility = Visibility.Visible;
            btnStart.Visibility = Visibility.Visible;
            lblNumberOfQuestions.Visibility = Visibility.Visible;
        }
        private void HideChoices()
        {
            groupBox.Visibility = Visibility.Hidden;
            rbEasy.Visibility = Visibility.Hidden;
            rbMedium.Visibility = Visibility.Hidden;
            rbHard.Visibility = Visibility.Hidden;
            comboBox1.Visibility = Visibility.Hidden;
            btnStart.Visibility = Visibility.Hidden;
            lblNumberOfQuestions.Visibility = Visibility.Hidden;
        }
        private void RevealQuiz()
        {
            lblQuestion.Visibility = Visibility.Visible;
            lbQuizStatus.Visibility = Visibility.Visible;
            lblNumberCorrect.Visibility = Visibility.Visible;
            lblNumberWrong.Visibility = Visibility.Visible;
            tbAnswer.Visibility = Visibility.Visible;
            btnAnswer.Visibility = Visibility.Visible;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
 }