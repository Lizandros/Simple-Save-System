using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SaveSystem.Test
{
    public class LongestJumpViewer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;
        [SerializeField]
        private SkiJumper skiJumper;

        private void OnEnable()
        {
            skiJumper.LongestJumpChanged += OnLongestJumpChanged;
        }

        private void OnDisable()
        {
            skiJumper.LongestJumpChanged -= OnLongestJumpChanged;
        }

        private void OnLongestJumpChanged(float distance)
        {
            text.text = "Longest jump: " + distance + "m";
        }
    }
}
