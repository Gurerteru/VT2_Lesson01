using UnityEngine;

public class AutoGun : BaseWeapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ** トリガーアクション (オーバーライド) ** //
    public override void OnTriggerAction()
    {
        Debug.Log("フルオートアクション");

        InstantiateBullet();    // 弾丸の生成

        // オートガンのトリガーアクションのロジックをここに追加
        base.OnTriggerAction();
    }
}
