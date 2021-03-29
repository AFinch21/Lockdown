using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerShooting : MonoBehaviour
{
    
    GameObject prefab;
    GameObject grenadePrefab;

    public float viewRadius = 20f;
    public float viewAngle = 170f;
    public float hearingRadius = 40f;
    public float stopDistance = 7f;
    public float attackRange = 3f;
    public float shootRange = 10f;
    public float enemyDamage = 25f;
    public float enemyBulletVelocity = 500f;
    public float enemyBulletMass = 0.05f;
    public float maxMag = 5;
    public float mag = 5;
    public float enemyspeed = 2.5f;
    public float startTimeBtwShots;
    public float startTimeBtwGrenades;
    private Animator animator;
    private float timeBtwShots;
    private float timeBtwGrenades;
    private float agentSpeed;

    public GameObject projectile;
    public Transform muzzle;
    public Transform aimPoint;
    public GameObject grenade;

    Transform target;
    NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int destPoint = 0;

    public bool SeesPlayer;
    public bool SeenBody;
    public bool HeardPlayer;
    public bool AwareOfPlayer;
    public bool audibleShot;
    public bool reloading;
    public bool onPatrol;
    public bool gotHit;
    private bool looking;
    public ParticleSystem MuzzleFlash;

    public bool isMoving;

    public List<Transform> targetsList;
    public List<Transform> friendsList;
    public List<Transform> bodiesList;

    public int damping;

    // Start is called before the first frame update
    void Start()
    {
        prefab = projectile;
        grenadePrefab = grenade;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        animator.SetBool("MovingAlert", false);
        animator.SetBool("Melee", false);
        animator.SetBool("Alert", false);
        animator.SetBool("Reload", false);
        animator.SetBool("Patrol", false);
        animator.SetBool("Sprint", false);
        animator.SetBool("Turn", false);
        animator.SetBool("Inspect", false);


        SeesPlayer = false;
        HeardPlayer = false;
        AwareOfPlayer = false;
        onPatrol = true;
        agent.speed = enemyspeed;
        agent.stoppingDistance = stopDistance;

        GotoNextPoint();

    }

    // Update is called once per frame
    void Update()
    {
        targetsList = this.GetComponentInChildren<FieldOfView>().visibleTargets;
        friendsList = this.GetComponentInChildren<FieldOfView>().visibleFriends;
        bodiesList = this.GetComponentInChildren<FieldOfView>().visibleBodies;
        animator.SetBool("MovingAlert", false);
        animator.SetBool("Melee", false);
        animator.SetBool("Alert", false);
        animator.SetBool("Patrol", false);
        animator.SetBool("Sprint", false);
        animator.SetBool("Inspect", false);
        float distance = Vector3.Distance(target.position, transform.position);
        SeesPlayer = targetsList.Contains(target);

        RaiseAlarm();

        if (bodiesList.Count > 0)
        {
            SeenBody = true;
        }

        //Send on Patrol
        if (onPatrol)
        {
            agent.stoppingDistance = 0f;
            if (!agent.pathPending && onPatrol && !AwareOfPlayer && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }

        //Is weapon silenced and can a shot be heard?
        if (GameObject.Find("Weapon Holder").GetComponentInChildren<InInventory>() != null)
        {
            if (GameObject.Find("Weapon").GetComponent<ProjectileShooter>() != null)
            {
                audibleShot = GameObject.Find("Weapon").GetComponent<ProjectileShooter>().isShooting;
            }
            else if (GameObject.Find("Weapon").GetComponent<ShotgunProjectileShooter>() != null)
            {
                audibleShot = GameObject.Find("Weapon").GetComponent<ShotgunProjectileShooter>().isShooting;
            }
        }

        //Control animations by checking if the enemy is moving or not.
        if (agent.velocity.sqrMagnitude > 0)
        {
            isMoving = true;
        }
        else if (agent.velocity.sqrMagnitude == 0)
        {
            isMoving = false;
        }

        

        if (isMoving && (!AwareOfPlayer || !HeardPlayer))
        {
            animator.SetBool("MovingAlert", false);
            animator.SetBool("Patrol", true);
        }

        if (isMoving && (AwareOfPlayer || HeardPlayer))
        {
            animator.SetBool("MovingAlert", true);
            //animator.SetBool("Patrol", false);
        }

        if (!isMoving)
        {
            animator.SetBool("MovingAlert", false);
            animator.SetBool("Patrol", false);
        }

        if (!isMoving && (HeardPlayer || AwareOfPlayer))
        {
            animator.SetBool("MovingAlert", false);
            animator.SetBool("Alert", true);
        }

        //Enemy can hear a shot but doesn't know where the player is
        if (audibleShot && distance <= hearingRadius)
        {
            HeardPlayer = true;
            animator.SetBool("Alert", true);
            StartCoroutine("LookOnHear", 2f);
            StartCoroutine("LoseTargetHearing", 5f);
        }

        if (distance >= viewRadius && AwareOfPlayer && !SeenBody)
        {
            StartCoroutine("LoseTargetSight", 15f);
        }

        if (SeenBody && !SeesPlayer)
        {
            InvestiagateBody();
        }

        if (SeesPlayer == true)
        {
            AttackPlayer();
        }

        if (looking)
        {
            FaceTarget();
        }
    }


    public void FaceTarget()
    {
        var rotation = Quaternion.LookRotation(aimPoint.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }

    void shootPlayer ()
    {
        GameObject proj = Instantiate(prefab, muzzle.position, Quaternion.identity) as GameObject;
        //proj.transform.position = muzzle.position + muzzle.forward * 2;
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * enemyBulletVelocity;
        rb.mass = enemyBulletMass;
        ParticleSystem mf = Instantiate(MuzzleFlash, muzzle.position, muzzle.rotation);
        mag -= 1f;
        Destroy(proj, 5f);
    }

    void AttackPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        agent.stoppingDistance = 20f;
        AwareOfPlayer = true;
        onPatrol = false;

        if (distance <= viewRadius)
        {
            agent.SetDestination(target.position);
            FaceTarget();
            animator.SetBool("Sprint", true);
            agent.speed = enemyspeed * 2;

            if (distance <= attackRange)
            {
                //Attack target
                //Face target
                animator.SetBool("Melee", true);
            }

            if (distance < shootRange)
            {
                agent.speed = (enemyspeed * 1.5f);
                animator.SetBool("Sprint", false);
                if (timeBtwGrenades <= 0 && !reloading)
                {
                    StartCoroutine("Grenade", 2.5f);
                    animator.SetBool("Grenade", true);
                    timeBtwGrenades = startTimeBtwGrenades;
                    timeBtwShots = startTimeBtwShots;

                }
                else if (timeBtwShots <= 0 && !reloading)
                {
                    shootPlayer();
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                    timeBtwGrenades -= Time.deltaTime;
                }

            }

            if (mag == 0)
            {
                StartCoroutine("Reload", 3f);
            }

        }
    }

    void GotoNextPoint()
    {
        if (destPoint == patrolPoints.Length)
        {
            destPoint = 0;
        }
        // Returns if no patrolPoints have been set up
        if (patrolPoints.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = patrolPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = destPoint + 1;
    }

    void InvestiagateBody()
    {
        agent.stoppingDistance = 2f;
        onPatrol = false;
        AwareOfPlayer = true;
        for (int i = 0; i < bodiesList.Count; i++)
        {
            Transform body = bodiesList[i].transform;
            agent.SetDestination(body.position);
            float bodydist = Vector3.Distance(transform.position, body.position);
            if (bodydist < 2)
            {
                animator.SetBool("Inspect", true);
            }
        }
    }

    void RaiseAlarm()
    {
        for (int i = 0; i < friendsList.Count; i++)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            Transform friend = friendsList[i].transform;
            bool sees = friend.GetComponentInParent<EnemyControllerShooting>().SeesPlayer;
            if (sees == true && distance < viewRadius)
            {
                FaceTarget();
            }
        }
    }

    IEnumerator LoseTargetHearing(float soundDelay)
    {
        onPatrol = false;
        agent.speed = 0f;
        yield return new WaitForSeconds(soundDelay);
        agent.speed = enemyspeed;
        HeardPlayer = false;
        onPatrol = true;
    }

    IEnumerator LoseTargetSight(float sightDelay)
    {
        yield return new WaitForSeconds(sightDelay);
        AwareOfPlayer = false;
    }

    IEnumerator Reload(float reloadTime)
    {
        reloading = true;
        animator.SetBool("Reload", true);
        yield return new WaitForSeconds(reloadTime);
        mag = maxMag;
        animator.SetBool("Reload", false);
        reloading = false;
    }

    IEnumerator Grenade(float cookTime)
    {
        yield return new WaitForSeconds(cookTime);
        GameObject grenade = Instantiate(grenadePrefab, muzzle.position, Quaternion.identity) as GameObject;
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(target.position, transform.position);
        rb.velocity = transform.forward * distance;
        animator.SetBool("Grenade", false);
    }

    IEnumerator LookOnHear(float lookTime)
    {
        looking = true;
        yield return new WaitForSeconds(lookTime);
        looking = false;
    }
}
