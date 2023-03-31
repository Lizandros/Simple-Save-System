using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.Test
{
    public class CloudTest : MonoBehaviour
    {
        [SerializeField]
        private SaveManager saveManager;
        [SerializeField]
        private SkiJumper skiJumper;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.F8))
            {
                Load();
            }
            if(Input.GetKeyDown(KeyCode.F4))
            {
                Save();
            }
        }

        private void Load()
        {
            saveManager.LoadDataFromCloud("key", OnLoaded);
        }

        private void OnLoaded(bool success, Save save)
        {
            Debug.Log("Successfully loaded? " + success.ToString());
            Debug.Log("LoadedData: " + save.data);
            skiJumper.SetData(SkiJumperData.CreateData(save.version, save.data));
        }

        private void Save()
        {
            SkiJumperData data = skiJumper.GetData();
            saveManager.SaveDataToCloud(data.name, data, OnSaved);
        }

        private void OnSaved(bool success)
        {
            Debug.Log("Successfully saved? " + success.ToString());
        }
    }
}
