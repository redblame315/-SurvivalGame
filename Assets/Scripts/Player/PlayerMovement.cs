using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 5f;


	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public GameObject inv;
	//public GameObject chMenu;
	public bool invShown = false;

	void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		cursorTexture = Resources.Load<Texture2D> ("cursor_default");
		Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
	}

    void Update() {
		Vector3 temp = transform.position; 
		temp.y = 0f; 
		transform.position = temp; 
		if (Input.GetKeyDown(KeyCode.E))
        {
            if (invShown)
            {
                invShown = false;
                Time.timeScale = 1;
                inv.SetActive(false);

                //chMenu.SetActive (false);
            }
            else
            {
                invShown = true;
                Time.timeScale = 0;
                inv.SetActive(true);

            }
            //else { invShown = true; inv.SetActive(true); chMenu.SetActive(true); }
        }
    }

    void OnTriggerEnter(Collider obj) {
        if (obj.tag == "Loot") {
            gameObject.GetComponent<PickUpManager>().PickUp(obj.gameObject);
        }
    }

    void FixedUpdate() {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
	 	Turning ();
        Animating(h, v);
	}

	void Move(float h, float v) {
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning() {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;


			//чёрная магия begin
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
			//чёрная магия end
		}
	}

	void Animating (float h, float v) {
		bool walking = ((h != 0f) || (v != 0f));
		anim.SetBool ("IsWalking", walking);
	}
}
