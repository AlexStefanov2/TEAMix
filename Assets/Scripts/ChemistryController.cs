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


    public static int multiplier = 0;

    public struct Quiz
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
    /*
    EASY

    new Quiz("__ + O₂ -> 2H₂O", "H₂", "2H", "2H₂", "4H", 3),
    new Quiz("2Na + Cl₂ -> __", "2NaCl", "NaCl", "Na₂Cl₂", "2Na₂Cl", 1),
    new Quiz("2Na + 2H₂O -> __ + H₂", "NaOH", "2NaOH", "2NaO", "H₂O", 2),
    new Quiz("__ + O₂ -> 2Na₂O", "Na", "2Na", "3Na", "4Na", 4),
    new Quiz("H₂ + F₂ -> __", "HF", "2HF", "H₂F₂", "F₂H₂", 2),
    new Quiz("H₂ + O₂ -> __", "2HO", "H₂O", "H₂O₂", "HO", 3),
    new Quiz("N₂ + 3H₂ -> __", "6H + 2N", "2NH₃", "H₂N₂", "6HN", 2),

    HARD

    new Quiz("2H₂S + SO₂ -> 3S+ __", "2HO", "2H₂O", "H₂O₂", "4H + 2O", 2),
    new Quiz("CH₃COOC₂H₅ + 3H₂ -> CH₃COO + H+ __", "6H + 2N + 3O", "2NH₃", "C₂H₅OH", "6HCOO", 3),
    new Quiz("2Mg + O₂ -> __", "2MG + 2O", "MgO", "Mg₂O₂", "2MgO", 4),
    new Quiz("H₂SO₄ -> 2H + __", "SO₄", "S + O₄", "S2O₂", "2SO", 1),
    new Quiz("NaCl + AgNO₃ -> NaNO₃ + __", "Ag", "Ag + Cl", "AgCl", "Cl", 3),
    new Quiz("Ca(OH)₃ -> Al + __", "3OH", "3(OH)", "3O + 3H", "COH", 2),
    new Quiz("NaCl + AgNO₃ -> NaNO₃ + __", "Cl", "Ag", "Ag + Cl", "AgCl", 4),
    */

    /*
    static Quiz[] databaseEasy = {
        new Quiz("__ + O₂ -> 2H₂O", "H₂", "2H", "2H₂", "4H", 3),
        new Quiz("2Na + Cl₂ -> __", "2NaCl", "NaCl", "Na₂Cl₂", "2Na₂Cl", 1),
        new Quiz("2Na + 2H₂O -> __ + H₂", "NaOH", "2NaOH", "2NaO", "H₂O", 2),
        new Quiz("__ + O₂ -> 2Na₂O", "Na", "2Na", "3Na", "4Na", 4),
        new Quiz("H₂ + F₂ -> __", "HF", "2HF", "H₂F₂", "F₂H₂", 2),
        new Quiz("H₂ + O₂ -> __", "2HO", "H₂O", "H₂O₂", "HO", 3),
        new Quiz("N₂ + 3H₂ -> __", "6H + 2N", "2NH₃", "H₂N₂", "6HN", 2),
    };
    static Quiz[] databaseHard = {
        new Quiz("2H₂S + SO₂ -> 3S+ __", "2HO", "2H₂O", "H₂O₂", "4H + 2O", 2),
        new Quiz("CH₃COOC₂H₅ + 3H₂ -> CH₃COO + H+ __", "6H + 2N + 3O", "2NH₃", "C₂H₅OH", "6HCOO", 3),
        new Quiz("2Mg + O₂ -> __", "2MG + 2O", "MgO", "Mg₂O₂", "2MgO", 4),
        new Quiz("H₂SO₄ -> 2H + __", "SO₄", "S + O₄", "S2O₂", "2SO", 1),
        new Quiz("NaCl + AgNO₃ -> NaNO₃ + __", "Ag", "Ag + Cl", "AgCl", "Cl", 3),
        new Quiz("Al(OH)₃ -> Al + __", "3OH", "3(OH)", "3O + 3H", "COH", 2),
        new Quiz("NaCl + AgNO₃ -> NaNO₃ + __", "Cl", "Ag", "Ag + Cl", "AgCl", 4),
    };
    */

    public static int[] possibleElements = new int[] {11, 26, 27, 56};
    public static Quiz[][] reactionDatabase = new Quiz[119][];
    void Start()
    {
        reactionDatabase[11] = new Quiz[] {
            new Quiz("2Na + O₂ → 2___O₂", "Na₂", "Na₄", "CO₃", "O", 1), 
            new Quiz("2Na + 2____ → 2NaOH + H", "H₂O", "H", "Na", "OH", 1), 
            new Quiz("2Na + Cl₂ → _____", "NaOH", "2NaCl", "NaO₂", "Cl₂", 2), 
            new Quiz("2Na + ____ → NaBr", "2Br", "BrO₂", "Br₂", "AlBr₃", 2)
        };
        reactionDatabase[26] = new Quiz[4] {
            new Quiz("4Fe + ____ → 2Fe₂O₃", "O₆", "2O₃", "O₂", "3O₂", 4),
            new Quiz("____ + 3F₂ → 2FeF₃", "Fe", "Fe₂", "2Fe", "F₃", 3), 
            new Quiz("2Fe + 3Cl2 → ____", "2FeCl₃", "FeCl", "2FeCl", "6FeCl", 1), 
            new Quiz("2Fe + ____ → 2FeBr3 ", "Br₂", "FeBr", "3Br₂", "2FeBr", 3)
        };
        reactionDatabase[27] = new Quiz[4] {
            new Quiz("3Co + 4O₂ → ____", "Co₂O₂", "CoO₂", "Co₂ + O₂", "2Co₂O₂", 4), 
            new Quiz("2Co + ____ → 2CoO", "O", "H₂O", "O₂", "2O", 3), 
            new Quiz("____ + Br₂ → CoBr₂", "2Co", "CoBr", "Co₂", "Co", 4), 
            new Quiz("Co + ____ → CoCl₂", "Cl", "HCl", "NaCl", "Cl₂", 4)
        };
        reactionDatabase[56] = new Quiz[4] {
            new Quiz("2Ba + O₂ → ____", "Ba₂O₂", "Ba₂ + O₂", "BaO", "2BaO", 4), 
            new Quiz("Ba + O₂ → ____", "BaO₂", "2BaO", "BaO", "Ba₂O₂", 1), 
            new Quiz("____ + N₂ → Ba3N₂", "Ba", "Ba₃", "3Ba", "3Ba₂", 3), 
            new Quiz("Ba + 2H2O → ____", "2BaH₂O", "BaOH", "Ba(OH)₂ + H₂", "BaOH + 2H₂", 3)
        };
    }

    bool lastShownStatus = !isShown;
    void Update()
    {
        if (isShown != lastShownStatus) {
            if (isShown) {
                ShowTask();
            } else {
                HideTask();
            }
        }
        lastShownStatus = isShown;
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
            multiplier = 0;
        } else {
            formulaField.color = new Color(0f, 1f, 0f, 1f);
            multiplier += 1;
        }
        StartCoroutine(WaitAndHide());

    }
    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(1f);
        isShown = false;
        formulaField.color = new Color(0f, 0f, 0f, 1f);
        TurnController.stage++;
        TurnController.CycleTurns();
    }

    //EnemyUnit: Random mouseElement = new Random();
    /*
     Element 1 - Na (Sodium)
     Element 2 - Fe (Iron)
     Element 3 - Co (Cobalt)
     Element 4 - Ba (Barium)
     Element 5 - Ca (Calcium)
     Element 6 - Li (Lithium)
     
    public void ArrayFiller(int[] theArray)
    {
        do
        {
            theArray[element_count-1] = element_count;
            element_count++;
        } while (element_count < 5);
        element_count = 1;
    }*/
    public static bool isShown = false;
    static Quiz chosenQuiz;
    static int varIndex;
    public static int currentElement = 11;
    public static void GiveTask(int var)
    {
        varIndex = var;
        isShown = true;

        Quiz[] chosenArray = reactionDatabase[currentElement];
        chosenQuiz = chosenArray[Random.Range(0, chosenArray.Length-1)];
    }

    public static void DebugPrint()
    {
        Debug.Log("Current element: " + currentElement.ToString());
    }
}

