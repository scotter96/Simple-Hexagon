using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public bool rotatorEffect = true;
    public bool deathShake = true;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (rotatorEffect)
            transform.Rotate(Vector3.forward, Time.deltaTime * -30f);
    }

    public void StartDeathShake()
    {
        if (deathShake)
            animator.SetTrigger("Start Death Shake");
    }
}