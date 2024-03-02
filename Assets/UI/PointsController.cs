using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shooter
{
    public class PointsController : MonoBehaviour
    {
        public IntReference points;

        private VisualElement _root;
        private Label _label;

        private void Start()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _label = _root.Q<Label>("PointsLabel");
            _label.text = "Points " + points.Value.ToString();
        }

        public void UpdatePoints()
        {
            _label.text = "Points " + points.Value.ToString();
        }
    }
}
