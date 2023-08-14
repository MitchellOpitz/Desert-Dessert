using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingTutorialManager : MonoBehaviour
{
    [SerializeField]
    private float timeToShow;

    [SerializeField]
    private GameObject drivingControlPopup;

    private void Start()
    {
        timeToShow = 5f;
        drivingControlPopup.SetActive(true);
    }

    void Update()
    {
        if (timeToShow > 0)
        {
            timeToShow -= Time.deltaTime;
        }
        else
        {
            drivingControlPopup.SetActive(false);
        }
    }
}
