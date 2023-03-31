using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SaveSystem.Test
{
    public class NameInputField : MonoBehaviour
    {
        [SerializeField]
        private SkiJumper skiJumper;
        [SerializeField]
        private TMP_InputField inputField;

        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(OnValueChanged);
            skiJumper.NameChanged += OnNameChanged;
        }

        private void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(OnValueChanged);
            skiJumper.NameChanged -= OnNameChanged;
        }

        private void OnValueChanged(string sjName)
        {
            skiJumper.SetName(sjName, false);
        }

        private void OnNameChanged(string sjName)
        {
            inputField.text = sjName;
        }
    }
}
