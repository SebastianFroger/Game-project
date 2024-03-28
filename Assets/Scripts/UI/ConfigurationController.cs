using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;


namespace Shooter
{
    public class ConfigurationController : Singleton<ConfigurationController>
    {
        public UnitStatsSO unitStats;
        public int sliderValueMultipler = 20;
        public TMPro.TMP_Text _text;
        public Slider _moveSlider;
        public TMPro.TMP_Text _moveText;
        public Slider _laserSlider;
        public TMPro.TMP_Text _laserText;
        public Slider _shieldSlider;
        public TMPro.TMP_Text _shieldText;
        public Slider _heatSlider;
        public TMPro.TMP_Text _heatText;

        private Slider[] _sliders;
        private string _textString = "Configuration Points ";
        private float _maxPoints;
        private float _usedPoints;

        private void OnEnable()
        {
            _sliders = GetComponentsInChildren<Slider>();
            _maxPoints = unitStats.configurationPoints.value;
            _usedPoints = _sliders.Sum(s => s.value);
            SetText();
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == _moveSlider.gameObject)
                _moveText.gameObject.SetActive(true);
            else
                _moveText.gameObject.SetActive(false);

            if (EventSystem.current.currentSelectedGameObject == _laserSlider.gameObject)
                _laserText.gameObject.SetActive(true);
            else
                _laserText.gameObject.SetActive(false);

            if (EventSystem.current.currentSelectedGameObject == _shieldSlider.gameObject)
                _shieldText.gameObject.SetActive(true);
            else
                _shieldText.gameObject.SetActive(false);

            if (EventSystem.current.currentSelectedGameObject == _heatSlider.gameObject)
                _heatText.gameObject.SetActive(true);
            else
                _heatText.gameObject.SetActive(false);
        }

        public void OnSliderValueChange(Slider slider)
        {
            if (slider.value == 0)
            {
                slider.value = slider.value + 1;
                return;
            }

            _usedPoints = _sliders.Sum(s => s.value);
            if (_usedPoints > _maxPoints)
            {
                slider.value = slider.value - 1;
                return;
            }

            SetText();
        }

        void SetText()
        {
            _text.text = _textString + (_maxPoints - _usedPoints).ToString();
        }

        public void ApplyConfigValues()
        {
            unitStats.currentMoveBattery.value = (float)_moveSlider.value * sliderValueMultipler;
            unitStats.maxMoveBattery.value = (float)_moveSlider.value * sliderValueMultipler;
            unitStats.maxAttackBattery.value = (float)_laserSlider.value * sliderValueMultipler;
            unitStats.currentAttackBattery.value = (float)_laserSlider.value * sliderValueMultipler;
            unitStats.currentShieldBattery.value = (float)_shieldSlider.value * sliderValueMultipler;
            unitStats.maxShieldBattery.value = (float)_shieldSlider.value * sliderValueMultipler;
            unitStats.maxHeat.value = (float)_heatSlider.value * sliderValueMultipler;
        }
    }
}