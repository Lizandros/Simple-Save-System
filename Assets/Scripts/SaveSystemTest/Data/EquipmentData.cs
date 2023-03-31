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
    public class EquipmentData
    {
        public readonly EquipmentElementData helmet;
        public readonly EquipmentElementData skiJumpingSuit;
        public readonly EquipmentElementData skis;

        public EquipmentData(EquipmentElementData helmet, EquipmentElementData skiJumpingSuit, EquipmentElementData skis)
        {
            this.helmet = helmet;
            this.skiJumpingSuit = skiJumpingSuit;
            this.skis = skis;
        }

        public static EquipmentData CreateData(int version, SerializedData data)
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

        public static EquipmentData CreateDefault()
        {
            EquipmentElementData helmet = EquipmentElementData.CreateDefault();
            EquipmentElementData skiJumpingSuit = EquipmentElementData.CreateDefault();
            EquipmentElementData skis = EquipmentElementData.CreateDefault();

            return new EquipmentData(helmet, skiJumpingSuit, skis);
        }

        private static EquipmentData CreateDataVer1(SerializedData data, int version)
        {
            if(!(data is JsonSerializedData))
            {
                Debug.LogWarning("EquipmentElementData.CreateDataVer1: wrong serialized data type");
                return CreateDefault();
            }

            string json = ((JsonSerializedData)data).GetData();

            JsonSerializedData helmetData = null;
            JsonSerializedData skiJumpingSuitData = null;
            JsonSerializedData skisData = null;

            JObject obj = JObject.Parse(json);
            helmetData = new JsonSerializedData(obj["helmet"].ToString());
            skiJumpingSuitData = new JsonSerializedData(obj["skiJumpingSuit"].ToString());
            skisData = new JsonSerializedData(obj["skis"].ToString());

            EquipmentElementData helmet = EquipmentElementData.CreateData(version, helmetData);
            EquipmentElementData skiJumpingSuit = EquipmentElementData.CreateData(version, skiJumpingSuitData);
            EquipmentElementData skis = EquipmentElementData.CreateData(version, skisData);

            return new EquipmentData(helmet, skiJumpingSuit, skis);
        }
    }
}
