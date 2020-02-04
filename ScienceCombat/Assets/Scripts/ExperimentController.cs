using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentController : MonoBehaviour{
  //  [SerializeField] private ExperimentManager experimentManager;
   // [SerializeField] private Experiment experiment;

    [SerializeField] private Image experimentProgressBar;
    [SerializeField] private Image progressUI;
    public float currentProgress;
    [SerializeField] private float maxProgress;
    public bool isComplete;
    // Start is called before the first frame update
    void Start() {

        isComplete = false;
        experimentProgressBar.enabled = true;
        progressUI.enabled = true;

    }
    private void Update(){
        ProgressCheck();
    }

    private void ProgressCheck() {

        if (currentProgress < maxProgress) {
            experimentProgressBar.enabled = true;
            progressUI.enabled = true;
            currentProgress += Time.deltaTime;
            experimentProgressBar.fillAmount = currentProgress / maxProgress;
        }
        else {
            isComplete = true;
            experimentProgressBar.enabled = false;
            progressUI.enabled = false;
            this.enabled = false;
        }
    }
}
