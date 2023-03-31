using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SaveSystem.Test
{
    public class JumpPanel : MonoBehaviour
    {
        [SerializeField]
        private SkiJumper skiJumper;
        [SerializeField]
        private Button jumpButton;
        [SerializeField]
        private TMP_Text jumpText;

        private void OnEnable()
        {
            jumpButton.onClick.AddListener(OnJumpButtonClicked);
        }

        private void OnDisable()
        {
            jumpButton.onClick.RemoveListener(OnJumpButtonClicked);
        }

        private void OnJumpButtonClicked()
        {
            if(skiJumper == null)
            {
                Debug.LogWarning("No ski jumper connected");
                return;
            }

            float distance = skiJumper.Jump();
            ActualizeJumpText(distance);
        }

        private void ActualizeJumpText(float distance)
        {
            jumpText.text = distance.ToString() + "m!";
        }
    }
}
