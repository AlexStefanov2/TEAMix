using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemistryController : MonoBehaviour
{
    public Image background;
    public Text formulaField;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;


    struct Quiz
    {
        public string formula;
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public int correctAnswer;

        public Quiz(string formula, string answer1, string answer2, string answer3, string answer4, int correctAnswer)
        {
            this.formula = formula;
            this.answer1 = answer1;
            this.answer2 = answer2;
            this.answer3 = answer3;
            this.answer4 = answer4;
            this.correctAnswer = correctAnswer;
        }
    }
    static Quiz[] database = {
        new Quiz("__ + O₂ = 2H₂O", "H₂", "2H", "2H₂", "4H", 3),
        new Quiz("2Na + Cl₂ = __", "2NaCl", "NaCl", "Na₂Cl₂", "2Na₂Cl", 1),
        new Quiz("2Na + 2H₂O = __ + H₂", "NaOH", "2NaOH", "2NaO", "H₂O", 2),
        new Quiz("__ + O₂ = 2Na₂O", "Na", "2Na", "3Na", "4Na", 4),
        new Quiz("H₂ + F₂ = __", "HF", "2HF", "H₂F₂", "F₂H₂", 2),
        new Quiz("H₂ + O₂ = __", "2HO", "H₂O", "H₂O₂", "HO", 3),
    };
    

    void Update()
    {
        if (isShown) {
            ShowTask();
        } else {
            HideTask();
        }
    }

    public void HideTask()
    {
        background.enabled = false;
        SetButtonsVisibility(false);
        formulaField.text = "";
    }

    public void SetButtonsVisibility(bool active)
    {
        button1.gameObject.SetActive(active);
        button2.gameObject.SetActive(active);
        button3.gameObject.SetActive(active);
        button4.gameObject.SetActive(active);
    }

    public void ShowTask()
    {
        background.enabled = true;
        SetButtonsVisibility(true);
        formulaField.text = chosenQuiz.formula;
        text1.text = chosenQuiz.answer1;
        text2.text = chosenQuiz.answer2;
        text3.text = chosenQuiz.answer3;
        text4.text = chosenQuiz.answer4;
    }

    public void ChooseAnswer(int answerNumber)
    {
        SetButtonsVisibility(false);
        if (answerNumber != chosenQuiz.correctAnswer) {
            formulaField.color = new Color(1f, 0f, 0f, 1f);
            if (varIndex == 0) { // attack
                PlayerController.playerAttack /= 2;
            } else if (varIndex == 1) { // defence
                PlayerController.playerDefence /= 2;
            }
        } else {
            formulaField.color = new Color(0f, 1f, 0f, 1f);
        }
        isDone = true;
    }

    public static bool isShown = false;
    public static bool isDone = false;
    static Quiz chosenQuiz;
    static int varIndex;
    public static void GiveTask(int var)
    {
        varIndex = var;
        isShown = true;
        chosenQuiz = database[Random.Range(0, database.Length-1)];
        isDone = false;
        while (!isDone) {}
        WaitForSeconds(1);
    }
}
