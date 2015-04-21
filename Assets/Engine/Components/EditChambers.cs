using UnityEngine;
using System.Collections;

public class EditChambers : MonoBehaviour {
	
	Ray2D ray;
	RaycastHit2D rayHit;
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButton (0)) {
						

			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);
			
			if (hit.collider != null) {

				//hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = 
				Debug.Log(hit.collider.name);
			}


		}
	}
}
