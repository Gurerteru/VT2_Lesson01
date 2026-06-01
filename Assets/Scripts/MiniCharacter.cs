using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniCharacter : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject BulletPrifab;    // 弾のプレハブ参照
    public GameObject ShotPoint;       // 弾の発射位置のオブジェクト参照
    public float ShotPower = 10f;      // 弾の発射力

    [Header("** Camera settings **")]
    // カメラ軸(CameraJoint)のオブジェクト
    public GameObject CameraJoint;

    [Header("** Weapon Settings **")]
    public BaseWeapon weapon;           // 武器をアタッチする変数

    public Vector3 _inputmoveVelocity = Vector3.zero;    // 移動操作の入力ベクトル
    public Vector3 _inputLookVelocity = Vector3.zero;    // カメラ操作の入力ベクトル
    public float _inputAttackValue = 0f;                // 攻撃操作の入力値
    public float moveSpeed = 10f;                       // 移動速度

    private Vector3 angles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();     // メソッドを呼び出す
        Look();

        if(_inputAttackValue > 0.1f)
        {
            weapon.OnTriggerAction();
        }
    }

    //===========================================
    // 移動メソッド
    // 引数   :  無し
    // 戻り値 :  無し
    // ==========================================
    public void Move()
    {
        Vector3 velocity = Vector3.zero;    // 速度の変数
        velocity.z = _inputmoveVelocity.y;   // 入力(前後)で前後移動
        velocity.x = _inputmoveVelocity.x;   // 入力(左右)で左右移動

        transform.Translate(velocity * Time.deltaTime);
    }

    //===========================================
    // 回転(向き)メソッド
    // 引数   :  無し
    // 戻り値 :  無し
    // ==========================================
    public void Look()
    {
        angles.x += _inputLookVelocity.y;   // y 入力で x 軸回転
        angles.y += _inputLookVelocity.x;   // x 入力で y 軸回転

        // x 軸の角度に制限を設ける
        // 範囲を設ける数学関数
        // [Mathf.Clamp(対象地、最小値、最大値)]
        angles.x = Mathf.Clamp(angles.x, -90, 90);

        // TpsPlauey(自分)のｙ軸だけ回転
        transform.eulerAngles = new Vector3(0,angles.y,0);     // 角度を代入
        // CameraJoint の x 軸を回転
        CameraJoint.transform.eulerAngles = new Vector3(angles.x, angles.y, 0);
    }

    void OnMove(InputValue value)
    {
        Debug.Log($"move value is {value.Get()}");

        _inputmoveVelocity = value.Get<Vector2>();  // 入力の値をベクトルとして取得して moveInputVelocity に設定する
    }

    void OnLook(InputValue value)
    {
        Debug.Log($"look value is {value.Get()}");

        _inputLookVelocity = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        Debug.Log($"attack value is {value.Get()}");

        _inputAttackValue = value.Get<float>();
    }
}
