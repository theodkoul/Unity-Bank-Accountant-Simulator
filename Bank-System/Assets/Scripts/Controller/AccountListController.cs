using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cheese.managers;
using Cheese.panelUI;
using System.IO;
using Cheese.data;

namespace Cheese.controller{
	public class AccountListController : MonoBehaviour {

		const string filename1 = "/Recources/myAccountsFile.json";

		private AccountList myAccounts;
		[SerializeField] private FindAccountPanel findAccPanel;
		[SerializeField] private NewAccountPanel newAccPanel;

		//save
		public void SaveList(AccountList accounts)
		{
			string filepath = Application.dataPath + filename1;
			File.WriteAllText (filepath,JasonManager.SetAccountList (accounts));
		}

		//load
		public void LoadList()
		{	
			string filePath = Application.dataPath + filename1;

			if (File.Exists (filePath)) {
				if (File.ReadAllText (filePath).Length!=0) {
					myAccounts = JasonManager.GetAccountList (File.ReadAllText (filePath));
				}else{
					List<CustomerAccount> mylistofcustomers = new List<CustomerAccount> ();
					myAccounts = new AccountList(mylistofcustomers);
				}

				findAccPanel.InitFindAccount (myAccounts);
				newAccPanel.InitNewAccount (myAccounts.CustomerAccountList);
			} 
			else {
				List<CustomerAccount> mylist = new List<CustomerAccount>();
				newAccPanel.InitNewAccount (mylist);
			}
		}


		public void ChangeSavedData(AccountList secondList)
		{
			for (int i = 0; i < myAccounts.CustomerAccountList.Count; i++) {
				for (int j= 0; j < secondList.CustomerAccountList.Count; j++) {
					if (myAccounts.CustomerAccountList[i].name==secondList.CustomerAccountList[j].name) {
						if (myAccounts.CustomerAccountList[i].surname==secondList.CustomerAccountList[j].surname) {
							if (myAccounts.CustomerAccountList[i].address==secondList.CustomerAccountList[j].address) {
								myAccounts.CustomerAccountList [i].balance = secondList.CustomerAccountList [j].balance;
								myAccounts.CustomerAccountList [i].state = secondList.CustomerAccountList [j].state;
								myAccounts.CustomerAccountList [i].address = secondList.CustomerAccountList [j].address;

								SaveList (myAccounts);
								break;
							}
						}
					}
				}
			}
			//end for

		}



	}
}