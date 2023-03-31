using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.Test
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField]
        private EquipmentElement helmet;
        [SerializeField]
        private EquipmentElement skiJumpingSuit;
        [SerializeField]
        private EquipmentElement skis;

        public EquipmentData GetData()
        {
            EquipmentElementData helmetData = helmet.GetData();
            EquipmentElementData skiJumpingSuitData = skiJumpingSuit.GetData();
            EquipmentElementData skisData = skis.GetData();
            EquipmentData data = new EquipmentData(helmetData, skiJumpingSuitData, skisData);
            return data;
        }

        public void SetData(EquipmentData data)
        {
            helmet.SetData(data.helmet);
            skiJumpingSuit.SetData(data.skiJumpingSuit);
            skis.SetData(data.skis);
        }
    }
}
