using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Popup GameObjects")]
    [SerializeField] private GameObject gameDescriptionGameObject;
    [SerializeField] private GameObject drivingControlsDisplayGameObject;
    [SerializeField] private GameObject serveControlsDisplayGameObject;

    [Header("Descriptions")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI gameDescriptionText;
    [SerializeField] private TextMeshProUGUI drivingControlsDescriptionText;
    [SerializeField] private TextMeshProUGUI serveControlsDescriptionText;

    private CanvasGroup drivingControlsCanvasGroup;
    private CanvasGroup serveControlsCanvasGroup;

    private bool hasDrivingControlPopupBeenDisplayed;
    private bool hasServeControlPopupBeenDisplayed;

    private void Start()
    {
        gameDescriptionGameObject.SetActive(false);
        drivingControlsDisplayGameObject.SetActive(false);
        serveControlsDisplayGameObject.SetActive(false);

        drivingControlsCanvasGroup = drivingControlsDisplayGameObject.GetComponent<CanvasGroup>();
        serveControlsCanvasGroup = serveControlsDisplayGameObject.GetComponent<CanvasGroup>();
    }

    //change to events later on
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            DisplayPopup(gameDescriptionGameObject, gameDescriptionText);

        if (Input.GetKeyDown(KeyCode.L) && !hasDrivingControlPopupBeenDisplayed)
        {
            DisplayPopup(drivingControlsDisplayGameObject, drivingControlsDescriptionText);
            hasDrivingControlPopupBeenDisplayed = true;
        }

        if (Input.GetKeyDown(KeyCode.J) && !hasServeControlPopupBeenDisplayed)
        {
            DisplayPopup(serveControlsDisplayGameObject, serveControlsDescriptionText);
            hasServeControlPopupBeenDisplayed = true;
        }
            
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (hasDrivingControlPopupBeenDisplayed)
                StartCoroutine(FadeOutPopup(drivingControlsCanvasGroup));
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hasServeControlPopupBeenDisplayed)
                StartCoroutine(FadeOutPopup(serveControlsCanvasGroup));
        }
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
