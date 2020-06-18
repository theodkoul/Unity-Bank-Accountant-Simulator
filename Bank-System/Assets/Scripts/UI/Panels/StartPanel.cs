using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cheese.managers;

namespace Cheese.panelUI{
	public class StartPanel : MonoBehaviour {

		[SerializeField] private Text title;
		[SerializeField] private InputField username;
		[SerializeField] private InputField password;
		[SerializeField] private Button adminLogIn;

		[SerializeField] private SoundManager soundManagerInstance;
		[SerializeField] private PanelManager panelManagerInstance;
		void Awake()
		{

			username.placeholder.GetComponent<Text>().text="Username";
			password.placeholder.GetComponent<Text>().text="Password";

			adminLogIn.GetComponentInChildren<Text>().text="Log In";

			username.gameObject.SetActive (true);
			adminLogIn.gameObject.SetActive (true);
			password.gameObject.SetActive (true);

		
		}

		void OnEnable()
		{

			adminLogIn.onClick.AddListener (() =>{
				soundManagerInstance.PlayClickSound();
				CheckLogIn(username.text,password.text);
				username.text="";
				password.text="";
			});
		}

		void CheckLogIn(string u, string p)
		{

			if (u=="user") {
				
				if (p=="1234") {
					panelManagerInstance.SwitchPanel(2);
				} 
				else {
					title.text = "Wrong Username or Password,\n try again!";
				}
			} 
			else {
				title.text="Wrong Username or Password,\n try again!";
			}

		}

		void OnDisable()
		{
			adminLogIn.onClick.RemoveAllListeners ();

		}

	}
}