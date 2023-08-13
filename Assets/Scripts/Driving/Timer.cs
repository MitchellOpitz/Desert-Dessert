using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private DestinationCheckManager destinationCheckManager;
    [SerializeField] private DayNightCycle dayNightCycle;
    [SerializeField] private float maxTime = 60f;
    private float timeLeft;

    private void Awake()
    {
        timeLeft = maxTime;
    }

    private void Update()
    {
        CountTimeLeft();
        UpdateTimeText();
    }

    private void CountTimeLeft()
    {
        if (timeLeft > 0f && !destinationCheckManager.playerReachedDestination)
            timeLeft -= Time.deltaTime;
        else
            timeLeft = 0f;
    }

    private void UpdateTimeText()
    {
        if (!destinationCheckManager.playerReachedDestination)
            timeLeftText.text = Mathf.CeilToInt(timeLeft).ToString();
    }

    /*
    public int CalculateBackdropSpriteIndex()
    {
        if (timeLeft >= 30)
            return 0;
        else if (timeLeft >= 20)
            return 1;
        else if (timeLeft >= 10)
            return 2;
        else
            return 3;
    }
    */
}
