using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class TopDownCharacter : MonoBehaviour
	{
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;

        public ParticleSystem AttackRay;
        public AudioSource AudioSource;

	}
}
