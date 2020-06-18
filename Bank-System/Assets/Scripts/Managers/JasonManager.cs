using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using Cheese.data;

namespace Cheese.managers{
	public class JasonManager : MonoBehaviour {

		private static readonly fsSerializer _serializer = new fsSerializer();

		//save
		public static string SetAccountList(AccountList myacc) {
			fsData data;
			_serializer.TrySerialize (typeof(AccountList), myacc, out data).AssertSuccess ();
			return fsJsonPrinter.CompressedJson(data);
		}

		//load
		public static AccountList GetAccountList(string jsontext)
		{
			fsData data = fsJsonParser.Parse (jsontext);

			AccountList deserialized = null;
			_serializer.TryDeserialize<AccountList>(data, ref deserialized).AssertSuccessWithoutWarnings ();

			return deserialized;

		}
	}
}