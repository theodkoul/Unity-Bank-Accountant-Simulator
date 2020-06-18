using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Cheese.managers
{
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private AudioSource efxSource;
		[SerializeField] private AudioClip buttonClick;

		public void PlayClickSound ()
		{
			efxSource.clip = buttonClick;
			efxSource.Play ();
		}
			
	
	}
}