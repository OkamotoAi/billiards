using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour
{
    //変数宣言
    int counter = 0;
    public int counterMax = 50;
    public int delta = 1;

    public float forceCoefficient = 1000f; // 力の係数 
    Vector3 force; // 力のベクトル
    Vector3 down,up,now;

    [SerializeField]
    private LineRenderer direction = null;
    bool pulling;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sphere Control Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pulling){ //左ボタンクリック時
            pulling = getClickObj();
            if (pulling){
                down = new Vector3(-Input.mousePosition.x, 0 ,-Input.mousePosition.y);
                down = this.transform.position;
                Debug.Log("down" + down);
                this.direction.enabled = true;
                this.direction.SetPosition(0, this.transform.position);
                this.direction.SetPosition(1, this.transform.position);
            }

        }else if (Input.GetMouseButton(0) && pulling){ //ボタンドラッグ時 
            var ray = new Ray();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var rayhit = new RaycastHit();
            Physics.Raycast(ray, out rayhit);
            this.direction.SetPosition(0, this.transform.position);
            this.direction.SetPosition(1, rayhit.point);

        } else if (Input.GetMouseButtonUp(0) && pulling){ //ボタンリリース時 
            up =  new Vector3(-Input.mousePosition.x, 0 ,-Input.mousePosition.y);
            up = this.direction.GetPosition(1);
            Debug.Log("up" + pulling);
            force = down-up; //外力の設定
            Debug.Log("クリックした点の座標:" + force);
            transform.GetComponent<Rigidbody>().AddForce(force * forceCoefficient);
            
            this.direction.enabled = false;
            pulling = false;
        }
    }

    bool getClickObj(){
        var ray = new Ray();
        var hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000.0f)){
            if (hit.collider.gameObject.CompareTag("Player") == true) {
                return true;
            }
        }
        return false;
    }
}

