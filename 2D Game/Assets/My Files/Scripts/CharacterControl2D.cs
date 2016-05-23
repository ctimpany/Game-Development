using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (Character2D))]
public class CharacterControl2D : MonoBehaviour {
	
	private Character2D m_Character;
	private bool m_Jump;
	private bool m_Climb;

	private void Awake()
	{
		m_Character = GetComponent<Character2D>();
	}
	
	
	private void Update()
	{
		m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		m_Climb = CrossPlatformInputManager.GetButton ("Fire2");
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		// Pass all parameters to the character control script.
		m_Character.Move(h, v, m_Climb, m_Jump);
	}
}
