using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SaveSystem.Test
{
    public class LoadButton : MonoBehaviour
    {
        [SerializeField]
        private SaveManager saveManager;
        [SerializeField]
        private SkiJumper skiJumper;
        [SerializeField]
        private TMP_InputField inputField;
        [SerializeField]
        private Button loadButton;

        private void OnEnable()
        {
            loadButton.onClick.AddListener(OnLoadButtonClicked);
        }

        private void OnDisable()
        {
            loadButton.onClick.RemoveListener(OnLoadButtonClicked);
        }

        private void OnLoadButtonClicked()
        {
            Save save;
            SkiJumperData data;
            bool successfullyLoaded = saveManager.LoadData(inputField.text, out save);
            if(successfullyLoaded)
            {
                data = SkiJumperData.CreateData(save.version, save.data);
                skiJumper.SetData(data);
                Debug.Log("Data loaded correctly");
            }
            else
            {
                Debug.LogWarning("Data not loaded correctly");
            }
        }
    }
}
