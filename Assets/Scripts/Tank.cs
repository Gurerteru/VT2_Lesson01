using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    public Vector3 moveInputVelocity = Vector3.zero;    // 移動操作の入力ベクトル
    public Vector3 lookInputVelocoty = Vector3.zero;    // カメラ操作の入力ベクトル
    public float moveSpeed = 10f;                       // 移動速度

    public GameObject topAxis;     // タンクの上部のオブジェクト参照
    public GameObject cannonAxis;  // タンクの砲のオブジェクト参照

    public GameObject bulletPrefab;    // 弾のプレハブ参照
    public GameObject shotPoint;       // 弾の発射位置のオブジェクト参照


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        //move.x = moveInputVelocity.x;    入力の X 成分を移動の X 成分に設定する
        move.z = moveInputVelocity.y;   // 入力の Y 成分を移動の Z 成分に設定する
        
        Vector3 bodyTorque = Vector3.zero;

        bodyTorque.y = moveInputVelocity.x;  // 入力の X 成分を回転の Y 成分に設定する

        transform.Translate(move * moveSpeed *Time.deltaTime);
        transform.Rotate(bodyTorque * Time.deltaTime * 90);

        // === タンクの上部の回転 ===
        Vector3 topTorque = Vector3.zero;
        topTorque.y = lookInputVelocoty.x;  // カメラ操作の入力の X 成分をタンクの上部の回転の Y 成分に設定する

        topAxis.transform.Rotate(topTorque * Time.deltaTime * 90);

        // === タンクの砲の回転 ===
        Vector3 cannonTorque = Vector3.zero;
        cannonTorque.x = lookInputVelocoty.y * -1;  // カメラ操作の入力の X 成分をタンクの砲の回転の Y 成分に設定する

        cannonAxis.transform.Rotate(cannonTorque * Time.deltaTime * 90);

    }

    // 攻撃の入力イベント
    void OnAttack( InputValue value)
    {
        Debug.Log($"attack value is {value.Get() }");

        GameObject bullet = Instantiate(
            bulletPrefab,         // 生成する弾のプレハブ
            shotPoint.transform.position,   // 弾丸の生成位置
            shotPoint.transform.rotation    // 弾丸の生成回転
            );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.transform.forward * 25, ForceMode.Impulse);
    }

    void OnMove( InputValue value)
    {
        Debug.Log($"move value is {value.Get() }");

        moveInputVelocity = value.Get<Vector2>();
    }

    // カメラ操作の入力イベント
    void OnLook( InputValue value)
    {
        Debug.Log($"look value is {value.Get() }");

        lookInputVelocoty = value.Get<Vector2>();
    }
}
