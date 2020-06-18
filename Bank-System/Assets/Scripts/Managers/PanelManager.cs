using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cheese.managers{
	public class PanelManager : MonoBehaviour {


		[SerializeField] private GameObject startPanel;
		[SerializeField] private GameObject createOrFindAccPanel;
		[SerializeField] private GameObject newAccPanel;
		[SerializeField] private GameObject findAccPanel;
		[SerializeField] private GameObject actionPanel;
		//[SerializeField] private GameObject userGraphs;

		void Awake()
		{
			createOrFindAccPanel.SetActive (false);
			newAccPanel.SetActive (false);
			findAccPanel.SetActive (false);
			startPanel.SetActive (true);				
			actionPanel.SetActive (false);
			//userGraphs.SetActive (false);

		}

		public void SwitchPanel(float panelActive)
		{
			if (panelActive==2) {
				
				createOrFindAccPanel.SetActive (true);
				newAccPanel.SetActive (false);
				findAccPanel.SetActive (false);
				startPanel.SetActive (false);
				actionPanel.SetActive (false);
				//userGraphs.SetActive (false);

			}else if (panelActive==3) {
				
				createOrFindAccPanel.SetActive (false);
				newAccPanel.SetActive (true);
				findAccPanel.SetActive (false);
				startPanel.SetActive (false);
				actionPanel.SetActive (false);
				//userGraphs.SetActive (false);

			}else if (panelActive==4) {
				
				createOrFindAccPanel.SetActive (false);
				newAccPanel.SetActive (false);
				findAccPanel.SetActive (true);
				startPanel.SetActive (false);
				actionPanel.SetActive (false);
				//userGraphs.SetActive (false);

			}else if (panelActive==1) {
				
				createOrFindAccPanel.SetActive (false);
				newAccPanel.SetActive (false);
				findAccPanel.SetActive (false);
				startPanel.SetActive (true);
				actionPanel.SetActive (false);
				//userGraphs.SetActive (false);

			}
			else if (panelActive==5) {
				createOrFindAccPanel.SetActive (false);
				newAccPanel.SetActive (false);
				findAccPanel.SetActive (true);
				startPanel.SetActive (false);
				//userGraphs.SetActive (false);
				actionPanel.SetActive (true);
			}
			else if (panelActive==2.5) {
				createOrFindAccPanel.SetActive (false);
				newAccPanel.SetActive (false);
				findAccPanel.SetActive (false);
				startPanel.SetActive (false);
				//userGraphs.SetActive (true);
				actionPanel.SetActive (false);
			}

		}


	}
}