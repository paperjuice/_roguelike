using UnityEngine;
using System.Collections;

public class revolverGeneralBehaviour : MonoBehaviour {

    private Animator _camera;
    private generalWeaponBehaviour generalWeaponBehaviour;
    private playerSts playerSts;
    private Animator anim;
    private bool isOnCooldown;
    //alternativly the right guns shots then the left gun
    private int gunThatShoots = 1;

    //raycasting
    private Ray ray;
    private RaycastHit raycast;

    //trail renderer
    private LineRenderer lineRenderer;
    private float startColour;
    private float endColour;
    private bool isLineRendererFading;
    
    [SerializeField] private TextMesh DmgText;

    [Header("Guns")]
    [SerializeField] private GameObject rightGun;
    [SerializeField] private GameObject leftGun;
    private Ray mouseRay;
    private int layerMask;


    [Header("Cooldown")]
    [SerializeField] private float cooldown=0.5f;
    private float currentCooldown=0f;

    [Header("Energy Cost")]
    [SerializeField] private float energyCost;
    


    void Awake()
    {
        layerMask = LayerMask.GetMask("floor");
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
        lineRenderer = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
        generalWeaponBehaviour = GetComponent<generalWeaponBehaviour>();
    }

    void Update()
    {
        if (generalWeaponBehaviour.isEquipped)
        {
            Attack();
            LineRenderer_fade();
           // RotateGunsTowardsCursor();
        }
    }


    void Attack()
    {
        if (!isOnCooldown && playerSts.currentPlayerENERGY > energyCost)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ActivateLineRendererFade();
                InstantiateBullet();
                playerSts.currentPlayerENERGY -= energyCost;
                isOnCooldown = true;
            }
        }


        if(isOnCooldown)
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown > cooldown)
            {
                currentCooldown = 0;
                isOnCooldown = false;
            }
        }
    }

    void InstantiateBullet()
    {
        if (gunThatShoots == 1)
        {
            gunThatShoots = 2;
        }
        else if (gunThatShoots == 2)
        {
            gunThatShoots = 1;
        }

        switch (gunThatShoots)
        {
            case 1:
                ray.origin = rightGun.transform.position;
                ray.direction = rightGun.transform.forward; 
                lineRenderer.SetPosition(0, rightGun.transform.position);
                rightGun.GetComponent<Animator>().SetTrigger("attack");
                break;
            case 2:
                ray.origin= leftGun.transform.position;
                ray.direction = leftGun.transform.forward;
                lineRenderer.SetPosition(0, leftGun.transform.position);
                leftGun.GetComponent<Animator>().SetTrigger("attack");
                break;
        }


        if (Physics.Raycast(ray, out raycast))
        {
            if (raycast.collider.tag == "enemy")
            {
                raycast.collider.GetComponent<enemySts>().enemyHP -= playerSts.playerDMG;

                raycast.collider.GetComponent<enemySts>().isHit = true;

                DmgText.text = playerSts.playerDMG.ToString("N1");
                Instantiate(DmgText, DmgText.transform.position = new Vector3(raycast.point.x+Random.Range(-1f,1f), raycast.point.y + Random.Range(-1f, 1f), raycast.point.z + Random.Range(-1f, 1f)), transform.rotation);
                
                _camera.SetTrigger("shake");
                lineRenderer.SetPosition(1, raycast.point);
            }
            else
            {
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 20f);
            }

            if (raycast.collider.tag == "prop")
            {
                raycast.collider.GetComponent<propBehaviour>().propIsHit = true;
                _camera.SetTrigger("shake");
            }
            else
            {
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 20f);
            }


        }
    }

    void LineRenderer_fade()
    {
        if (isLineRendererFading)
        {
            startColour -= Time.deltaTime*1.5f;
            endColour -= Time.deltaTime * 1.5f;
            lineRenderer.SetColors(new Color(0.2f, 0.2f, 0.2f, startColour), new Color(0.2f, 0.2f, 0.2f, endColour));
        }

        if (startColour <= 0)
        {
            isLineRendererFading = false;
            
        }
    }
    void ActivateLineRendererFade()
    {
        isLineRendererFading = true;
        startColour = 1;
        endColour = 1;
    }

    void RotateGunsTowardsCursor()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out raycast, 20000f, layerMask))
        {
            print("intra");
            Vector3 pos = raycast.point - transform.position;
            pos.y = 0f;
            rightGun.transform.rotation = Quaternion.LookRotation(pos);
            leftGun.transform.rotation = Quaternion.LookRotation(pos);

        }
    }








}
