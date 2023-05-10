using System;
using System.Collections;
using UnityEngine;

namespace TwoDots.SimpleHealthSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _healthBar;

        [SerializeField]
        private RectTransform _healthBarFade;

        public string healthTag;

        private float _healthbarAnimateTime = .125f;

        private float _healthbarFadeDelay = .5f;

        private float _healtbarFadeAnimationSpeedPercPerSec = .4f;

        private Coroutine _coroutine;

        private void Start()
        {
            GameObject.FindWithTag(healthTag).GetComponent<Health>().healthBar = this;
        }

        public void SetNewHealth(float normalizedHealth)
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(AnimateHealthBar(normalizedHealth));
        }

        private IEnumerator AnimateHealthBar(float normalizedHealth)
        {
            float timer = 0;
            Vector3 newFadeScale = _healthBarFade.localScale;
            Vector3 newHealthScale = _healthBar.localScale;
            float animatePerSecond = (_healthBar.localScale.x - normalizedHealth) / _healthbarAnimateTime;

            while(timer < _healthbarAnimateTime)
            {
                newHealthScale.x -= animatePerSecond * Time.deltaTime;
                _healthBar.localScale = newHealthScale;

                timer += Time.deltaTime;
                yield return null;
            }
            newHealthScale.x = normalizedHealth;
            _healthBar.localScale = newHealthScale;

            yield return new WaitForSeconds(_healthbarFadeDelay);

            while(_healthBarFade.localScale.x > _healthBar.localScale.x)
            {
                newFadeScale.x -= _healtbarFadeAnimationSpeedPercPerSec * Time.deltaTime;
                _healthBarFade.localScale = newFadeScale;

                yield return null;
            }
        }
    }
}
