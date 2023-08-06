using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Popup GameObjects")]
    [SerializeField] private GameObject gameDescriptionGameObject;
    [SerializeField] private GameObject controlsDisplayGameObject;

    [Header("Descriptions")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI gameDescriptionText;
    [SerializeField] private TextMeshProUGUI controlsDescriptionText;

    private CanvasGroup controlsCanvasGroup;
    private bool hasPopupBeenDisplayed;

    private void Start()
    {
        gameDescriptionGameObject.SetActive(false);
        controlsDisplayGameObject.SetActive(false);

        controlsCanvasGroup = controlsDisplayGameObject.GetComponent<CanvasGroup>();
    }

    //change to events later on
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            DisplayPopup(gameDescriptionGameObject, gameDescriptionText);

        if (Input.GetKeyDown(KeyCode.L))
            DisplayPopup(controlsDisplayGameObject, controlsDescriptionText);

        if (!hasPopupBeenDisplayed && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            StartCoroutine(FadeOutPopup(controlsCanvasGroup));
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

    private IEnumerator FadeOutPopup(CanvasGroup canvasGroup)
    {
        hasPopupBeenDisplayed = true;

        yield return new WaitForSeconds(2f);

        canvasGroup.alpha = 1f;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
