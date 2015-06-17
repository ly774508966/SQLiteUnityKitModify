using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShowUserInfomation : MonoBehaviour {

	public Text text;
	private List<USERS> users;

	void Start () 
	{
		text = GameObject.Find("Text").GetComponent<Text>();
		if(text != null)
		{
			users =	MainDataController.GetUserList();
			text.text = "User Name 1:"+ users[0].username;
			text.text += " \nUser Name 2: " + users[1].username;
			MainDataController.UpdateUserName("username","phamthanhdat",3);

			text.text = "User Name 1:"+ users[0].username;
			text.text += " \nUser Name 2: " + users[1].username;

		}
	}
	

	void Update () 
	{
	
	}
}
