using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TangoCanvas : MonoBehaviour {
	public GameObject ColorM;
	public GameObject ToolC;
	public GameObject BrushM;
	public GameObject SettingsM;
    public GameObject onScreenController;
    public bool isHidden = false;
    
    private BrushManager brushScript;
    private PinchDraw pinchScript;
    private dLineManager dLineScript;

	// Use this for initialization
	void Start () {
        brushScript = (BrushManager) BrushM.GetComponentInParent(typeof(BrushManager));
	    pinchScript = (PinchDraw) onScreenController.GetComponent(typeof(PinchDraw));
        dLineScript = (dLineManager) onScreenController.GetComponent(typeof(dLineManager));
        
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Brushson();



		StartCoroutine (colorStart ());
	
	
	
	
	
	}
    void Update ()
    {
        //if (GvrControllerInputDevice.GetButtonDown(GvrControllerButton.App))
        if (GvrControllerInput.AppButtonDown)
        {
            print("Click App button down");
            hideAll();
        }

        if (GvrControllerInput.ClickButtonDown)
        { 
            print("Start click Touchpad");
            brushScript.PaintingStart();
            pinchScript.paintStart();
            dLineScript.painterStart();
        }
        if (GvrControllerInput.ClickButtonUp)
        { 
            print("End click Touchpad");
            brushScript.PaintingEnd();
            pinchScript.paintEnd();
            dLineScript.painterEnd();
        }

        /*if (GvrControllerInput.IsTouching)
        {
            print("Position Vector: " + GvrControllerInput.TouchPos);
        }*/
    }
	public void motionLevelLoad(){


		SceneManager.LoadScene ("OpenBrush_Tango");




	}

	public void openbrushload(){
		SceneManager.LoadScene ("OpenBrush_Tango_Augmented_Reality");


	}
	public IEnumerator colorStart(){
		//turn on color selection at artart
		yield return new WaitForSeconds (.25f);

		settingon ();
		yield return new WaitForSeconds (.25f);
		Toolson ();

		yield return new WaitForSeconds (.25f);
		coloron ();
	}

	public void settingon(){
		SettingsM.SetActive (true);
		ToolC.SetActive (false);
		BrushM.SetActive (false);
		ColorM.SetActive (false);
	} 

	public void Toolson(){
	ToolC.SetActive (true);
		SettingsM.SetActive (false);
		BrushM.SetActive (false);
		ColorM.SetActive (false);
	} 
	public void Brushson(){
		BrushM.SetActive (true);
		SettingsM.SetActive (false);
		ToolC.SetActive (false);
		ColorM.SetActive (false);
	} 
	public void coloron(){
		ColorM.SetActive (true);
		SettingsM.SetActive (false);
		BrushM.SetActive (false);
		ToolC.SetActive (false);
	}
	public void hideAll(){
        if (isHidden)
        {
            BrushM.SetActive (true);
            SettingsM.SetActive (false);
            ToolC.SetActive (false);
            ColorM.SetActive (false);
            isHidden = false;
        }
        else
        {
            ColorM.SetActive(false);
            SettingsM.SetActive(false);
            BrushM.SetActive(false);
            ToolC.SetActive(false);
            isHidden = true;
        }
	} 



}
