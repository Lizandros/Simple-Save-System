using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem.Test
{
    [Serializable]
    public class SkiJumperData
    {
        public readonly string name;
        public readonly float longestJump;
        public readonly EquipmentData equipment;

        public SkiJumperData(string name, float longestJump, EquipmentData equipment)
        {
            this.name = name;
            this.longestJump = longestJump;
            this.equipment = equipment;
        }

        public static SkiJumperData CreateData(int version, SerializedData data)
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

        public static SkiJumperData CreateDefault()
        {
            string name = "Noname";
            float longestJump = 0;
            EquipmentData equipment = EquipmentData.CreateDefault();
            return new SkiJumperData(name, longestJump, equipment);
        }

        private static SkiJumperData CreateDataVer1(SerializedData data, int version)
        {
            if(!(data is JsonSerializedData))
            {
                Debug.LogWarning("EquipmentElementData.CreateDataVer1: wrong serialized data type");
                return CreateDefault();
            }

            string json = ((JsonSerializedData)data).GetData();

            string name = null;
            float longestJump = 0;
            JsonSerializedData equipmentData = null;

            JObject obj = JObject.Parse(json);

            name = obj["name"].ToString();
            longestJump = float.Parse(obj["longestJump"].ToString());
            equipmentData = new JsonSerializedData(obj["equipment"].ToString());

            EquipmentData equipment = EquipmentData.CreateData(version, equipmentData);
            return new SkiJumperData(name, longestJump, equipment);
        }
    }
}
