using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private float _speed;
        public float Speed => _speed;

        [SerializeField]
        private float _jumpForce;
        public float JumpForce => _jumpForce;
    }
}
