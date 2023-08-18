using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreDisplayText;
    public int Score { get; private set; } = 0;

    public void ChangeScore(int ScoreModifier) {
        Score += ScoreModifier;
    }

    void Update() {
        ScoreDisplayText.text = $"${Score}";
    }
}
