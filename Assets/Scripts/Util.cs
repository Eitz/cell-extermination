using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

	public static void CheckForGameOver() {
		print ("Diseases yet: " + Disease.diseases);
		if (Disease.diseases <= 0) {
			int qtdDiseasesAvailable = 0;
			foreach (DiseaseButton dButton in DiseaseButton.buttons) {
				qtdDiseasesAvailable += dButton.GetUnits ();
			}
			print ("Buttons yet: " + qtdDiseasesAvailable);
			if (qtdDiseasesAvailable == 0) {
                WinPanel.Lose();
			}
		}
	}
}
