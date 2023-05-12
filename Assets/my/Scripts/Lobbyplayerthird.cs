using System;
using System.Collections;
//using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
#endif
using Photon.Pun;
using Photon.Realtime;
using Vuplex.WebView;
/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets {
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class Lobbyplayerthird : MonoBehaviourPunCallbacks, IPunObservable {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 100f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 150f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
        [Tooltip("player Jump Power")]
        public float JumpPower = -10f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        //public GameObject CinemachineCameraTarget;

        //[Tooltip("Camera Rotation Speed")]
        public float Camspeed = 2.0f;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        [Tooltip("Customizing hair color")]
        public Material hairs;
        public Material hairs2;



        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        // ray
        private string raytag;
        private float portalcool = 0.0f;

        // customizing
        private Color[] color = new Color[9];
        private int colors;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        public Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;


        public bool turnStop = false;

        public PhotonView PV;

        private Transform tr;
        private float yRotate, yRotateMove;
        public float rotateSpeed = 500.0f;
        public bool JumpCheck = false;

        public BaseWebViewPrefab webViewPrefab;
        private bool IsCurrentDeviceMouse {
            get {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }

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


        private void Awake()
        {
            tr = GetComponent<Transform>();
            // get a reference to our main camera

            if (_mainCamera == null) {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
                if (PV.IsMine)
                    Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
            }
            colors = 0;
            color[0] = Color.white;
            color[1] = Color.red;
            color[2] = Color.yellow;
            color[3] = Color.green;
            color[4] = Color.blue;
            color[5] = Color.cyan;
            color[6] = Color.magenta;
            color[7] = Color.black;
            color[8] = Color.gray;


        }

        public void Start()
        {

            //_cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();

#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
                _hasAnimator = TryGetComponent(out _animator);

                JumpAndGravity();
                GroundedCheck();
                Move();
                cooldown();
        }

        private void LateUpdate()
        {
                CameraRotation();
        }

        private void FixedUpdate()
        {
                PlayerRay();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator) {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition) {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? Camspeed : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            //CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            //    _cinemachineTargetYaw, 0.0f);
        }

        public void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset) {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction

            // 카메라 회전 및 플레이어 움직임
            //  public delegate void EventHandler(object sender, EventArgs e);
            if (!turnStop) {
                Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
                if (Input.GetMouseButton(1)) {
                    yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

                    yRotate = transform.eulerAngles.y + yRotateMove;

                    transform.eulerAngles = new Vector3(0, yRotate, 0);
                }

                inputDirection = Camera.main.transform.TransformDirection(inputDirection);
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);


            // update animator if using character
            if (_hasAnimator) {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {

            if (Grounded) {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator) {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                    JumpCheck = true;
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f) {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f) {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * JumpPower * Gravity);

                    // update animator if using character
                    if (_hasAnimator) {
                        _animator.SetBool(_animIDJump, true);
                        JumpCheck = false;
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f) {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f) {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else {
                    // update animator if using character
                    if (_hasAnimator) {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }
                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity) {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f) {
                if (FootstepAudioClips.Length > 0) {
                    var index = UnityEngine.Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            
        }

        private void PlayerRay()
        {
            if (gameObject.activeSelf == true && !JumpCheck) {
                Ray plray = new Ray(gameObject.transform.position + new Vector3(0f, 0.5f, 0f), transform.up * -1);
                RaycastHit hitinfo;
                Debug.DrawRay(gameObject.transform.position + new Vector3(0f, 0.5f, 0f), transform.up * -1, new Color(1, 0, 0));


                if (Physics.Raycast(plray, out hitinfo, 1f)) {
                    raytag = hitinfo.collider.tag;
                    portaluse(raytag);
                }
            }
        }

        private void portaluse(string raytag)
        {
            if (raytag == "portal1" && Input.GetButtonDown("interact") && portalcool >= 1.0f) {
                gameObject.transform.position = PortalManager.ptmgr.portal2.position;
                Debug.Log(raytag + " Use!");
                portalcool = 0.0f;
            }
            else if (raytag == "portal2" && Input.GetButtonDown("interact") && portalcool >= 1.0f) {
                gameObject.transform.position = PortalManager.ptmgr.portal1.position;
                Debug.Log(raytag + " Use!");
                portalcool = 0.0f;
            }
            else if (raytag == "portal3" && Input.GetButtonDown("interact") && portalcool >= 1.0f) {
                gameObject.transform.position = PortalManager.ptmgr.portal4.position;
                Debug.Log(raytag + " Use!");
                portalcool = 0.0f;
            }
            else if (raytag == "portal4" && Input.GetButtonDown("interact") && portalcool >= 1.0f) {
                gameObject.transform.position = PortalManager.ptmgr.portal3.position;
                Debug.Log(raytag + " Use!");
                portalcool = 0.0f;
            }

        }

        private void cooldown()
        {
            portalcool += Time.deltaTime;
        }

        public void customizing()
        {
            if (colors < 8) {
                hairs.color = color[++colors];
                if (hairs2 != null) {
                    hairs2.color = color[++colors];
                }
            }
            else {
                colors = 0;

            }
        }
    }
}