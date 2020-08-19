using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class WaterOrigin : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule emission;
    private float bSpeed;
    

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        emission = ps.emission;
        emission.enabled = false;

        bSpeed = BeltConveyor.S.speed;
    }

    private void Update()
    {
        
#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartWaterFlow();
            return;
        }

        EndWaterFlow();

#endif
        
        foreach (Touch touch in Input.touches)
        {
            //If finger is touching screen
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                StartWaterFlow();
            }
            //If touch is over
            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                EndWaterFlow();
            }
        }
    }



    private void StartWaterFlow()
    {
        //Save from emit water when level is already beaten
        if(FillMe.S.levelIsBeated) return;
        
        emission.enabled = true;
        
        //Make conveyor stop 
        BeltConveyor.S.speed = 0;
    }

    private void EndWaterFlow()
    {
        emission.enabled = false;
        
        //Make conveyor move again
        BeltConveyor.S.speed = bSpeed;
    }
}
