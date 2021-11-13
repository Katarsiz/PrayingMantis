using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Crouch;
        
        public KeyCode crouchKeyCide;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_Crouch) {
                m_Crouch = Input.GetKeyDown(crouchKeyCide);
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            // Dodge move set
            if (h != 0f) {
                m_Character.dodgeXMove = h;
            }
            // Pass all parameters to the character control script.
            m_Character.Move(h, v, m_Crouch, m_Jump);
            m_Jump = false;
            m_Crouch = false;
        }
    }
}
