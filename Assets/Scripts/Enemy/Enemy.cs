using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(Rigidbody),typeof(BoxCollider))]        // このスクリプトを使うにはNavMeshAgentが必須ですということ
public class Enemy : MonoBehaviour
{

    public NavMeshAgent EnemyAgent;

    public MiniCharacter player;

    public Rigidbody rb;

    // 目的地の座標
    public Vector3 targetPoint;

    // 巡回座標
    public Vector3[] patrolPooint;
    public int currentIndex;

    void Start()
    {
        // agent = GetCommponent<NavNasgAgent>();と一緒。
        TryGetComponent<NavMeshAgent>(out EnemyAgent);

        TryGetComponent<Rigidbody>(out rb);

        // シーン上から指定されたコンポーネントを持っているオブジェクトを取得する。
        player = GameObject.FindAnyObjectByType<MiniCharacter>();
    }

    
    void Update()
    {
        // Rigidbodyの影響軽減
        rb.linearVelocity = Vector3.zero;

        // Playerがある程度近づいてきたら追跡してくる
        Vector3 posA = player.transform.position;
        Vector3 posB = transform.position;
        float distance = Vector3.Distance(posA,posB);

        if(distance < 3)
        {
            targetPoint = posA;     // Playerの座標を入れる
        }
        else
        {
            targetPoint = patrolPooint[currentIndex % patrolPooint.Length];     // Enemyの座標を入れる
        }

        float patrollDistance = Vector3.Distance(patrolPooint[currentIndex % patrolPooint.Length],transform.position);
        if(patrollDistance < 1f)
        {
            currentIndex++;
        }

        // エージェントに目的座標を設定するだけ
        EnemyAgent.SetDestination(targetPoint);
    }
}
