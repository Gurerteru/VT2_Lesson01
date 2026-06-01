using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Header("*** Base Settings ***")]
    [SerializeField] protected GameObject _bulletWeapon;
    [SerializeField] protected GameObject _ShotPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ** トリガーアクション ** //
    public virtual void OnTriggerAction()
    {
        
    }

    // ** 弾丸の生成 ** //
    protected void InstantiateBullet()
    {
        // === 弾丸の生成 ===
        GameObject bulle = Instantiate(
            _bulletWeapon,                      // 生成オブジェクト
            _ShotPoint.transform.position,      // 生成位置
            Quaternion.identity                 // 生成角度
        );

        // === 弾丸をカメラの方向に飛ばす ===
        Rigidbody bulletRb = bulle.GetComponent<Rigidbody>();         // 弾丸のRigidbodyを取得
        bulletRb.AddForce(Camera.main.transform.forward * 50f,ForceMode.Impulse);   // 弾丸に前方への力を加える

        Destroy(bulle, 5f);                     // n秒後に弾丸を削除
    }
}
