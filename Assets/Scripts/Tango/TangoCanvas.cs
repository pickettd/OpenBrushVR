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
    public GameObject mainGVRController;
    public GameObject secondGVRController;
    
    private BrushManager brushScript;
    private PinchDraw pinchScript;
    private dLineManager dLineScript;
    private Canvas entireCanvas;
    private GvrTrackedController mainGVRtrackedController;
#if UNITY_ANDROID || UNITY_IOS
	// Use this for initialization
	void Start () {
        brushScript = (BrushManager) BrushM.GetComponentInParent(typeof(BrushManager));
	    pinchScript = (PinchDraw) onScreenController.GetComponent(typeof(PinchDraw));
        dLineScript = (dLineManager) onScreenController.GetComponent(typeof(dLineManager));
        entireCanvas = (Canvas)GetComponent(typeof(Canvas));
        mainGVRtrackedController = (GvrTrackedController)mainGVRController.GetComponent(typeof(GvrTrackedController));
        
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Brushson();



		StartCoroutine (colorStart ());
	
	
	
	
	
	}
    void Update ()
    {
        GvrControllerButton paintingButtons = GvrControllerButton.TouchPadButton | GvrControllerButton.Trigger | GvrControllerButton.Grip;
        //if (GvrControllerInputDevice.GetButtonDown(GvrControllerButton.App))
        if (GvrControllerInput.AppButtonDown)
        {
            print("Click App button down");
            toggleEntireCanvas();
        }

        // Note that we're only going to be painting if the menu is hidden
        if (isMenuHidden && mainGVRtrackedController.ControllerInputDevice.GetButtonDown(paintingButtons))
        { 
            print("Start click Touchpad");
            brushScript.PaintingStart();
            pinchScript.paintStart();
            dLineScript.painterStart();
        }
        if (mainGVRtrackedController.ControllerInputDevice.GetButtonDown(paintingButtons)) {
            Debug.Log("got it");
        }
        // Note that we're only going to be painting if the menu is hidden
        if (isMenuHidden && mainGVRtrackedController.ControllerInputDevice.GetButtonUp(paintingButtons))
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
            // If the second controller is not active, we do want to show the big menu
            // But we won't show the big menu in case we have two controllers (because then second hand is the palette hand)
            if (!secondGVRController.activeInHierarchy)
            {
                entireCanvas.enabled = true;
            }
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

#endif
}
