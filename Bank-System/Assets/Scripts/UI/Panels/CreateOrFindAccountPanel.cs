using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cheese.managers;
using Cheese.data;
using Cheese.controller;

namespace Cheese.panelUI{
	public class CreateOrFindAccountPanel : MonoBehaviour {

		[SerializeField] private Button findAccount;
		[SerializeField] private Button createAccount;
		[SerializeField] private Button logOut;
		//[SerializeField] private Button userGraphs;
		[SerializeField] private Text title;

		[SerializeField] private SoundManager soundManagerInstance;
		[SerializeField] private PanelManager panelManagerInstance;
		[SerializeField] private AccountListController accountListController;

		void Awake()
		{
			accountListController.LoadList ();
			title.text="----------------\nCREATE ACCOUNT\n----------------\nchoose between\n----------------\nFIND ACCOUNT\n----------------";

			logOut.GetComponentInChildren<Text>().text="Log Out";
			findAccount.GetComponentInChildren<Text>().text="Find Account";
			createAccount.GetComponentInChildren<Text>().text="Create Account";
			//userGraphs.GetComponentInChildren<Text> ().text="User Graphs";

			logOut.gameObject.SetActive (true);
			findAccount.gameObject.SetActive (true);
			createAccount.gameObject.SetActive (true);
			//userGraphs.gameObject.SetActive (true);


		}

		void OnEnable()
		{

			createAccount.onClick.AddListener (() =>{
				soundManagerInstance.PlayClickSound();
				panelManagerInstance.SwitchPanel(3);
			});
			findAccount.onClick.AddListener (() =>{
				soundManagerInstance.PlayClickSound();
				panelManagerInstance.SwitchPanel(4);
			});

			logOut.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();
				panelManagerInstance.SwitchPanel (1);
			});
			/*userGraphs.onClick.AddListener (()=> {
				soundManagerInstance.PlayClickSound();

			});*/


		}


		void OnDisable()
		{
			createAccount.onClick.RemoveAllListeners ();
			findAccount.onClick.RemoveAllListeners ();
			logOut.onClick.RemoveAllListeners ();

		}

	}
}