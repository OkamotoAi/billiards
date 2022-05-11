using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 lastMousePosition; //一つ前のマウス座標
    Vector3 targetPos; //回転中心
    Vector3 axis = new Vector3(0, 1, 0); //Y軸周り に回転
    float rot = 0.0f; //回転角度
    
    // Start is called before the first frame update
    void Start()
    {
        //targetに、”Table"の名前のオブジェクトのコン ポーネントを見つけてアクセスする
        Transform target = GameObject.Find("BilliardsTable").transform;
        //変数targetPosにTableの位置情報を取得 
        targetPos = target.position;
        //自分の向きをターゲットの正面に向ける
        // transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        rot = (Input.GetAxis("Horizontal"))*0.5f;
        transform.RotateAround(targetPos, axis, rot);
        // Debug.Log(rot);
        
    }

}
