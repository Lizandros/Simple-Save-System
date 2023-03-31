using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class JsonSerializedData : SerializedData
    {
        public JsonSerializedData(string jsonData)
        {
            data = jsonData;
        }

        public string GetData()
        {
            if(data is string)
            {
                return (string)data;
            }
            else
            {
                return null;
            }
        }
    }
}
