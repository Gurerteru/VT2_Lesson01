using UnityEngine;

public class SemiAutoGun : BaseWeapon
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
        Debug.Log("セミオートアクション");

        InstantiateBullet();    // 弾丸の生成

        // セミオートガンのトリガーアクションのロジックをここに追加
        base.OnTriggerAction();
    }
}
