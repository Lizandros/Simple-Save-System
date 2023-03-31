using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveSystem.Test
{
    [Serializable]
    public class EquipmentElementData
    {
        public readonly string hexColor;

        public EquipmentElementData(string hexColor)
        {
            this.hexColor = hexColor;
        }

        public static EquipmentElementData CreateData(int version, SerializedData data)
        {
            if(version > 0)
            {
                return CreateDataVer1(data, version);
            }
            else
            {
                return CreateDefault();
            }
        }

        public static EquipmentElementData CreateDefault()
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(Color.white);
            return new EquipmentElementData(hexColor);
        }

        private static EquipmentElementData CreateDataVer1(SerializedData data, int version)
        {
            if(!(data is JsonSerializedData))
            {
                Debug.LogWarning("EquipmentElementData.CreateDataVer1: wrong serialized data type");
                return CreateDefault();
            }

            string json = ((JsonSerializedData)data).GetData();

            if(string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("EquipmentElementData.CreateDataVer1: data json is empty");
                return CreateDefault();
            }

            try
            {
                return JsonConvert.DeserializeObject<EquipmentElementData>(json);
            }
            catch
            {
                return CreateDefault();
            }
        }
    }
}
