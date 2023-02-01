using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioStepper : MonoBehaviour
{
    // Start is called before the first frame update
    public bool InitPlayOnStart = true;
    void Start()
    {
        if(InitPlayOnStart)
            SetNextStep(0); // выполняем первый шаг в плее

        // раздадим номера в старте
        for (int i = 0; i < ScenarioList.Count; i++)
        {
            ScenarioList[i].StepScenario.SendMessage("SetStp", i);
        }
    }

    public int step = -1;
    public void SetNextStep(int ofset)
    {

        step =1+ step + ofset;
        Debug.Log(ScenarioList[step].StepScenario.GetComponent<PlayScenarioOneStep>().CurStepNum + " Начало нового степа в сценарии " + step);

            ScenarioList[step].StepScenario.SendMessage("StandardScenarioFunc", ScenarioList[step].name);
            //ScenarioList[step].StepScenario.SendMessage("SetStp", step);
            //ScenarioList[step].StepScenario.SendMessage("" + ScenarioList[step].name, ScenarioList[step].dataToSend);
            if(step > ScenarioList.Count-2)
                 Debug.Log("Весь блок сценария пройден начните новый блок");
        
        }

    public void laserP(GameObject selLas) 
    {
        if (selLas.GetComponent<PlayScenarioOneStep>().CurStepNum == step)
        {

            selLas.GetComponent<PlayScenarioOneStep>().Next = true;
            SetNextStep(0); // следующий
        }
    }

    public List<DropStep> ScenarioList = new List<DropStep>();
}
[System.Serializable]
public class DropStep
{
    public string name; // имя функции что мы там выполняем
    public GameObject StepScenario; //выполнить шаг сценария
                                    // тут параметры которые мы передаем
  // public string dataToSend;

}