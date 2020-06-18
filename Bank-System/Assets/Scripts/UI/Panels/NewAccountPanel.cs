using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cheese.managers;
using Cheese.data;
using Cheese.controller;

namespace Cheese.panelUI{

	public class NewAccountPanel : MonoBehaviour {

		[SerializeField] private InputField name;
		[SerializeField] private InputField surname;
		[SerializeField] private InputField address;
		[SerializeField] private InputField balance;
		[SerializeField] private InputField accountState;
		[SerializeField] private InputField accountID;

		[SerializeField] private Text title;

		[SerializeField] private Button save;
		[SerializeField] private Button back;

		[SerializeField] private SoundManager soundManagerInstance;
		[SerializeField] private PanelManager panelManager;
		[SerializeField] private AccountListController accList;

		string[] users = new string[3];

		private int decimalLength;
		private int realCount;
		private string accountIDNumber;

		private bool check;

		private AccountList myCustomers = new AccountList ();

		void Awake()
		{

			title.text="Register New Account";
			name.placeholder.GetComponent<Text>().text="Name";
			surname.placeholder.GetComponent<Text>().text="Surname";
			address.placeholder.GetComponent<Text>().text="Address";

			balance.text="Balance: 0";
			accountState.text = "Account State: Active";

			accountID.text = "Account ID: " +CountID ();

			balance.readOnly=true;
			accountState.readOnly=true;
			accountID.readOnly=true;

			back.GetComponentInChildren<Text>().text="Go Back";
			save.GetComponentInChildren<Text>().text="Register Account";



		}

		void OnEnable()
		{
			
			save.onClick.AddListener (() => {
				soundManagerInstance.PlayClickSound();
				RegisterAction ();
				name.text="";
				surname.text="";
				address.text="";
				accountID.text="Account ID: " +CountID ();
			});

			back.onClick.AddListener (() => {
				soundManagerInstance.PlayClickSound();
				panelManager.SwitchPanel (2);
				title.text="Register New Account";
				accList.LoadList ();

			});
		}

		private string CountID()
		{
			realCount = myCustomers.CustomerAccountList.Count + 1;
			decimalLength = (realCount).ToString("D").Length + (6-realCount.ToString ().Length);

			accountIDNumber =realCount.ToString("D" + decimalLength.ToString());
			return accountIDNumber;
		}

		private void RegisterAction()
		{
			if (CheckFields ()) {

				title.text="Account "+name.text+" "+surname.text+" added!";

				if (!(addNewAccToList (name.text, surname.text, address.text))) {
					title.text="Account already exists.";
				}
			}else{
				title.text="Wrong or Empty Inputs, try again.";
			}
		}

		private bool CheckFields()
		{
			users [0] = name.text;
			users [1] = surname.text;
			users [2] = address.text;

			//check if field is empty
			for (int i = 0; i < 2; i++) {
				if (users[i]=="") {
					return false;
				}
			}
			//address check
			if (!(users[2].Length>=5)) {
				return false;
			} 
			return true;
		}

		private bool addNewAccToList(string putName,string putSurname,string putAddress)
		{
			CustomerAccount newaccount = new CustomerAccount(putName,putSurname,putAddress,0, AccountState.Active,CountID ());
			check = true;
			for (int i = 0; i < myCustomers.CustomerAccountList.Count; i++) {
				if (((myCustomers.CustomerAccountList[i].surname==newaccount.surname)&&(myCustomers.CustomerAccountList[i].name==newaccount.name))||(myCustomers.CustomerAccountList[i].address==newaccount.address)) {
					check = false;
				}
			}

			if (check) {
				myCustomers.CustomerAccountList.Add (newaccount);
				AccountList newaccountlist = new AccountList (myCustomers.CustomerAccountList);
				accList.SaveList(newaccountlist);
				return check;
			} else {
				return check;
			}
		}


		public void InitNewAccount(List<CustomerAccount> mylistofaccounts)
		{
			myCustomers.CustomerAccountList = mylistofaccounts;

		}


		void OnDisable()
		{
			save.onClick.RemoveAllListeners ();
			back.onClick.RemoveAllListeners ();

		}

	}
}