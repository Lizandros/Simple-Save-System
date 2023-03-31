using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.Test
{
    public class EquipmentElementCustomizer : MonoBehaviour
    {
        [SerializeField]
        private EquipmentElement equipmentElement;
        [SerializeField]
        private List<ColorButton> colorButtons;

        private void OnEnable()
        {
            foreach(ColorButton button in colorButtons)
            {
                button.Clicked += ChangeColor;
            }
        }

        private void OnDisable()
        {
            foreach(ColorButton button in colorButtons)
            {
                button.Clicked -= ChangeColor;
            }
        }

        private void ChangeColor(Color color)
        {
            equipmentElement.SetColor(color);
        }
    }
}
