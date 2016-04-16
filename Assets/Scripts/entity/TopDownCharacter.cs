using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class TopDownCharacter : MonoBehaviour
	{
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;


		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
		}


		public void MoveForeward(float move, bool jump)
		{
                transform.rotation = GetMousePosition();
                transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, 0);
                transform.Translate(transform.forward * move * Time.fixedDeltaTime);
                //ApplyExtraTurnRotation();
		}
        public void MoveSidewards(float move, bool jump)
        {
           // transform.rotation = rotation;
            //transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, 0);
            transform.Translate(new Vector3 (1,0,0) * move * Time.fixedDeltaTime);
        }

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			//float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			//transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
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
