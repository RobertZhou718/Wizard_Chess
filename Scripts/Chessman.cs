using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


public abstract class Chessman : MonoBehaviour
{
    public AudioSource attack_source;
    public AudioSource is_hit_source;
    public AudioSource walk_source;
    public AudioSource die_source;


    public int currentX { set; get; }
    public int currentY { set; get; }
    public bool isWhite;
    
    protected Animator animator;
    protected NavMeshAgent agent;

    protected bool is_moving;
    protected bool is_attacking;

    public Chessman target;

    protected Vector3 destination;

    protected float timer = 2f;
    protected bool wasCreate = false;

    protected bool isChanged = false;
    public bool[,] possible_moves { set; get; }


    public void SetPosition(int x, int y)
    {
        currentX = x;
        currentY = y;
    }

    public virtual bool[, ] PossibleMove()
    {
        return new bool[8,8];
    }

    //play animation when is seledted
    public virtual void isSelected() { }

    public bool[, ] GetPossibleMoves()
    {
        return possible_moves;
    }

    //play walk animation
    public void moveToTarget(Vector3 destination, ref Chessman target) {
        if (agent == null) return;
        agent.destination = destination;
        setWalk();
        BoardManager.Instance.pause = true;
        if (target == null)
        {   
            is_moving = true;
        }
        else
        {
            this.destination = destination;
            is_attacking = true;
            this.target = target;
            transform.LookAt(target.transform);
            target.transform.LookAt(transform);
            //target.setAttack();
            //target.target = GetComponent<Chessman>();
            //attackTarget(ref target);
        }
    }

    protected void lookAtTarget()
    {
        if (target == null) return;
        transform.LookAt(target.transform);
    }

    protected void isMoving()
    {
        if (!is_moving) return;

        if (Vector3.Distance(agent.destination, transform.position) < 0.05f)
        {
            is_moving = false;
            setIdle();
            BoardManager.Instance.pause = false;

            if (isWhite)
            {
                BoardManager.Instance.AITurn = true;
            }
        }
    }

    protected void isAttacking()
    {
        if (!is_attacking) return;
        
        if (Vector3.Distance(agent.destination, transform.position) < 0.8f)
        {
            agent.destination = transform.position;
            setAttack();
            if (target == null)
            {
                moveToTarget(this.destination, ref target);
                is_attacking = false;
            }
            //BoardManager.Instance.pause = false;
        }
        
    }

    // is used by animator frame
    public void attack_target()
    {
        if (target == null) return;
        target.setDie();
        target.GetComponent<HittedMatEffect>().Active();
        target.GetComponent<HittedMatEffect>().SetColor(Color.yellow, 1.5f);
    }

    public void falling()
    {
        die_source.Play();
    }
    public void walking() 
    {
        walk_source.Play();
    }

    public void is_hitAudio()
    {
        if (target == null) return;
        target.is_hit_source.Play();
    }

    public void yawp()
    {
        attack_source.Play();
    }

    // is used by animator frame
    public void die_destroy()
    {
        Destroy(gameObject);
    }

    protected void generationPiece()
    {
        if (!wasCreate)
        {
            if (timer < 0)
            {
                wasCreate = true;
                GetComponent<HittedMatEffect>().clossHighlight();
                BoardManager.Instance.pause = false;
                return;
            }

            if (isWhite)
            {
                GetComponent<HittedMatEffect>().Active();
                GetComponent<HittedMatEffect>().SetColor(Color.red, 2);
            }
            else
            {
                GetComponent<HittedMatEffect>().Active();
                GetComponent<HittedMatEffect>().SetColor(Color.blue, 2);
            }

            timer -= Time.deltaTime;
            
            BoardManager.Instance.pause = true;
        }
    }

    protected void newStart()
    {
        animator = GetComponent<Animator>();
        is_moving = false;
        setIdle();
        agent = GetComponent<NavMeshAgent>();
        timer = 2f;

        //possible_moves = new bool[8, 8];

    }

    protected void newUpdate()
    {
        generationPiece();
        isMoving();
        isAttacking();
        lookAtTarget();
        close_up();
        //UpdatePossibleMoves();
    }

    protected void setIdle()
    {
        animator.SetBool("attacking", false);
        animator.SetBool("walking", false);
        animator.SetBool("die", false);
        //animator.SetBool("idle2", false);
    }

    protected void setWalk()
    {
        animator.SetBool("attacking", false);
        animator.SetBool("walking", true);
        animator.SetBool("die", false);
        //animator.SetBool("idle2", false);
    }

    protected void setAttack()
    {
        animator.SetBool("attacking", true);
        animator.SetBool("walking", false);
        animator.SetBool("die", false);
        //animator.SetBool("idle2", false);
    }

    protected void setDie()
    {
        animator.SetBool("attacking", false);
        animator.SetBool("walking", false);
        animator.SetBool("die", true);
    }

    protected void close_up()
    {
        if (target == null) return;
        if (target.GetType() == typeof(King))
        {
            CameraController.Instance.target = gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target.isWhite != isWhite && target.GetComponentInChildren<Chessman>() == other.gameObject)
        {
            is_hit_source.Play();
            setDie();
        }
    }

}
