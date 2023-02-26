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
			// stream - �����͸� �ְ� �޴� ��� 
			// ���� �����͸� ������ ���̶��
			if (stream.IsWriting) {
				// �� ��ȿ� �ִ� ��� ����ڿ��� ��ε�ĳ��Ʈ 
				// - �� ������ ���� ��������
				stream.SendNext(this.gameObject.transform.position);
				stream.SendNext(this.gameObject.transform.rotation);
				//stream.SendNext(camera.rotation);
			}
			// ���� �����͸� �޴� ���̶�� 
			else {
				// ������� ������ ������� ����. �ٵ� Ÿ��ĳ���� ���־�� ��
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