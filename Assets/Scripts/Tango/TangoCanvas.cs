using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TangoCanvas : MonoBehaviour {
	public GameObject ColorM;
	public GameObject ToolC;
	public GameObject BrushM;
	public GameObject SettingsM;
    public GameObject onScreenController;
    public GameObject GVRVisual;
    public GameObject GVRLaser;
    public bool isMenuHidden = false;
    
    private BrushManager brushScript;
    private PinchDraw pinchScript;
    private dLineManager dLineScript;
    private Canvas entireCanvas;

	// Use this for initialization
	void Start () {
        brushScript = (BrushManager) BrushM.GetComponentInParent(typeof(BrushManager));
	    pinchScript = (PinchDraw) onScreenController.GetComponent(typeof(PinchDraw));
        dLineScript = (dLineManager) onScreenController.GetComponent(typeof(dLineManager));
        entireCanvas = (Canvas)GetComponent(typeof(Canvas));
        
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Brushson();
        // Note this is a hack because I couldn't figure out where the code was reseting this transform elsewhere
        //onScreenController.transform.parent.position = new Vector3(0,-1,0);



		StartCoroutine (colorStart ());
	
	
	
	
	
	}
    void Update ()
    {
        //if (GvrControllerInputDevice.GetButtonDown(GvrControllerButton.App))
        if (GvrControllerInput.AppButtonDown)
        {
            print("Click App button down");
            toggleEntireCanvas();
        }

        // Note that we're only going to be painting if the menu is hidden
        if (isMenuHidden && GvrControllerInput.ClickButtonDown)
        { 
            print("Start click Touchpad");
            brushScript.PaintingStart();
            pinchScript.paintStart();
            dLineScript.painterStart();
        }
        // Note that we're only going to be painting if the menu is hidden
        if (isMenuHidden && GvrControllerInput.ClickButtonUp)
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
        // We just want to reload the current scene to clear the artwork
        Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (currentScene.name);




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
        
        yield return new WaitForSeconds (.25f);
        toggleEntireCanvas();
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
        BrushM.SetActive (false);
        SettingsM.SetActive (false);
        ToolC.SetActive (false);
        ColorM.SetActive (false);
	} 
    public void toggleEntireCanvas(){
        if (isMenuHidden)
        {
            entireCanvas.enabled = true;
            // When the menu is shown, hide the painter and show the laser
            onScreenController.SetActive(false);
            GVRLaser.SetActive(true);
            //GVRVisual.SetActive(true);
            isMenuHidden = false;
        }
        else
        {
            entireCanvas.enabled = false;
            // When the menu is gone, show the painter and hide the laser
            onScreenController.SetActive(true);
            GVRLaser.SetActive(false);
            //GVRVisual.SetActive(false);
            isMenuHidden = true;
        }
    }


}
