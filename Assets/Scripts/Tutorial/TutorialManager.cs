using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //create static instance of this class
    public static TutorialManager Instance { get; private set; }

    [Header("Popup GameObjects")]
    [SerializeField] private GameObject gameDescriptionGameObject;
    [SerializeField] private GameObject drivingControlsDisplayGameObject;
    [SerializeField] private GameObject serveControlsDisplayGameObject;

    [Header("Descriptions")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI drivingControlsDescriptionText;
    [SerializeField] private TextMeshProUGUI serveControlsDescriptionText;

    private CanvasGroup drivingControlsCanvasGroup;
    private CanvasGroup serveControlsCanvasGroup;

    private bool hasDrivingControlPopupBeenDisplayed;
    private bool hasServeControlPopupBeenDisplayed;

    [SerializeField] private TutorialPlayerInput tutorialPlayerInput;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DisplayPopup(gameDescriptionGameObject); //do this when after transitioning scenes

        DisplayPopup(drivingControlsDisplayGameObject); //do this when closing game description popup
        hasDrivingControlPopupBeenDisplayed = true;

        DisplayPopup(serveControlsDisplayGameObject); //do this after serve scene transition
        hasServeControlPopupBeenDisplayed = true;

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
            StartCoroutine(FadeOutPopup(drivingControlsCanvasGroup, 4f));
    }

    private void FadeOutServeTutorial()
    {
        if (tutorialPlayerInput.MouseButtonClicked() && hasServeControlPopupBeenDisplayed)
            StartCoroutine(FadeOutPopup(serveControlsCanvasGroup, 4f));
    }

    public void DisplayPopup(GameObject popup) => popup.SetActive(true);

    public void ClosePopup(GameObject popup) => popup.SetActive(false);

    private IEnumerator FadeOutPopup(CanvasGroup canvasGroup, float secondsBeforeFadeStarts)
    {
        yield return new WaitForSeconds(secondsBeforeFadeStarts);

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
