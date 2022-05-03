using System.Collections;
using System.Collections.Generic;
using ainst.Core;
using UnityEngine;

namespace ainst.UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField]
        private ScoreController _scoreController;

        [SerializeField]
        private TMPro.TextMeshProUGUI _score;

        void OnEnable()
        {
            _scoreController.ScoreChanged += OnScoreChanged;
        }
        
        void OnDisable()
        {
            _scoreController.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int newScore)
        {
            _score.text = newScore.ToString();
        }
    }
}