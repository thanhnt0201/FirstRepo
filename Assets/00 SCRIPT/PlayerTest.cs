using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float moveSpeed;
    public Queue<Vector2> listTarget = new Queue<Vector2>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 posTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posTarget.z = 0;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                listTarget.Enqueue(posTarget);
            }
            else
            {
                target.position = posTarget;
            }

        }

        Vector3 dir = target.position - transform.position;

        if (dir.sqrMagnitude <= 0.1f && listTarget.Count != 0)
        {
            target.position = listTarget.Dequeue();
        }

        MoveToTarget(target);
    }

    public void MoveToTarget(Transform target)
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        transform.LookAt(target);
    }
}
