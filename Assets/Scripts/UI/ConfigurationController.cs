using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public Slider _laserSlider;
        public Slider _shieldSlider;
        public Slider _heatSlider;

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