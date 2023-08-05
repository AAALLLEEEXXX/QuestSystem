using UnityEngine;

namespace QuestsSystem.Missions
{
    public class MissionSwitchObjectView : MissionObjectViewBase
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Sprite _spriteCompleted;

        private Sprite _spriteDefault;

        private void Awake()
        {
            _spriteDefault = _spriteRenderer.sprite;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // var player = collision.gameObject.GetComponent<PlayerController>();
            //
            // if (player != null)
            //     OnActivateObject?.Invoke();
        }

        public override void StateComplete()
        {
            _spriteRenderer.sprite = _spriteCompleted;
        }

        public override void StateActive()
        {
            _spriteRenderer.sprite = _spriteDefault;
        }
    }
}
