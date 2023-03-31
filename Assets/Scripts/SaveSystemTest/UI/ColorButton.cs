using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SaveSystem.Test
{
    public class ColorButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<Color> Clicked;

        [SerializeField]
        private Color color;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Vector3 hoverScale;
        private Vector3 defaultScale;

        private void Start()
        {
            defaultScale = transform.localScale;
            image.color = color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Clicked?.Invoke(color);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = hoverScale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = defaultScale;
        }
    }
}
