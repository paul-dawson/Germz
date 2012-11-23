using UnityEngine;

public class PlayerSwitchingRanged : PlayerSwitching
{
	
	public Texture textureTarget;
	
    private int targetSizeX = 100;
    private int targetSizeY = 100;

    new public void Awake()
    {
        base.Awake();
       // CharacterMotor test = (CharacterMotor)GetComponent("CharacterMotor");
       // test.MovementSpeed = 5.0f;
       // //walkSpeed = 5.0;
       ////jumpHeight = 2;
       // test.JumpSpeed = 2;
    }


    void FixedUpdate()
    {
        base.FixedUpdate();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
         //   gameObject.transform.position = hit.point;
        }
    }

    void OnGUI()
    {
        float x = Input.mousePosition.x - targetSizeX / 2;
        float y = (Screen.height - Input.mousePosition.y) - targetSizeY / 2;

        GUI.DrawTexture(new Rect(x, y, targetSizeX, targetSizeY), textureTarget);

        


    }

}