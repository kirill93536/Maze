using UnityEngine;
using TMPro;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public LayerMask interactableLayer;
    public int maxHp = 100;
    public int trapDamage = 20;

    public UIController uIController;


    private float xRotation = 0f;
    private Rigidbody rb;
    private bool cursorLocked = true;
    private bool keyFound = false;
    private int currentHP;
    private int previousCurrentHP;

    private void Start() {
        currentHP = maxHp;
        previousCurrentHP = currentHP;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private void Update() {
        // Передвижение
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.right * horizontal + transform.forward * vertical;
        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.deltaTime);

        // Вращение камеры

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Interaction
        if(Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }

        // UI
        if(currentHP != previousCurrentHP) {
            uIController.UpdateHP(currentHP.ToString());
            previousCurrentHP = currentHP;
            Debug.Log("Took damage!!!");
        }
        
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Trap")) {
            TakeDamage(trapDamage);
        }
    }

    private void TakeDamage(int damage) {
        currentHP -= damage;

        if(currentHP <=0) {
            Debug.Log("Player Died");
            uIController.ShowLooseMenu();
        }
        else {
            Debug.Log("Player took " + damage + " damage. Current HP: " + currentHP);
        }
    }

    private void Interact() {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, interactableLayer)) {
                if(hit.collider.CompareTag("Key") && keyFound == false) {
                    Debug.Log("Found a key!");
                    keyFound = true;
                    uIController.UpdateKeyText();
                    Destroy(hit.collider.gameObject);
                }

                if(hit.collider.CompareTag("Door")) {
                    if(keyFound == true) {
                        Debug.Log("Door opened");
                        uIController.ShowWinMenu();
                    }
                    else {
                        Debug.Log("Need to find the key");
                    }
                }

                if(hit.collider.CompareTag("Button")) {
                    Debug.Log("Pressed a button!");
                    hit.collider.gameObject.GetComponent<Button>().DeactivateTrap();
                }
            }
    }
}


