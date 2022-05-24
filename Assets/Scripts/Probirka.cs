using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Probirka : MonoBehaviour
{
    [SerializeField]
    float Tim;
    private Vector3 delta = Vector3.zero;
    private bool setProbka = true;
    private bool active = false;
    public GameObject kaplya;

    private void OnMouseDown() {
        Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        delta = new Vector3(posMouse.x - transform.position.x, posMouse.y - transform.position.y, 0);
        active = true;
    }

    private void OnMouseDrag() {
        Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posMouse.z = 0;
        posMouse -= delta;
        this.transform.position = posMouse;
        if(Input.GetKeyUp(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0, -45f));
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 0, 45f));
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            setProbka = !setProbka;
            this.transform.GetChild(2).gameObject.SetActive(setProbka);
        }
        if (Mathf.Abs(this.transform.rotation.eulerAngles.z) > 90f && !setProbka)
        {
            Tim += Time.deltaTime;
            if (Tim > 0.5)
            {
                GameObject kap = Instantiate(kaplya);
                kap.name = "Kaplya";
                Vector3 coordinate = this.transform.GetChild(2).transform.position;
                kap.transform.position = coordinate + Vector3.forward * 10;
                Tim = 0;
                GameObject fon = this.transform.GetChild(1).gameObject;
                fon.GetComponent<Image>().fillMethod = Image.FillMethod.Horizontal;
                fon.GetComponent<Image>().fillAmount -= 0.1f;
            }
        }
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        // float scroll = Input.GetAxis("Mouse ScrollWheel");
        // if(scroll != 0)
        // {

        //     transform.Rotate(new Vector3(0, 0, scroll * 900f));
        // }
    }

    private void OnMouseUp() {
        active = false;
    }

    private void Start() {
        Tim = 0;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        for (int i = 0; i < collision.contactCount; i++)
        {
            GameObject gam = collision.contacts[i].collider.gameObject;
            bool f = gam.name == "Kaplya" && !setProbka &&
                this.transform.GetChild(1).GetComponent<Image>().fillAmount < 1 && !active;
                if (f)
                {
                    this.transform.GetChild(1).GetComponent<Image>().fillAmount += 0.1f;
                    Destroy(gam);
                }
        }
        
    }
}
