using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Popup GameObjects")]
    [SerializeField] private GameObject gameDescriptionGameObject;

    [Header("Descriptions")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI gameDescriptionText;

    private void Start()
    {
        gameDescriptionGameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            DisplayPopup(gameDescriptionGameObject, gameDescriptionText);
    }

    private void DisplayText(GameObject popup, string text)
    {
        if (popup != null)
            popup.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void DisplayPopup(GameObject popup, TextMeshProUGUI popupText)
    {
        popup.SetActive(true);
        DisplayText(popup, popupText.text);
    }

    public void ClosePopup(GameObject popup) => popup.SetActive(false);
}