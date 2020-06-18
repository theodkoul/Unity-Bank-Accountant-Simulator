using System.Collections;
using System.Collections.Generic;

namespace Cheese.data
{
	[System.Serializable]
	public class AccountList
	{
		public List<CustomerAccount> CustomerAccountList;

		public AccountList(List<CustomerAccount> newlist)
		{
			CustomerAccountList = newlist;
		}

		public AccountList()
		{
			CustomerAccountList = null;
		}
	}
}
