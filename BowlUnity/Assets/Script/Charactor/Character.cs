using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour
{
    [SerializeField]
    public Status status;

    [SerializeField]
    Slider hpSlider;

    float _forceHeight = 0;       //吹き飛ばす高さ調整値
    float _forcePower = 10;        //吹き飛ばす強さ調整値

    public float speed = 6.0F;          //歩行速度
    public float jumpSpeed = 8.0F;      //ジャンプ力
    public float gravity = 20.0F;       //重力の大きさ
    public float rotateSpeed = 3.0F;    //回転速度

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private float h, v;

    public FloatingJoystick joystick;
    private Rigidbody rb;

    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        speed = status.speed;
        SetHp();

        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
    }

    //void Update()
    //{
        //h = joystick.Horizontal;   //左右矢印キーの値(-1.0~1.0)
        //v = joystick.Vertical;     //上下矢印キーの値(-1.0~1.0)

        //if (controller.isGrounded)
        //{
        //    //gameObject.transform.Rotate(new Vector3(0, rotateSpeed * h, 0));
        //    moveDirection = - speed * v * gameObject.transform.forward;
        //    if (Input.GetButton("Jump"))
        //    {
        //        animator.SetTrigger("jump");
        //        moveDirection.y = jumpSpeed;
        //    }
        //}
        //moveDirection.y -= gravity * Time.deltaTime;
        //controller.Move(moveDirection * Time.deltaTime);
        //animator.SetFloat("speed", Mathf.Abs(v));
    //}

    private void Update()
    {
        if (!m_Jump)
        {
            //m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            m_Jump = false;
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        h = joystick.Horizontal;   //左右矢印キーの値(-1.0~1.0)
        v = joystick.Vertical;     //上下矢印キーの値(-1.0~1.0)

        //bool crouch = Input.GetKey(KeyCode.C);
        bool crouch = false;

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
        transform.Translate(m_Move * speed * Time.deltaTime, Space.World);
    }



void SetHp()
    {
        if (status.maxhp < status.hp) status.hp = status.maxhp;
        hpSlider.value = (float)status.hp / (float)status.maxhp;
    }

    //何かに触れたとき
    //オブジェクトがCharacterControllerで動かされている場合OnControllerColliderHit以外で衝突は検出できなくなる事に注意 
    //OnCollisionEnterは反応しにくくなる（反応しない訳ではない）　また「物体が動いている間だけ」呼び出しが発生する 
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //障害物の場合
        if (hit.transform.parent.gameObject.tag == "Prop")
        {
            Debug.Log("hit:" + hit.gameObject);

            //ぶつかった相手からRigitBodyを取り出す
            Rigidbody otherRigitbody = hit.transform.parent.gameObject.GetComponent<Rigidbody>();
            if (!otherRigitbody)
            {
                return;
            }

            Debug.Log("test2");

            //吹き飛ばす方向を求める(プレイヤーから触れたものの方向)
            Vector3 toVec = GetAngleVec(this.gameObject, hit.gameObject);

            //Y方向を足す
            toVec = toVec + new Vector3(0, _forceHeight, 0);

            //吹き飛ばし
            otherRigitbody.AddForce(toVec * status.atk, ForceMode.Impulse);

            //ダメージ
            status.hp -= 1;
            SetHp();
        }

        //回復アイテムの場合
        if (hit.transform.parent.gameObject.tag == "RecoveryItem")
        {

            Destroy(hit.transform.gameObject);

            //回復
            status.hp += 20;
            SetHp();
        }


    }

    Vector3 GetAngleVec(GameObject _from, GameObject _to)
    {
        //高さの概念を入れないベクトルを作る
        Vector3 fromVec = new Vector3(_from.transform.position.x, 0, _from.transform.position.z);
        Vector3 toVec = new Vector3(_to.transform.position.x, 0, _to.transform.position.z);

        return Vector3.Normalize(toVec - fromVec);
    }

}
