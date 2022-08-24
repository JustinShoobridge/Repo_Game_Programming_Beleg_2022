
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ProjectileGunTutorial : MonoBehaviour
{

    [SerializeField] public GameObject bullet;
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform attackPoint;

    private Character_Controls _Character_Controls;
    public float shootForce, upwardForce;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _Character_Controls = new Character_Controls();
        _Character_Controls.Enable();
        _Character_Controls.NormalMovement.Mouse_Actions.started += Shoot;
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {

        fpsCam = Camera.main;
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); 
                                                                                                   
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        _Character_Controls.NormalMovement.Mouse_Actions.started -= Shoot;
    }
}
