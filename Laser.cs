using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    // Laser sistemini kullanmak icin laser islemi yapilcak nesnelere LINE RENDERER componenti eklemelisiniz !!!


    [SerializeField] Transform laserstartpoint;
    Vector3 laserdirection;
    LineRenderer lr;

    GameObject tempreflector;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        laserdirection = laserstartpoint.forward;
        lr.positionCount = 2;
        lr.SetPosition(0, laserstartpoint.position);
    }
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(laserstartpoint.position, laserdirection, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("reflector"))
            {
                tempreflector = hit.transform.gameObject;
                tempreflector.GetComponent<LaserReflector>().laserDegdi = true;
                Vector3 temp = Vector3.Reflect(laserdirection, hit.normal); // vector3.reflect yansýma için kullanýlýr yansýmanýn yerini gösteri;
                hit.collider.GetComponent<LaserReflector>().openray(hit.point, temp);

                lr.SetPosition(1, hit.point);
            }
            

        }
        else
        {
            if (tempreflector)
            {
                tempreflector.GetComponent<LaserReflector>().closray();
            }
            lr.SetPosition(1, laserdirection * 100);
        }
    }
}
