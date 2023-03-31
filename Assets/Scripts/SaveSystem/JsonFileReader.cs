using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SaveSystem
{
    public class JsonFileReader : FileReader
    {
        public override string Extension => ".json";

        public override bool TryReadFromString(string json, out Save save)
        {
            save = null;
            int version = -1;
            JsonSerializedData data = null;
            JObject obj = JObject.Parse(json);

            if(!int.TryParse(obj["version"].ToString(), out version))
            {
                Debug.LogWarning("JsonSerializer: No file version found");
                return false;
            }
            data = JsonConvert.DeserializeObject<JsonSerializedData>(obj["data"].ToString());

            save = new Save(version, data);

            return true;
        }

        public override bool TryRead(string path, out Save save)
        {
            save = null;
            string json = "";
            try
            {
                json = File.ReadAllText(path);
            }
            catch
            {
                Debug.LogWarning("JsonSerializer: File reading failed - " + path);                
                return false;
            }

            return TryReadFromString(json, out save);
        }
    }
}
