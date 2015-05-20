using UnityEngine;
using System.Collections;

public class ChamberMove : MonoBehaviour {
	
	Ray2D ray;
	RaycastHit2D rayHit;
	

	private Transform LevelObject;
	private Vector2 initPos;
	private Vector2 pos;
	private Chamber[] ChambersToMove = new Chamber[8];
	public Sprite[] ChamberSprite;


	private enum MovementDir 
	{
		Left,
		Right,
		Up,
		Down,
		current
	}

	private MovementDir movementDir;


	// Update is called once per frame
	void Update () 
	{

		///////////////////////////////////////////////////////////////////////////////////
		// When the mouse button is first pressed down
		///////////////////////////////////////////////////////////////////////////////////
		if (Input.GetMouseButtonDown (0)) 
		{

			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);
			
			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{
					//define intial chamber position
					initPos = hit.collider.gameObject.transform.position;

					initPos.x = Mathf.Round(initPos.x);
					initPos.y = Mathf.Round(initPos.y);

					Debug.Log(" inital position : x :" + initPos.x + " y " + initPos.y);

				}
			}
		}

		///////////////////////////////////////////////////////////////////////////////////
		// If left mouse is down
		///////////////////////////////////////////////////////////////////////////////////
		if (Input.GetMouseButton (0)) 
		{
			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);

			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{

					// define chamber position
					pos = hit.collider.gameObject.transform.position;
					// round the postion so it is only moves by one unit
					pos.x = Mathf.Round(pos.x);
					pos.y = Mathf.Round(pos.y);

					///////////////
					// DIRECTION //
					///////////////
					if (pos.x == initPos.x && pos.y == initPos.y) {
						movementDir = MovementDir.current;
					} else if (pos.x  > initPos.x) {
						movementDir = MovementDir.Right;
					} else if ( pos.x  < initPos.x) {
						movementDir = MovementDir.Left;
					} else if (pos.y  > initPos.y ) {
						movementDir = MovementDir.Up;
					} else if (pos.y  < initPos.y ) {
						movementDir = MovementDir.Down;
					}

					// find out the distance that the chamber has moved
					float differencePos = Vector2.Distance (initPos, pos);

				}	
			}
		}

		///////////////////////////////////////////////////////////////////////////////////
		// if left mouse Button is up
		///////////////////////////////////////////////////////////////////////////////////
		if (Input.GetMouseButtonUp (0)) 
		{
			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);
			
			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{
					// update the chamber array with the new content
					// move left
					if (movementDir == MovementDir.Left){
						//debug
						Debug.Log("left");
						//move chambers method
						moveChambers(MovementDir.Left, pos, true);
					} else if (movementDir == MovementDir.Right) {
						//debug
						Debug.Log("right");
						//move chambers
						moveChambers(MovementDir.Right, pos, false);
					} else if (movementDir == MovementDir.Up) {
						Debug.Log("up");
						//move chambers
						moveChambers(MovementDir.Up, pos, true);	
					} else if (movementDir == MovementDir.Down) {
						Debug.Log("down");
						//move chambers
						moveChambers(MovementDir.Down, pos, false);
					}
				}
			}
		}
	}

	/// <summary>
	/// Moves the chambers.
	/// </summary>
	/// <param name="direction">Direction.</param>
	/// <param name="currentChamber">Current chamber.</param>
	/// <param name="positive">If set to <c>true</c> positive.</param>
	void moveChambers(MovementDir direction, Vector2 currentChamber, bool positive)
	{
		for (int x = 0; x < 8; x++) 
		{
			for (int y = 0; y < 8; y++) 
			{
				//define the chamber
				Chamber Chambers = CreateChamberArray.ChamberArray[x,y];

				// define the direction
				float changedPos = 0;

				// change the value based on the direction
				if (direction == MovementDir.Left || direction == MovementDir.Right)
				{
					if (currentChamber.y == y)
					{
						if (positive){
							// DEFINE A ROW OF CHAMBERS TO MOVE 
							ChambersToMove[x] = Chambers;
							// EMPTY THE CHAMBER IN THE 2D ARRAY
							CreateChamberArray.ChamberArray[x,y] = null;

							if (Chambers.Object.transform.position.x == 7)
							{ 
								CreateNewChamber (x,y);
							}
							
							//null check
							if (Chambers != null)
							{
								// DEFINE THE MOVEMENT OF THE CHAMBER (one to the right)
								changedPos = Chambers.Object.transform.position.x - 1;
								
								// DELETE CHAMBER ON THE END
								if (Chambers.Object.transform.position.x == 0)
								{
									Debug.Log("Destory Extra");
									// destroy the gameobject
									Destroy(Chambers.Object);
									// remove from chamber array
									Chambers = null;
								}
							} 

						} else {

							// DEFINE A ROW OF CHAMBERS TO MOVE 
							ChambersToMove[x] = Chambers;
							// EMPTY THE CHAMBER IN THE 2D ARRAY
							CreateChamberArray.ChamberArray[x,y] = null;


							if (Chambers.Object.transform.position.x == 0)
							{ 
								CreateNewChamber (x,y);
							}

							//null check
							if (Chambers != null)
							{
								// DEFINE THE MOVEMENT OF THE CHAMBER (one to the right)
								changedPos = Chambers.Object.transform.position.x + 1;

								// DELETE CHAMBER ON THE END
								if (Chambers.Object.transform.position.x == 7)
								{
									Debug.Log("Destory Extra");
									// destroy the gameobject
									Destroy(Chambers.Object);
									// remove from chamber array
									Chambers = null;
								}
							} 
						}

						//null check before moving the objects
						if (Chambers != null)
						{
							// and move the chambers
							Chambers.Object.transform.position = new Vector2(changedPos,currentChamber.y);
						}
					}

				} else if (direction == MovementDir.Down || direction == MovementDir.Up){
					if (currentChamber.x == x)
					{
						if (positive){
							changedPos = Chambers.Object.transform.position.y + 1;

						} else {
							changedPos = Chambers.Object.transform.position.y - 1;

						}
						// and move the chambers
						Chambers.Object.transform.position = new Vector2(currentChamber.x, changedPos);
					}	
				}
			}

		}


			//	
			// UPDATE THE MAIN CHAMBER ARRAY //
			//
			for (int x = 0; x < 7; x++) 
			{
				for (int y = 0; y < 7; y++) 
				{
					if (currentChamber.y == y)
					{
						//
						// find the movement direction and move the chamber acordinginly in the main array
						//
						if (direction == MovementDir.Right)
						{
							CreateChamberArray.ChamberArray[x+1,y] = null;
							CreateChamberArray.ChamberArray[x+1,y] = ChambersToMove[x];
						} else if (direction == MovementDir.Left)
						{
							Debug.Log(CreateChamberArray.ChamberArray[x-1,y].Name);

							CreateChamberArray.ChamberArray[x-1,y] = null;
							CreateChamberArray.ChamberArray[x-1,y] = ChambersToMove[x];
						} else if (direction == MovementDir.Up)
						{
							CreateChamberArray.ChamberArray[x,y+1] = null;
							CreateChamberArray.ChamberArray[x,y+1] = ChambersToMove[y];
						} else if (direction == MovementDir.Down)
						{
							CreateChamberArray.ChamberArray[x,y-1] = null;
							CreateChamberArray.ChamberArray[x,y-1] = ChambersToMove[y];
						}

						Debug.Log("Main Chamber Array Updated");

					}
				}
			}

		}



	/// <summary>
	/// Updates the grid.
	/// </summary>
	void UpdateGrid ()
	{


		//LevelObject = GameObject.Find ("Level_01");
		//Transform LOtrans = LevelObject.transform;


		// isnt it better just to move the chambers rather than remove from the array


		// remove all old children from the grid that have changed
		for (int x = 0; x < 8; x++) {
			for (int y = 0; y < 8; y++) {
				// if the chamber in the array has value (it could be empty....)
				CreateChamberArray.ChamberArray[x,y].Object = null;

					// check if the chamber has been defined to change

					// make the value null so it can be edited later

				}
		}



		// work though all the children of the level gameobject.. 
		foreach (Transform child in LevelObject)
		{
			// if statement to define if its one of the chambers that has changed postion


			// round the positions of the 
			float PosX  = Mathf.Round(child.position.x);
			float PosY  = Mathf.Round(child.position.y);
			// vector 2 for each child
			Vector2 v = new Vector2(PosX,PosY);
			
			// make the chamber positon the chamber number in the array
//			chamber newChamber = new Chamber();


//			CreateChamberArray.ChamberArray[(int)v.x,(int)v.y].Object.transform = child;

			

		}



		// remove all the chambers then place them back 


		// add the childern to the grid 
	}



	void RemoveChambers ()
	{





	}



	void CreateNewChamber (int x, int y)
	{
		Debug.Log ("created chamber at " + x + " , " + y);

		//Create chamber
		Chamber chamber = new Chamber();
		chamber.Name = "chamber" + "_" + x + "_" + y;
		chamber.Type = UnityEngine.Random.Range(0,5);


		//add it to the array
		CreateChamberArray.ChamberArray [x, y] = chamber;

		
		//instantite gameobject loaded from the resources folder, set the sprite and the name
		GameObject ChamberGO = (GameObject)Instantiate(Resources.Load("Prefab/brick"), new Vector2(x,y),Quaternion.identity);
		ChamberGO.GetComponent<SpriteRenderer>().sprite = ChamberSprite[chamber.Type];
		ChamberGO.name = "chamber" + "_" + x + "_" + y;
		
		//set the transform of the chamber class
		chamber.Object = ChamberGO;
		
		//set the parent of chamber to the level gameobject
		//ChamberGO.transform.parent = GO_level.transform;
		
		
		SpriteRenderer spriteRenderer = ChamberGO.GetComponent<SpriteRenderer>();
		spriteRenderer.sortingLayerName = "Chambers";

	}

}
