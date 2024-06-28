using Lando.Core.Extensions;
using UnityEngine;

namespace Tatedrez.Entities
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private float _frequency = 1f;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Update()
        {
            float time = Time.time;
            float y = Mathf.Cos(f: time * _frequency) * _amplitude;
            _spriteRenderer.transform.localPosition = Vector3.zero.With(y: y);
        }
    }
}
