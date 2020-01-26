using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Penguin : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    private float nextTime = 0;
    private bool isHappy = false;
    private Vector3 targetPos = Vector3.zero;

    public PenguinBar Bar { get; set; } = null;

    private void Start()
    {
        Manager.Instance.AddPenguBar(this);
    }

    private void Update()
    {
        CheckForHappy();
    }

    private void FixedUpdate()
    {
        MovePengu();
    }

    public void GoNext()
    {
        isHappy = false;
        nextTime = Random.Range(5, 10);
    }

    private void CheckForHappy()
    {
        if (nextTime <= 0)
        {
            if (!isHappy)
            {
                isHappy = true;
                nextTime = 0;
                Bar.gameObject.SetActive(true);
            }
        }
        else nextTime -= Time.deltaTime;
    }

    private void MovePengu()
    {
        Vector3 dir = targetPos - transform.position;

        if (dir.magnitude < 0.1f)
        {
            targetPos = GetNewPoint();
            dir = targetPos - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private Vector3 GetNewPoint()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        return new Vector3(x, 0, z) * 7;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var a = Vector3.up;
        Gizmos.DrawLine(transform.position + a, targetPos + a);
    }
}
