using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;

    [SerializeField] List<Vector3> lstClick;
    [SerializeField] int currentIndex = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;  // Set the depth to the camera's distance

            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
            lstClick.Add(pos);
            target.transform.position = pos;  // Move target to new click position
        }

        // Move the object if there are positions in lstClick
        if (lstClick != null && lstClick.Count > 0)
        {
            // Check if the object is close to the current target position
            if (Vector3.Distance(transform.position, lstClick[currentIndex]) < 0.1f)
            {
                if (currentIndex < lstClick.Count - 1)
                    currentIndex++;  // Move to the next target position
            }

            // Make sure currentIndex does not exceed the number of positions
            if (currentIndex < lstClick.Count)
            {
                MoveInListPosition(lstClick[currentIndex]);  // Move towards the current target position
            }
        }
        else
        {
            MoveUsePosition(target);  // Perform this if no positions are in lstClick
        }
    }

    private void MoveInListPosition(Vector3 position)
    {
        if (target == null) return;

        Vector3 dir = (position - transform.position).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

        //Tisnh goc dua vao vector chi huong cua player
        float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angleZ));
    }

    private void MoveUsePosition(Transform target)
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

        //Tisnh goc dua vao vector chi huong cua player
        float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angleZ));
    }

    //Sử dụng MoveToWards
    private void UseMoveToWards(Transform target)
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    //Sử dụng Rigibody để di chuyển đối tượng
    private void MoveRigibody(Transform target)
    {
        if (GetComponent<Rigidbody>() == null)
        {
            transform.AddComponent<Rigidbody>();
        }

        Vector3 direction = (target.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(direction * moveSpeed);
    }

    // Sử dụng NavMeshAgent
    private void UseNavMeshAgent(Transform target)
    {
        if (target == null) return;
        if (GetComponent<NavMeshAgent>() == null)
        {
            transform.AddComponent<NavMeshAgent>();
        }

        transform.GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    // Sử dụng LoakAt và Translate
    void UseTranslate(Transform target)
    {
        if (target == null) return;

        transform.LookAt(target);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
