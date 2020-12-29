using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTouchPoint : MonoBehaviour
{
	
	Character chara;

	//　レイを飛ばす距離
	private float rayRange = 100f;
	//　移動する位置
	private Vector3 targetPosition;
	//　速度
	private Vector3 velocity;
	//　移動スピード
	[SerializeField]
	private float moveSpeed = 1.5f;
	//　マウスクリックで移動する位置を決定するかどうか
	[SerializeField]
	private bool isMouseDownMode = true;
	//　スムースにキャラクターの向きを変更するかどうか
	[SerializeField]
	private bool smoothRotateMode = true;
	//　回転度合い
	[SerializeField]
	private float smoothRotateSpeed = 180f;
	private CharacterController characterController;
	private Animator animator;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		targetPosition = transform.position;
		velocity = Vector3.zero;
		chara = GetComponent<Character>();
		moveSpeed = chara.status.speed / 2.0f;
	}

	void Update()
	{
		if (characterController.isGrounded)
		{
			velocity = Vector3.zero;
			//　マウスクリックまたはisMouseDownModeがOffの時マウスの位置を移動する位置にする
			if (Input.GetButton("Fire1") || !isMouseDownMode)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, rayRange, LayerMask.GetMask("Field")))
				{
					targetPosition = hit.point;
				}
			}

			//　移動の目的地と0.1mより距離がある時は速度を計算
			if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
			{
				var moveDirection = (targetPosition - transform.position).normalized;
				velocity = new Vector3(moveDirection.x * moveSpeed, velocity.y, moveDirection.z * moveSpeed);
				//　スムースモードの時は徐々にキャラクターの向きを変更する
				if (smoothRotateMode)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), smoothRotateSpeed * Time.deltaTime);
					//　スムースモードでなければ一気に目的地の方向を向かせる
				}
				else
				{
					transform.LookAt(transform.position + new Vector3(moveDirection.x, 0, moveDirection.z));
				}
				//　アニメーションパラメータの設定
				animator.SetFloat("speed", moveDirection.magnitude);
				//　目的地に近付いたら走るアニメーションをやめる
			}
			else
			{
				animator.SetFloat("speed", 0f);
			}
		}

		velocity.y += Physics.gravity.y * Time.deltaTime;
		characterController.Move(velocity * Time.deltaTime);
	}
}
