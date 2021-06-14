using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public GameObject root;
	public int damege;


    //　コライダのIsTriggerのチェックを外し物理的な衝突をさせる場合
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject != root)
        {
            Destroy(gameObject);
            if (col.gameObject.GetComponent<Charactor>())
            {
                col.gameObject.GetComponent<Charactor>().OnDamege(damege);
            }
        }

    }

    /*
//　コライダのIsTriggerのチェックを入れ物理的な衝突をしない（突き抜ける）場合
	void OnTriggerEnter(Collider col) {
		if(col.tag == "Enemy") {
			Destroy(col.gameObject);
		}
	}
	*/
}
