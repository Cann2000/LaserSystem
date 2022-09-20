using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflector : MonoBehaviour
{

    // Laser sistemini kullanmak icin laser islemi yapilcak nesnelere LINE RENDERER componenti eklemelisiniz !!!



    Vector3 position;
    Vector3 laserdirection;
    LineRenderer lr;

    public bool laserhit;

    GameObject tempreflector;

    bool laseropen;

    void Start()
    {
        laseropen = false;
        lr = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (laseropen)
        {
            if (laserhit)
            {
                lr.positionCount = 2;
                lr.SetPosition(0, position);

                RaycastHit hit;

                if (Physics.Raycast(position, laserdirection, out hit, Mathf.Infinity))
                {
                    if (hit.transform.CompareTag("reflector"))
                    {
                        tempreflector = hit.transform.gameObject;
                        tempreflector.GetComponent<LaserReflector>().laserhit = true;
                        Vector3 temp = Vector3.Reflect(laserdirection, hit.normal); // vector3.reflect yansima için kullanilir yansimanin yerini gosterir
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

                laserhit = false;
            }
            else
            {
                lr.positionCount = 0;
            }

        }
        else
        {
            lr.positionCount = 0;
        }
            
    }

    public void openray(Vector3 pos, Vector3 dir)
    {
        laseropen = true;
        position = pos;
        laserdirection = dir;
    }
    public void closray()
    {
        laseropen = false;
        tempreflector = null;
    }
}
