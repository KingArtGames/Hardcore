using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class TopDownCharacter : MonoBehaviour
	{
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;

        public ParticleSystem AttackRay;
        public AudioSource AudioSource;


		void Start()
		{
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
		}

        public void FollowMouse()
        {
            transform.rotation = GetMousePosition();
            transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, 0);
        }

		public void MoveForeward(float move)
		{
            float distance = Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance > 5.5f)
            {
                transform.Translate(transform.forward * move * Time.fixedDeltaTime);
                //ApplyExtraTurnRotation();
            }
		}
        public void MoveSidewards(float move)
        {
            transform.Translate(new Vector3 (1,0,0) * move * Time.fixedDeltaTime);
        }

        private Quaternion GetMousePosition()
        {
            Vector3 mousePosition = Input.mousePosition;

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 relativePos = targetPosition - transform.position;
            relativePos.y = 90;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            return rotation;
        }
	}
}
