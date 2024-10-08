using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    private float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();
    
    // Start is called before the first frame update
    
    
    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localEulerAngles.y - (goalTransform.localPosition.y - 0.1f);
        LoadStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if(lastTapPos == Vector2.zero)
            {
                lastTapPos = curTapPos;
            }

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null)
        {
            Debug.LogError("No Stage" + stageNumber + " Found in allStages List. Are all stages assigned in the list?");
        }

        //Change color of the background of the satge
        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
        //Change color of the ball
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        // Reset helix rotation
        transform.localEulerAngles = startRotation;

        // destroy old levels
        foreach (GameObject gameObject in spawnedLevels)
        {
            Destroy(gameObject);
        }

        // create new level / platforms

        float levelDistance = helixDistance / stage.level.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.level.Count; i++)
        {
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("Level spawned");
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);

            int partsToDiable = 12 - stage.level[i].partCount;
            List<GameObject> diasabledParts = new List<GameObject>();

            while (diasabledParts.Count < partsToDiable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
            }
        }
    }





}
