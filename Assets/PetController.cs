using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum PetState
{
    wait, play, chase
}
public class PetController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject player;

    public GameObject[] ball;
    public PetState state = PetState.wait;
    //public Animator petAnimator;

    //public float timer;
    // public Collider triggerObject;

    public Transform basePosition;
    public float searchRadius, playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectsWithTag("Ball");
    }


    void DistanceCheck()
    {
        if (state == PetState.play) return; //플레이중엔 무시. 플레이를 끝내면 다시 WAIT모드로 바꾸는 버튼ui라던가 필요?
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance < searchRadius) //플레이어가 일정거리 이상 가까워지면 추적모드로
        {
            state = PetState.chase;
        }
        else //멀어지면 대기모드
        {
            state = PetState.wait;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
        if (state == PetState.play) //플레이 모드일때 공을 쫒는건가
        {
            navMeshAgent.SetDestination(ball[0].transform.position);
        }

        else if (state == PetState.chase) //추적일때 플레이어 추적
        {
            navMeshAgent.SetDestination(player.transform.position);
        }

        else //대기모드에선 원래 위치로 복귀
        {
            navMeshAgent.SetDestination(basePosition.position);
        }


        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //petAnimator.SetBool("Run", false);
            navMeshAgent.isStopped = true;
        }

        else if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            //petAnimator.SetBool("Run", true);
            navMeshAgent.isStopped = false;
        }

    }

    /*
    void Delay()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            triggerObject.enabled = false;
        }

        else if (timer <= 0)
        {
            triggerObject.enabled = true;
        }
    }*/
}
