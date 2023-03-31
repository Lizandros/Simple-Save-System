using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SaveSystem.Test
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField]
        private SaveManager saveManager;
        [SerializeField]
        private SkiJumper skiJumper;
        [SerializeField]
        private Button saveButton;

        private void OnEnable()
        {
            saveButton.onClick.AddListener(OnSaveButtonClicked);
        }

        private void OnDisable()
        {
            saveButton.onClick.RemoveListener(OnSaveButtonClicked);
        }

        private void OnSaveButtonClicked()
        {
            SkiJumperData data = skiJumper.GetData();
            bool successfullySaved = saveManager.SaveData(data.name, data);
            if(successfullySaved)
            {
                Debug.Log("Data saved correctly");
            }
            else
            {
                Debug.LogWarning("Data not saved correctly");
            }
        }
    }
}
