using System;

namespace Cheese.data
{

	//====AccountState====
	public enum AccountState
	{
		Active,
		Frozen,
		Closed
	};
	public abstract class Account
	{
		public AccountState  state ;  
		public string  name ;  
		public string surname;
		public string  address ;  
		public string  accountID ;  
		public float balance;

		public Account(string inName, string inSurname, string inAddress, float inBalance, AccountState inState, string inAccountID)
		{
			
			name = inName;
			surname = inSurname;
			address = inAddress;
			balance = inBalance;
			state = inState;
			accountID = inAccountID;
		}

		public Account():
		this ("not supplied", "not supplied", "not supplied",0, AccountState.Active, "1234567"){}


		public Account(string inName, string inSurname, string inAddress,float inBalance):
		this (inName, inSurname, inAddress, inBalance, AccountState.Active, "1234567"){}


		public virtual bool WithdrawFunds ( float amount )
		{
			if ( balance < amount ){
				return false ;
			}
			balance = balance - amount ;
			return true;
		}

		public virtual void DepositFunds ( float amount ) 
		{
			balance = balance + amount ;
		}

		public float GetBalance ()
		{
			return balance;
		}


	}
}