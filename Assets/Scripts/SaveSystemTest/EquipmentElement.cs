using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.Test
{
    public class EquipmentElement : MonoBehaviour
    {
        [SerializeField]
        private List<MeshRenderer> meshRenderers;

        private Color color = Color.white;
        private MaterialPropertyBlock matPropertyBlock;
        private readonly int colorID = Shader.PropertyToID("_Color");

        private int id;

        public void Start()
        {
            matPropertyBlock = new MaterialPropertyBlock();
        }

        public void SetColor(Color newColor)
        {
            color = newColor;
            matPropertyBlock.SetColor(colorID, color);
            foreach(MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.SetPropertyBlock(matPropertyBlock);
            }
        }

        private void SetColor(string hexColor)
        {
            Color color;
            if(ColorUtility.TryParseHtmlString(hexColor, out color))
            {
                SetColor(color);
            }
        }

        private Color GetColor()
        {
            return color;
        }

        private string GetHexColor()
        {
            return "#" + ColorUtility.ToHtmlStringRGB(GetColor());
        }

        public EquipmentElementData GetData()
        {
            string hexColor = GetHexColor();
            EquipmentElementData data = new EquipmentElementData(hexColor);
            return data;
        }

        public void SetData(EquipmentElementData data)
        {
            SetColor(data.hexColor);
        }
    }
}