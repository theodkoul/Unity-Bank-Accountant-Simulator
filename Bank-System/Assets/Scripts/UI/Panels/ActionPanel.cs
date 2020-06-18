using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cheese.data;
using Cheese.managers;
using Cheese.controller;

namespace Cheese.panelUI{
	public class ActionPanel : MonoBehaviour {

		[SerializeField] private GameObject greyPanel;
		[SerializeField] private GameObject actionPopUp;
		[SerializeField] private Text popUpTitle;
		[SerializeField] private InputField popUpInput;
		[SerializeField] private Button popUpOk;
		[SerializeField] private Button popUpCancel;

		[SerializeField] private SoundManager soundManagerInstance;
		[SerializeField] private FindAccountPanel findAccPan;
		[SerializeField] private PanelManager panelManager;
		[SerializeField] private AccountListController accList;

		AccountList mylist;

		private int index;
		private bool withdrawBool;

		private int x;

		void Awake()
		{
			popUpTitle.text="Ammount?";
			popUpInput.placeholder.GetComponentInChildren<Text>().text="Enter Here..";
			popUpOk.GetComponentInChildren<Text> ().text="Ok";
			popUpCancel.GetComponentInChildren<Text> ().text="Cancel";

		}

		void OnEnable()
		{
			popUpOk.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();
				if (withdrawBool) {
					popUpTitle.text= WithdrawFunction (mylist,popUpInput.text,index);
				}
				else {
					popUpTitle.text=DepositFunction (mylist,popUpInput.text,index);
				}
			});
			popUpCancel.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();

				accList.LoadList ();
				findAccPan.ChangeAccount (index);
				popUpTitle.text="Ammount";
				popUpInput.text="";
				popUpOk.gameObject.SetActive (true);
				findAccPan.ChangeInteractable(true);
				panelManager.SwitchPanel (4);
			});
		}

		private string WithdrawFunction(AccountList mylist,string input,int i)
		{
			if ((int.TryParse (input, out x)) && x > 0 && x <= 400) {
				x = int.Parse (input);

				if (mylist.CustomerAccountList[i].WithdrawFunds (x)) {
					popUpOk.gameObject.SetActive (false);
					popUpCancel.GetComponentInChildren<Text> ().text="Back";

					accList.ChangeSavedData (mylist);

					return "Cash withdrawn,\nnew balance: " + mylist.CustomerAccountList[i].GetBalance ();

				} else {
					popUpOk.gameObject.SetActive (false);
					return "Insufficient funds";
				}
			} 
			else {
				return "Wrong Input, try again \nusing value between 0 and 400";
			}

		}

		private string DepositFunction(AccountList mylist,string input,int i)
		{
			if ((int.TryParse (input,out x)) && x>0 && x<=1500) {

				popUpOk.gameObject.SetActive (false);
				popUpCancel.GetComponentInChildren<Text> ().text="Back";
				x = int.Parse (input);
				mylist.CustomerAccountList [i].DepositFunds (x);

				accList.ChangeSavedData (mylist);

				return "Transaction completed!\n New balance: "+mylist.CustomerAccountList[i].GetBalance ()+"$";

			}
			else {
				return "Wrong Input, try again \nusing value between 1 and 1500";
			}
		}


		public void setAccountElements(AccountList thislist, int i, bool thisbool)
		{
			mylist = thislist;
			index = i;
			withdrawBool = thisbool;
		}


		void OnDisable()
		{
			popUpOk.onClick.RemoveAllListeners ();
			popUpCancel.onClick.RemoveAllListeners ();
		}

	}
}