using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.Test
{
    public class SkiJumper : MonoBehaviour
    {
        public event System.Action<float> LongestJumpChanged;
        public event System.Action<string> NameChanged;

        [SerializeField]
        private Equipment equipment;

        private string skiJumperName;
        private float longestJump;       

        public float Jump()
        {
            float distance = Random.Range(60f, 140f);
            distance = (float)System.Math.Round(distance, 1);
            if(distance > longestJump)
            {
                SetLongestJump(distance);
            }
            return distance;
        }

        public void SetData(SkiJumperData data)
        {
            SetName(data.name);
            SetLongestJump(data.longestJump);
            equipment.SetData(data.equipment);
        }

        public SkiJumperData GetData()
        {
            EquipmentData equipmentData = equipment.GetData();
            SkiJumperData data = new SkiJumperData(skiJumperName, longestJump, equipmentData);
            return data;
        }

        public void SetName(string newName, bool callEvent = true)
        {
            skiJumperName = newName;            
            if(callEvent)
            {
                NameChanged?.Invoke(skiJumperName);
            }
        }

        public void SetLongestJump(float distance, bool callEvent = true)
        {
            longestJump = distance;
            if(callEvent)
            {
                LongestJumpChanged?.Invoke(longestJump);
            }
        }
    }
}
