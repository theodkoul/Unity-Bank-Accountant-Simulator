using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cheese.managers;
using Cheese.data;
using Cheese.controller;

namespace Cheese.panelUI{
	public class FindAccountPanel : MonoBehaviour {

		[SerializeField] private Text title;
		[SerializeField] private Button withdraw;
		[SerializeField] private Button deposit;
		[SerializeField] private Button save;
		[SerializeField] private Button back;

		//account details

		[SerializeField] private InputField name;
		[SerializeField] private InputField surName;
		[SerializeField] private InputField address;
		[SerializeField] private InputField balance;
		[SerializeField] private InputField accountID;
		[SerializeField] private Dropdown accountState;

		[SerializeField] private Text nameTitle;
		[SerializeField] private Text surNameTitle;
		[SerializeField] private Text addressTitle;
		[SerializeField] private Text balanceTitle;
		[SerializeField] private Text accountStateTitle;
		[SerializeField] private Text accountIDTitle;

		//instances
		[SerializeField] private SoundManager soundManagerInstance;
		[SerializeField] private GameObject accountDetailsContainer;
		[SerializeField] private PanelManager panelManager;
		[SerializeField] private AccountListController accListCont;
		[SerializeField] private ActionPanel actionPanel;

		private AccountList accounts = new AccountList();

		List<Button> buttons;
		Button button;

		[SerializeField] private GameObject accountButtonContainer;
		[SerializeField] private Button accountButton;

		private int index = 0;
		private int buttonInt;
		private string buttonText = "";

		private int check = 0;


		//search engine
		[SerializeField] private Text errorText;
		[SerializeField] private Button search;
		[SerializeField] private InputField searchInput;
		[SerializeField] private Text searchTitle;
		private bool searchFoundSomething;

		void Awake()
		{

			SetActives (false);
			save.gameObject.SetActive (false);

			name.placeholder.GetComponent<Text>().text="";
			surName.placeholder.GetComponent<Text>().text="";
			balance.placeholder.GetComponent<Text>().text="";
			address.placeholder.GetComponent<Text>().text="";
			accountID.placeholder.GetComponent<Text>().text="";

			name.readOnly = true;
			surName.readOnly = true;
			accountID.readOnly = true;
			balance.readOnly = true;

			back.gameObject.SetActive (true);

			title.text = "Account List ";
			save.GetComponentInChildren<Text>().text="Save";
			withdraw.GetComponentInChildren<Text>().text="Withdraw";
			deposit.GetComponentInChildren<Text>().text="Deposit";
			back.GetComponentInChildren<Text> ().text = "Go Back";

			nameTitle.text = "Name";
			surNameTitle.text="Surname";
			balanceTitle.text="Balance";
			accountIDTitle.text = "Account ID";
			accountStateTitle.text="Account State";
			addressTitle.text="Address";

			searchTitle.text="Search:";
			search.GetComponentInChildren<Text> ().text="GO";
			errorText.text="Nothing Found!";
			errorText.gameObject.SetActive (false);

		}

