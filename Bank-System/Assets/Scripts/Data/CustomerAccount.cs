using System;

namespace Cheese.data
{
	public class CustomerAccount:Account
	{

		public CustomerAccount (string inName, string inSurname,string inAddress, float inBalance, AccountState inState, string inAccountID):
		base(inName, inSurname, inAddress, inBalance, inState, inAccountID){}
	
	}
}

