using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    /// <summary>
    /// Main script contains information about UI and game states
    /// </summary>
    public class FillMe : MonoBehaviour
    {
        
        //Links to game objects representing UI elements
        [Header("Set In Inspector")]
        public GameObject replayButton;
        public GameObject nextLevelButton;
        public Text scoreText;
        public Text finalScore;
        public Text nextLevelScore;
        public ScoreSO scoreScrObj;
        
        //List that contains all Glasses game objects on level
        public List<Glass> glassesOnLevel = new List<Glass>();

        //Float value that represent one step on progress bar (can't be greater than 1)
        private float progressBarStep;
        [Header("Set Dynamically")] public bool levelIsBeated = false;

        //Singelton instance used in WaterOrigin to control (stop) water flow after game is over
        private static FillMe _S;

        private void Awake()
        {
            _S = this;
        }

        private void Start()
        {
            levelIsBeated = false;
            ProgressBar.S.ClearProgressOnSlider();
            
            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag:"Glass"))
            {
                glassesOnLevel.Add(go.GetComponentInChildren<Glass>());
            }
            
            //Callbacks from main game object
            //Glass calls when water hits glass collider
            Glass.OnWaterCollided += GlassOnOnWaterCollided;
            //BeltConveyor calls when water collides with it (lose state)
            BeltConveyor.OnWaterTouchedGround += BeltConveyorOnOnWaterTouchedGround;

            // ReSharper disable once PossibleLossOfFraction
            progressBarStep = (1f / (float)glassesOnLevel.Count);


        }

        private void Update()
        {
            foreach (Glass glass in glassesOnLevel)
            {
                if (glass.transform.position.x > 3 && glass.isFilled == false)
                {
                    LoseGame();
                }
            }
            
            scoreText.text = "Score: " + scoreScrObj.score;
        }

        private void BeltConveyorOnOnWaterTouchedGround()
        {
            LoseGame();
        }

        private void GlassOnOnWaterCollided()
        {
            //Adds 1 point to score
           scoreScrObj.AddScore(1);
           
           //Slider on ProgressBar increments
           ProgressBar.S.IncrementProgress(progressBarStep);
           
           //Checking for true on every Glass object on level on isFilled param, that detects level completion
            bool levelCompleted = glassesOnLevel.All(g => g.isFilled == true);
            //If level completed
            if (levelCompleted)
            {
                StartCoroutine(LevelBeatedCheckRoutine());
            }
            else
            {
                print("Game still in progress");
            }

        }

        public void LoseGame()
        {
            levelIsBeated = true;
            replayButton.SetActive(true);
            finalScore.text = "Final Score: " + scoreScrObj.score;

        }
        
        public void LevelBeated()
        {
            levelIsBeated = true;
            nextLevelButton.SetActive(true);
            nextLevelScore.text = "Score: " + scoreScrObj.score;
        }
        
        
        private void OnDestroy()
        {
            Glass.OnWaterCollided -= GlassOnOnWaterCollided;
            BeltConveyor.OnWaterTouchedGround -= BeltConveyorOnOnWaterTouchedGround;
        }

        private IEnumerator LevelBeatedCheckRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            if (!levelIsBeated)
            {
                LevelBeated();
            }
        }
        
        //---------------Buttons-----------------\\
        public void OnButtonReplayPressed()
        {
            replayButton.SetActive(false);
            scoreScrObj.ResetScore();
            SceneManager.LoadScene(0);
        }
        public void OnButtonNExtLevelPressed()
        {
            nextLevelButton.SetActive(false);
            SceneManager.LoadScene(0);

        }
        
        //------------------Statics---------------\\

        public static FillMe S
        {
            get => _S;
            set
            {
                if (_S == null) _S = value;
            }
        }
    }
    
    
}