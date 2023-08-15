using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreDisplayText;
    int Score;

    public void ChangeScore(int ScoreModifier) {
        Score += ScoreModifier;
    }

    void Update() {
        ScoreDisplayText.text = "$" + Score.ToString();
    }
}
