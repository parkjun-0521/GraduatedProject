using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif
using Photon.Pun;
using Photon.Realtime;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviourPunCallbacks, IPunObservable
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

#endif
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			// stream - 데이터를 주고 받는 통로 
			// 내가 데이터를 보내는 중이라면
			if (stream.IsWriting) {
				// 이 방안에 있는 모든 사용자에게 브로드캐스트 
				// - 내 포지션 값을 보내보자
				stream.SendNext(this.gameObject.transform.position);
				stream.SendNext(this.gameObject.transform.rotation);
				//stream.SendNext(camera.rotation);
			}
			// 내가 데이터를 받는 중이라면 
			else {
				// 순서대로 보내면 순서대로 들어옴. 근데 타입캐스팅 해주어야 함
				transform.position = (Vector3)stream.ReceiveNext();
				transform.rotation = (Quaternion)stream.ReceiveNext();
				//camera.rotation = (Quaternion)stream.ReceiveNext();
			}
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        private void OnApplicationFocus(bool hasFocus)
		{
			//SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}