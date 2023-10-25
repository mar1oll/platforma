using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float shootDistance;

    public int currentWeapon;
    public KeyCode[] keysToSwitch;

    public Weapon[] weapons;
    [SerializeField] private Transform hand;

    public Animator animator;
    
    private void Start()
    {
        for (int i = 0; i < hand.childCount; i++)
        {
            weapons[i] = hand.GetChild(i).GetComponent<Weapon>();
        }
    }
    
    
    private void FixedUpdate()
    {

    }
    
    private void Update()
    {   
        CheckSwitch();
        
        if(Input.GetMouseButtonDown(0))
        {
            animator.CrossFade("Shoot", 0.05f);
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, shootDistance))
            {
                //Destroy(hit.collider.gameObject);
            }
        }
    }

    private void CheckSwitch()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                currentWeapon--;
            } 
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                currentWeapon++;
            }

            currentWeapon = Mathf.Clamp(currentWeapon, 0, 1);

            SwitchWeapon();
        }
        

        for (int i = 0; i < keysToSwitch.Length; i++)
        {
            if (Input.GetKeyDown(keysToSwitch[i]))
            {
                if (currentWeapon != i)
                {
                    currentWeapon = i;

                    SwitchWeapon();
                }

                break;
            }
        }
    }

    private void SwitchWeapon()
    {
        for (int j = 0; j < weapons.Length; j++)
                    {
                        weapons[j].gameObject.SetActive(false);
                    }

                    weapons[currentWeapon].gameObject.SetActive(true);
    }
}
