using UnityEngine;
using UnityEngine.UI;  // ✅ Needed for Text

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour {

    static private MissionDemolition S; // a private Singleton

    [Header("Inscribed")]
    public Text uitLevel;   // The UIText_Level Text
    public Text uitShots;   // The UIText_Shots Text
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Awake() {
        S = this;  //  
        levelMax = castles.Length; // optional: auto-set max levels
        StartLevel();
    }

    void StartLevel() {
        // destroy old castle if exists
        if (castle != null) {
            Destroy(castle);
        }

        
        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;

        shotsTaken = 0;

        UpdateGUI();

        mode = GameMode.playing;

        
        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }

    void UpdateGUI() {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    void Update() {
        if ((mode == GameMode.playing) && Goal.goalMet) {
            mode = GameMode.levelEnd;

            //  Correct singleton call
            FollowCam.SWITCH_VIEW(FollowCam.eView.both);

            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    // Static method that allows code anywhere to increment shotsTaken
    static public void SHOT_FIRED() {
        S.shotsTaken++;
        S.UpdateGUI(); // 
    }

    // Static method that allows code anywhere to get a reference to S.castle
    static public GameObject GET_CASTLE() {
        return S.castle;
    }
}
