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
    public Image scoreImage; // Reference to the UI Image component
    public Sprite[] scoreSprites; // Array of sprites for different score ranges

    private string[] predefinedAnswers = { "Never", "Rarely", "Sometimes", "Often", "Always" };
    private int[] answerWeights = { 4, 3, 2, 1, 0 };

    void Start()
    {
        DisplayQuestion();
        UpdateScoreImage();
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
        UpdateScoreImage();
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

    void UpdateScoreImage()
    {
        if (totalScore < 4)
        {
            scoreImage.sprite = scoreSprites[0]; // Display the first image
        }
        else if (totalScore < 8)
        {
            scoreImage.sprite = scoreSprites[1]; // Display the second image
        }
        else if (totalScore < 12)
        {
            scoreImage.sprite = scoreSprites[2]; // Display the third image
        }
        else if (totalScore < 16)
        {
            scoreImage.sprite = scoreSprites[3]; // Display the third image
        }
        else if (totalScore < 20)
        {
            scoreImage.sprite = scoreSprites[4]; // Display the third image
        }
        else
        {
            scoreImage.sprite = scoreSprites[5]; // Display the fourth image or more
        }
    }
}