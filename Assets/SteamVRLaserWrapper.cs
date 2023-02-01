using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Valve.VR.Extras;

public class SteamVRLaserWrapper : MonoBehaviour
{
    private SteamVR_LaserPointer steamVrLaserPointer;

    private void Awake()
    {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick; 
        SS = GameObject.Find("ScenarioStepper").GetComponent<ScenarioStepper>();
    }
    public UnityEvent LaserClick; ScenarioStepper SS;
    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log("OnPointerClick" + e.target.gameObject.name);
        SS.laserP(e.target.gameObject);
        LaserClick?.Invoke(); // 
        IPointerClickHandler clickHandler = e.target.GetComponent<IPointerClickHandler>();
        if (clickHandler == null)
        {
            return;
        }


        clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
       // Debug.Log("OnPointerOut" + e.target.gameObject.name);
        IPointerExitHandler pointerExitHandler = e.target.GetComponent<IPointerExitHandler>();
        if (pointerExitHandler == null)
        {
            return;
        }

        pointerExitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
       // Debug.Log("OnPointerIn" + e.target.gameObject.name);
        IPointerEnterHandler pointerEnterHandler = e.target.GetComponent<IPointerEnterHandler>();
        if (pointerEnterHandler == null)
        {
            return;
        }

        pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
    }
}