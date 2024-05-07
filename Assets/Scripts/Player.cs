using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int hp;
    public float moveSpeed;
    public float rotateSpeed;

    PhotonView photonView;
    NavMeshAgent navMeshAgent;
    Camera mainCam;


    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
        
    }

    void Update()
    {
        if (!photonView.IsMine)
            return;

        // 1인칭 움직임
         //float hor = Input.GetAxisRaw("Horizontal");
         //float ver = Input.GetAxisRaw("Vertical");

         //Vector3 forward = transform.forward * ver;
         //Vector3 right = transform.right * hor;
         //Vector3 dir = (forward + right).normalized;

         //transform.position += dir * moveSpeed * Time.deltaTime;

         float rotateX = Input.GetAxis("Mouse X");
         transform.Rotate(Vector3.up * rotateX * rotateSpeed * Time.deltaTime);

        // 현재 마우스 아래에 UI가 있다면 이동할 수 없게 한다.
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << LayerMask.NameToLayer("Ground")))
                navMeshAgent.SetDestination(hit.point);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {

    }
}
