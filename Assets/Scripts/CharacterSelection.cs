using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private int selectedCharacterIndex;
    private Color desiredColor;
    private AudioClip deathSound;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;
    public Scene scene;

    private void Start()
    {
        UpdateCharacterSelectionUI();
    }

    public void RightArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
        {
            selectedCharacterIndex = 0;
        }
        UpdateCharacterSelectionUI();
    }

    public void Confirm()
    {
        Debug.Log(string.Format("Character {0}: {1} has been chosen", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));

        string chosenCharacter = characterList[selectedCharacterIndex].characterName;
        switch (chosenCharacter)
        {
            case "Rosie":
                SceneManager.LoadScene(1);
                break;
            case "Josie":
                SceneManager.LoadScene(4);
                break;
            case "Kozie":
                SceneManager.LoadScene(7);
                break;
            case "Lozie":
                SceneManager.LoadScene(10);
                break;
            case "Nozie":
                SceneManager.LoadScene(13);
                break;


        }

    }


    private void UpdateCharacterSelectionUI()
    {
        characterSplash.sprite = characterList[selectedCharacterIndex].splash;
        characterName.text = characterList[selectedCharacterIndex].characterName;
        backgroundColor.color = characterList[selectedCharacterIndex].characterColor;
        desiredColor = characterList[selectedCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
    }

    public void resetScores()
    {
        PlayerPrefs.DeleteAll();
    }
}
