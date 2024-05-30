using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
    }

    public Question[] questions;
    private int currentQuestionIndex = 0;
    private int totalScore = 0;

    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI resultText;

    private string[] predefinedAnswers = { "Never", "Rarely", "Sometimes", "Often", "Always" };
    private int[] answerWeights = { 4, 3, 2, 1, 0 };

    void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = predefinedAnswers[i];
                int weight = answerWeights[i];  // Capture the weight for the button
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => SelectAnswer(weight));
            }
        }
        else
        {
            EndQuiz();
        }
    }

    void SelectAnswer(int weight)
    {
        totalScore += weight;
        currentQuestionIndex++;
        DisplayQuestion();
    }

    void EndQuiz()
    {
        questionText.text = "Quiz Over!";
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        resultText.text = "Your total score: " + totalScore.ToString();
        resultText.gameObject.SetActive(true);
    }
}
