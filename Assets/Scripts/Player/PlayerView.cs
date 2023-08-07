using Common;
using Controls;
using UniRx;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerView : InitMonoBehaviour<PlayerView.Params>
    {
        private const float JumpThresh = 0.5f;
        
        public readonly struct Params
        {
            public readonly MoveWidget MoveWidget;

            public Params(MoveWidget moveWidget)
            {
                MoveWidget = moveWidget;
            }
        }
        
        [SerializeField]
        private PlayerData _data;

        [SerializeField] 
        private Rigidbody2D _rigidbody;
        
        [SerializeField] 
        private Collider2D _collider;
        
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] 
        private Camera _camera;
        public Camera Camera => _camera;

        private ContactsPoller _contactsPoller;
        private Vector2 _deltaPosition;

        protected override void Init()
        {
            _contactsPoller = new ContactsPoller(_collider);

            InputParams.MoveWidget.DeltaPosition.Subscribe(ChangePlayerPositionAndFlip).AddTo(Disposables);
            Observable.EveryFixedUpdate().Subscribe(_ => OnFixedTick()).AddTo(Disposables);
        }

        private void OnFixedTick()
        {
            _contactsPoller.FixedUpdate();
            
            var moveVelocity = _deltaPosition.normalized * _data.Speed;
            _rigidbody.velocity = _rigidbody.velocity.Change(x: moveVelocity.x);

            if (_contactsPoller.IsGrounded && _deltaPosition.y > JumpThresh)
                _rigidbody.AddForce(Vector3.up * _data.JumpForce);
        }
        
        private void ChangePlayerPositionAndFlip(Vector2 deltaPosition)
        {
            _deltaPosition = deltaPosition;
            
            if (deltaPosition.x < 0)
                _spriteRenderer.flipX = true;
            else if (deltaPosition.x > 0)
                _spriteRenderer.flipX = false;
        }
    }
}
