using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public enum KingIcon { TYPE1, TYPE2, TYPE3 }

public class CharacterCreator : MonoBehaviour
{

    [SerializeField]
    int remainingCharacterPoints = 10;

    //fuerza del ejército entiendo que será
    [SerializeField]
    int characterStrength;

    //conocimiento y sabiduría
    [SerializeField]
    int characterKnowledge;

    //su nivel de fe es de más de 8000!!!
    [SerializeField]
    int characterFaith;

    //podríamos dejar la coña de que la característica de suerte esté en 1 y no se pueda cambiar
    //así vamos dejando una pista al jugador de lo que se va a encontrar xD
    [SerializeField]
    int characterLuck = 0;

    public TMP_InputField inputStrength;
    public TMP_InputField inputKnowledge;
    public TMP_InputField inputFaith;
    public TMP_InputField inputLuck;
    public TMP_InputField inputName;
    List<TMP_InputField> modifiableInputFields;
    public TMP_Text remainingPointsText;
    public Image kingIcon;

    List<string> predefinedNames = new List<string> { "Darius III", "Sin-shar-ishkun", "Paco Porras", "Shakira", "Messi chikito",
        "Alexis Oberana", "Luffy", "Leticia Sabater", "Josefa Raona", "Don Omar"};

    // Start is called before the first frame update
    void Start()
    {
        inputStrength.onValueChanged.AddListener(delegate { inputStrength.text = ValueChangeCheck(inputStrength, new List<TMP_InputField>{ inputKnowledge, inputFaith }).ToString(); });
        inputKnowledge.onValueChanged.AddListener(delegate { inputKnowledge.text = ValueChangeCheck(inputKnowledge, new List<TMP_InputField> { inputStrength, inputFaith }).ToString(); });
        inputFaith.onValueChanged.AddListener(delegate { inputFaith.text = ValueChangeCheck(inputFaith, new List<TMP_InputField> { inputKnowledge, inputStrength }).ToString(); });
        remainingPointsText.text = remainingCharacterPoints.ToString();
        modifiableInputFields = new List<TMP_InputField> { inputStrength, inputKnowledge, inputFaith };
        Random.InitState((int)DateTime.Now.Ticks);
    }

    private int ValueChangeCheck(TMP_InputField inputField, List<TMP_InputField> remainingInputFields)
    {
        int intValue;
        if (!int.TryParse(inputField.text, out intValue))
            intValue = 0;
        int remainingValue = AllRemainingPoints(remainingInputFields);
        int totalValue = intValue + remainingValue;
        if (intValue < 0 || intValue > 10 || totalValue > 10 || totalValue < 0)
        {
            return 0;
        } else
        {
            remainingCharacterPoints = 10 - totalValue;
            remainingPointsText.text = remainingCharacterPoints.ToString();
            return intValue;
        }
    }

    private int AllRemainingPoints(List<TMP_InputField> remainingInputFields) 
    {
        int remainingValue = 0;
        remainingInputFields.ForEach(inputField =>
        {
            int intFieldValue = 0;
            if (!int.TryParse(inputField.text, out intFieldValue))
            {
                intFieldValue = 0;
            }

            remainingValue += intFieldValue;
        });

        return remainingValue;
    }

    public void AddValueStrength()
    {
        int intValue;
        if (!int.TryParse(inputStrength.text, out intValue))
            intValue = 0;
        inputStrength.text = AddValue(intValue);
    }

    public void SubstractValueStrength()
    {
        int intValue;
        if (!int.TryParse(inputStrength.text, out intValue))
            intValue = 0;
        inputStrength.text = SubstractValue(intValue);
    }

    public void AddValueKnowledge()
    {
        int intValue;
        if (!int.TryParse(inputKnowledge.text, out intValue))
            intValue = 0;
        inputKnowledge.text = AddValue(intValue);
    }

    public void SubstractValueKnowledge()
    {
        int intValue;
        if (!int.TryParse(inputKnowledge.text, out intValue))
            intValue = 0;
        inputKnowledge.text = SubstractValue(intValue);
    }

    public void AddValueFaith()
    {
        int intValue;
        if (!int.TryParse(inputFaith.text, out intValue))
            intValue = 0;
        inputFaith.text = AddValue(intValue);
    }

    public void ModifyValueLuck()
    {
        
    }

    public void SubstractValueFaith()
    {
        int intValue;
        if (!int.TryParse(inputFaith.text, out intValue))
            intValue = 0;
        inputFaith.text = SubstractValue(intValue);
    }

    public string AddValue(int inputValue)
    {
        if (remainingCharacterPoints > 0 && inputValue < 11)
        {
            remainingCharacterPoints--;
            remainingPointsText.text = remainingCharacterPoints.ToString();
            return (++inputValue).ToString();
        }
        else
        {
            return inputValue.ToString();
        }
    }

    public string SubstractValue(int inputValue)
    {
        if (inputValue > 0)
        {
            remainingCharacterPoints++;
            remainingPointsText.text = remainingCharacterPoints.ToString();
            return (--inputValue).ToString();
        }
        else
        {
            return inputValue.ToString();
        }
    }

    public void SetCharacterValues()
    {
        GameController controller = GameController.instance;
        controller.playerName = inputName.text;
        controller.playerStrength = int.Parse(inputStrength.text);
        controller.playerKnowledge = int.Parse(inputKnowledge.text);
        controller.playerFaith = int.Parse(inputFaith.text);
        controller.playerLuck = int.Parse(inputLuck.text);
        SceneManager.LoadScene("GameScene");

    }

    public void RandomizeValues() 
    {
        ResetValues();
        RandomizeName();
        RandomizeStats();
    }

    void RandomizeName()
    {
        inputName.text = predefinedNames[Random.Range(0, predefinedNames.Count)];
    }

    void RandomizeStats()
    {
        int characterPoints = 10;
        modifiableInputFields.ForEach(inputField =>
        {
            int intValue = Random.Range(0, characterPoints + 1);
            inputField.text = intValue.ToString();
            characterPoints = characterPoints - intValue;
        });
        if (characterPoints > 0)
        {
            inputFaith.text = (characterPoints + int.Parse(inputFaith.text)).ToString();
            remainingCharacterPoints = 0;
            remainingPointsText.text = remainingCharacterPoints.ToString();
        }
        if (Random.Range(1, 1001) == 1000)
        {
            inputLuck.text = "10";
            //GetComponent<AngryFaceController>().gameObject.GetComponent<Image>().enabled = true;
        }
        else
            inputLuck.text = "0";
    }

    void ResetValues() 
    {
        modifiableInputFields.ForEach(inputField =>
        {
            inputField.text = "0";
        });
    }
}
