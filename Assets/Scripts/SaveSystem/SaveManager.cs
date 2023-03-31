using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField]
        private int currentVersion;

        private FileReader jsonReader;
        private FileWriter writer;

        private CloudReader cloudReader;
        private CloudWriter cloudWriter;

        private bool cloudLocked;

        private const string DATA_PATH = "/Saves/";

        public void Start()
        {
            writer = new JsonFileWriter();
            jsonReader = new JsonFileReader();
            cloudWriter = new MockCloudWriter();
            cloudReader = new MockCloudReader();
        }

        #region Local Save

        public bool SaveData<T>(string key, T obj)
        {
            if(writer == null)
            {
                Debug.LogWarning("SaveManager: No serializer connected");
                return false;
            }

            if(!writer.IsTypeSerializable(typeof(T)))
            {
                Debug.LogWarning("SaveManager: Tried to save nonserializable type of data - " + typeof(T).ToString());
                return false;
            }

            string folderPath = Application.persistentDataPath + DATA_PATH;

            if(!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch
                {
                    Debug.LogWarning("SaveManager: Cannot create directory - " + folderPath);
                }
            }

            string path = Application.persistentDataPath + DATA_PATH + key;

            bool serializationSuccess = writer.TryWrite(obj, path, currentVersion);

            return serializationSuccess;
        }

        public bool LoadData(string key, out Save save)
        {
            FileReader reader = null;
            save = null;

            bool file_found = false;
            string path = Application.persistentDataPath + DATA_PATH + key;
            string possible_path1 = path + jsonReader.Extension;
            //could possibly add other extensions paths

            if(File.Exists(possible_path1))
            {
                path = possible_path1;
                file_found = true;
            }
            //could possibly search for other extensions

            if(!file_found)
            {
                Debug.LogWarning("SaveManager: Tried to load nonexistent file from path - " + path);
                return false;
            }

            FileInfo info = new FileInfo(path);

            if(info.Extension == jsonReader.Extension)
            {
                reader = jsonReader;
            }

            if(reader == null)
            {
                Debug.LogWarning("SaveManager: No reader connected");
                return false;
            }            

            bool deserializationSuccess = reader.TryRead(path, out save);

            return deserializationSuccess;
        }

        public bool DeleteData(string key)
        {
            string path = Application.persistentDataPath + DATA_PATH + key;

            if(!File.Exists(path))
            {
                Debug.LogWarning("SaveManager: Tried to delete nonexistent file from path - " + path);
                return false;
            }

            try
            {
                File.Delete(path);
            }
            catch
            {
                Debug.LogWarning("SaveManager: Failed when deleting file from path - " + path);
                return false;
            }

            return true;
        }

        #endregion

        #region Cloud Save

        public bool SaveDataToCloud<T>(string key, T obj, Action<bool> callback)    //callback for informing about success/failure
        {
            if(cloudLocked)
                return false;
            string save = null;
            if(!writer.TryCreateSave(obj, currentVersion, out save))
            {
                Debug.LogWarning("SaveManager: Save creating failed - " + key);
                return false;
            }
            cloudLocked = true;
            cloudWriter.DataSaved += OnCloudDataSaved;
            cloudWriter.DataSaveFailed += OnCloudDataSaveFailed;
            cloudWriter.SaveDataAsync(key, save, callback);
            return true;
        }

        public void OnCloudDataSaved(string key, Action<bool> callback)
        {
            cloudWriter.DataSaved -= OnCloudDataSaved;
            cloudWriter.DataSaveFailed -= OnCloudDataSaveFailed;
            cloudLocked = false;
            Debug.Log("Data saved in cloud - " + key);
            callback(true);
        }

        public void OnCloudDataSaveFailed(string key, Action<bool> callback)
        {
            cloudWriter.DataSaved -= OnCloudDataSaved;
            cloudWriter.DataSaveFailed -= OnCloudDataSaveFailed;
            cloudLocked = false;
            Debug.Log("Data saved in cloud failed - " + key);
            callback(false);
        }

        public bool LoadDataFromCloud(string key, Action<bool, Save> callback)
        {
            if(cloudLocked)
                return false;
            cloudLocked = true;
            cloudReader.DataLoaded += OnCloudDataLoaded;
            cloudReader.DataLoadFailed += OnCloudDataLoadFailed;
            cloudReader.LoadDataAsync(key, callback);
            return true;
        }

        public void OnCloudDataLoaded(string key, string data, Action<bool, Save> callback)
        {
            cloudReader.DataLoaded -= OnCloudDataLoaded;
            cloudReader.DataLoadFailed -= OnCloudDataLoadFailed;
            cloudLocked = false;
            Save save = null;
            if(!jsonReader.TryReadFromString(data, out save))
            {
                Debug.LogWarning("Wrong data downloaded - " + key);
                return;
            }

            callback(true, save);
        }

        public void OnCloudDataLoadFailed(string key, Action<bool, Save> callback)
        {
            cloudReader.DataLoaded -= OnCloudDataLoaded;
            cloudReader.DataLoadFailed -= OnCloudDataLoadFailed;
            cloudLocked = false;
            Debug.Log("Data load from cloud failed - " + key);
            callback(false, null);
        }
        #endregion
    }
}