		void OnEnable()
		{
			back.onClick.AddListener (()=>{				
				soundManagerInstance.PlayClickSound();
				panelManager.SwitchPanel (2);
			});

			accountState.onValueChanged.AddListener (delegate {
				save.interactable=true;
			});

			search.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();
				SearchFunction(searchInput.text);
			});
		}

		// change account display properties
		public void ChangeAccount(int i)
		{
			SetActives (true);
			name.text = accounts.CustomerAccountList[i].name;
			surName.text = accounts.CustomerAccountList [i].surname;
			address.text = accounts.CustomerAccountList [i].address;
			balance.text = ""+accounts.CustomerAccountList [i].balance;
			accountID.text = accounts.CustomerAccountList [i].accountID;

			if (accounts.CustomerAccountList [i].state == AccountState.Active) {
				accountState.value = 0;
			}
			else if (accounts.CustomerAccountList[i].state==AccountState.Frozen) {
				accountState.value = 2;
			}
			else if(accounts.CustomerAccountList[i].state==AccountState.Closed) {
				accountState.value = 1;
			}

		}

		private void SetActives(bool check)
		{
			accountDetailsContainer.SetActive (check);

			save.gameObject.SetActive (check);
			withdraw.gameObject.SetActive (check);
			deposit.gameObject.SetActive (check);

		}

		public void ChangeInteractable(bool check)
		{
			name.interactable = check;
			surName.interactable = check;
			address.interactable = check;
			accountID.interactable = check;
			accountState.interactable = check;
			balance.interactable = check;

			withdraw.interactable = check;
			deposit.interactable = check;

		}

		public void InitFindAccount(AccountList myacc)
		{

			if (myacc.CustomerAccountList.Count!=0) {
				if (index == 0) {
					buttons = new List<Button> ();
				}
				
				if (buttons.Count > 0) {
					for (int i = 0; i < buttons.Count; i++) {
						Destroy (buttons [i].gameObject);
						index = 0;
					}
					buttons.Clear ();
				}

				for (int i = 0; i < myacc.CustomerAccountList.Count; i++) {
					button = Instantiate (accountButton, accountButtonContainer.transform) as Button;
					buttonText = "Surname: " + myacc.CustomerAccountList [i].surname +
					"\nName: " + myacc.CustomerAccountList [i].name + "\nAddress: " + myacc.CustomerAccountList [i].address 
						+ "\nAccount ID: " + myacc.CustomerAccountList[i].accountID;
					button.GetComponentInChildren<Text> ().text = buttonText;
					index++;
					buttons.Add (button);
					Button b = button.GetComponent<Button> ();
					ButtonAddListener (myacc, b);
				}
				accounts.CustomerAccountList = myacc.CustomerAccountList;
			}


		}

		private void ButtonAddListener(AccountList mylist,Button b)
		{
			b.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();

				for (int i= 0; i < buttons.Count; i++) {
					if (buttons[i]==b) {
						SetActives (true);
						buttonInt=i;
						ChangeAccount (buttonInt);
						break;
					}
				}
				DoActions (mylist,buttonInt);
			});




		}

		private void SearchFunction(string text)
		{
			for (int i = 0; i < accounts.CustomerAccountList.Count; i++) {
				if ((text==accounts.CustomerAccountList[i].surname)||(text==accounts.CustomerAccountList[i].accountID)) {
					SetActives (true);
					ChangeAccount (i);
					searchFoundSomething = true;
					errorText.gameObject.SetActive (false);
					buttonInt = i;
					break;
				}
			}
			if (searchFoundSomething) {
				DoActions (accounts, buttonInt);
			}else{
				errorText.gameObject.SetActive (true);
			}

		}

		private void DoActions(AccountList mylist, int myint)
		{

			withdraw.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();
				panelManager.SwitchPanel (5);
				ChangeInteractable(false);

				actionPanel.setAccountElements (mylist, myint,true);
			});
			deposit.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();
				panelManager.SwitchPanel (5);
				ChangeInteractable(false);

				actionPanel.setAccountElements (mylist, myint,false);
			});

			save.onClick.AddListener (()=>{
				soundManagerInstance.PlayClickSound();
				accounts.CustomerAccountList[myint].address=address.text;
				if (accountState.value == 0) {
					accounts.CustomerAccountList[myint].state=AccountState.Active;
				}
				else if (accountState.value == 1) {
					accounts.CustomerAccountList[myint].state=AccountState.Closed;
				}
				else if (accountState.value == 2) {
					accounts.CustomerAccountList[myint].state=AccountState.Frozen;
				}
				AccountList newacclist = new AccountList(accounts.CustomerAccountList);
				accListCont.ChangeSavedData (newacclist);
				save.interactable=false;
			});
		}


		void OnDisable()
		{
			back.onClick.RemoveAllListeners ();
			search.onClick.RemoveAllListeners ();
		}

	}
}