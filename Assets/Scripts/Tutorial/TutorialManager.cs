using System.Collections;
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
    [SerializeField] private TextMeshProUGUI firstLetterText;
    [SerializeField] private TextMeshProUGUI secondLetterText;
    [SerializeField] private TextMeshProUGUI drivingControlsDescriptionText;
    [SerializeField] private TextMeshProUGUI serveControlsDescriptionText;

    private CanvasGroup drivingControlsCanvasGroup;
    private CanvasGroup serveControlsCanvasGroup;

    private bool hasDrivingControlPopupBeenDisplayed;
    private bool hasServeControlPopupBeenDisplayed;

    [SerializeField] private TutorialPlayerInput tutorialPlayerInput;

    private void Start()
    {
        gameDescriptionGameObject.SetActive(true);

        drivingControlsDisplayGameObject.SetActive(false);
        hasDrivingControlPopupBeenDisplayed = false; //move later to when popup is displayed and set true

        serveControlsDisplayGameObject.SetActive(false);
        hasServeControlPopupBeenDisplayed = false; //move later to when popup is displayed and set true

        drivingControlsCanvasGroup = drivingControlsDisplayGameObject.GetComponent<CanvasGroup>();
        serveControlsCanvasGroup = serveControlsDisplayGameObject.GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        tutorialPlayerInput.OnAllKeysPressed += FadeOutDrivingTutorial;
        tutorialPlayerInput.OnMouseButtonClicked += FadeOutServeTutorial;
    }

    private void OnDisable()
    {
        tutorialPlayerInput.OnAllKeysPressed -= FadeOutDrivingTutorial;
        tutorialPlayerInput.OnMouseButtonClicked -= FadeOutServeTutorial;
    }

    //change to events later on
    private void Update()
    {
        /*
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
        */
    }

    private void FadeOutDrivingTutorial()
    {
        if (tutorialPlayerInput.AllKeysPressed() && hasDrivingControlPopupBeenDisplayed)
            StartCoroutine(FadeOutPopup(drivingControlsCanvasGroup));
    }

    private void FadeOutServeTutorial()
    {
        if (tutorialPlayerInput.MouseButtonClicked() && hasServeControlPopupBeenDisplayed)
            StartCoroutine(FadeOutPopup(serveControlsCanvasGroup));
    }

    //may not be needed as text is not currently changing
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
