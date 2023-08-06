using UnityEngine;

namespace Player
{
    public class ContactsPoller
    {
        private const float CollisionThresh = 0.1f;

        private readonly ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private readonly Collider2D _collider;

        public bool IsGrounded { get; private set; }
        public bool HasLeftContacts { get; private set; }
        public bool HasRightContacts { get; private set; }

        public ContactsPoller(Collider2D  collider)
        {
            _collider = collider;
        }

        public void FixedUpdate()
        {
            IsGrounded = false;
            HasLeftContacts = false;
            HasRightContacts = false;
        
            var contactsCount = _collider.GetContacts(_contacts);
        
            for (var i = 0; i < contactsCount; i++)
            {
                var normal = _contacts[i].normal;

                if (normal.y > CollisionThresh) 
                    IsGrounded = true;
            
                if (normal.x > CollisionThresh) 
                    HasLeftContacts = true;
            
                if (normal.x < -CollisionThresh) 
                    HasRightContacts = true;
            }
        }
    }
}
