using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class JsonFileWriter : FileWriter
    {
        public override string Extension => ".json";

        public override bool TryCreateSave<T>(T obj, int version, out string saveString)
        {
            saveString = "";
            string dataJson = "";
            try
            {
                dataJson = JsonConvert.SerializeObject(obj);
            }
            catch
            {
                Debug.LogWarning("JsonSerializer: Object serialization failed");
                return false;
            }

            JsonSerializedData serializedData = new JsonSerializedData(dataJson);
            Save save = new Save(version, serializedData);

            try
            {
                saveString = JsonConvert.SerializeObject(save);
            }
            catch
            {
                Debug.LogWarning("JsonSerializer: Save serialization failed");
                return false;
            }

            return true;
        }

        public override bool TryWrite<T>(T obj, string path, int version)
        {
            path += Extension;

            string json = "";

            if(!TryCreateSave(obj, version, out json))
            {
                return false;
            }

            try
            {
                File.WriteAllText(path, json);
            }
            catch
            {
                Debug.LogWarning("JsonSerializer: File writing failed - " + path);
                return false;
            }

            return true;
        }
    }
}
