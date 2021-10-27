using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCamera : MonoBehaviour
{
    Camera cam;
    public float speed = 2, scallspeed = 5, rotspeed = 2, maxgrounddis = 20;
    public int maxoutrot = 40, mininrot = 15;
    Vector3 ipos;
    bool outr, inr;

    Rect top, down, left, right;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        float hi = (Screen.height / 10) * 2;
        float wi = Screen.width / 20;
        float w = Screen.width;
        float h = Screen.height;

        down = new Rect(0, 0, w, hi);
        top = new Rect(0, h - hi, w, hi);
        left = new Rect(0, 0, wi, h);
        right = new Rect(w - wi, 0, wi, h);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var p = transform.position;
        var r = transform.rotation;
        var time = Time.deltaTime;

        float w = Input.GetAxis("Mouse ScrollWheel");
        if (outr)
        {
            if (w < 0)
            {
                p.z += w * (scallspeed * 2) * time;
                p.y -= w * scallspeed * 2 * time;
                transform.position = new Vector3(p.x, p.y, p.z);
                outr = false;
            }
        }
        else if (inr)
        {
            if (w > 0)
            {
                p.z += w * (scallspeed * 2) * time;
                p.y -= w * scallspeed * 2 * time;
                transform.position = new Vector3(p.x, p.y, p.z);
                inr = false;
            }
        }
        else
        {
            p.z += w * (scallspeed * 2) * time;
            p.y -= w * scallspeed * 2 * time;
            transform.position = new Vector3(p.x, p.y, p.z);
        }

        if (Physics.Raycast(p, -Vector3.up, out hit, 1000))
        {
            inr = false;
            float ds = Vector3.Distance(p, hit.point);

            if (ds < maxgrounddis)
            {
                transform.rotation = Quaternion.Slerp(r, Quaternion.Euler
                    (new Vector3(mininrot, r.y, r.z)), time * rotspeed);
                outr = true;

            }
            else outr = false;

            if (ds > maxgrounddis * 2) transform.rotation = Quaternion.Slerp(r,
                 Quaternion.Euler(new Vector3(maxoutrot, r.y, r.z)),
                 time * rotspeed);
            else if (ds < maxgrounddis - 1) transform.position = new Vector3(p.x,
                  p.y + 0.1f, p.z);

        }
        else inr = true;

        
        if(Input.GetButtonDown("Fire3"))
        {
            ipos = Input.mousePosition;
            return;
        }

        if(Input.GetButton("Fire3"))
        {
            Vector3 dir = cam.ScreenToViewportPoint(Input.mousePosition - ipos);
            transform.Translate(new Vector3(-dir.x * speed, 0, -dir.y * speed),
                Space.World);
        }


        Vector3 dirr = Vector3.zero;
        Vector2 m = Input.mousePosition;

        if (top.Contains(m)) dirr.z = speed * time;
        else if (down.Contains(m)) dirr.z = -speed * time;

        if (left.Contains(m)) dirr.x = -speed * time;
        else if (right.Contains(m)) dirr.x = speed * time;

        transform.position = transform.position + dirr;

    }
}
