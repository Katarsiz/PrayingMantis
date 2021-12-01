using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxPlus : MonoBehaviour {

    public GameObject[] availablePrefabs;
    
    public int prefabsToGenerate;

    public float maxSpeed;

    public float currentSpeed;

    /// <summary>
    /// Determines in which direction the backgrounds will move
    /// </summary>
    public float movingFactor;

    public List<GameObject> generatedBackgrounds;
    
    public enum MovingPlane {
        Horizontal,
        Vertical
    }

    public MovingPlane plane;


    public Camera camera;

    private Vector3 prefabDimensions;

    private Vector2 cameraDimensions;

    private Vector3 movementDirection;

    private float spriteWidth;

    // Start is called before the first frame update
    void Start() {
        // Direction get
        if (plane == MovingPlane.Horizontal) {
            movementDirection = Vector2.right;
        }
        else {
            movementDirection = Vector2.up;
        }
        // Background prefabs generation
        prefabDimensions = availablePrefabs[0].GetComponent<SpriteRenderer>().bounds.size;
        spriteWidth = prefabDimensions.x;
        generatedBackgrounds = new List<GameObject>();
        for (int i = 0; i < prefabsToGenerate; i++) {
            GameObject newBackground = Instantiate(availablePrefabs[UnityEngine.Random.Range(0,availablePrefabs.Length-1)],
                transform.position + Vector3.Scale(prefabDimensions * i, movementDirection), Quaternion.identity);
            newBackground.transform.parent = transform;
            newBackground.name = "BG" + i;
            generatedBackgrounds.Add(newBackground);
        }
    }

    private void FixedUpdate() {
        // Moving the objects
        generatedBackgrounds.ForEach(x=>x.transform.position += movingFactor * currentSpeed * Time.fixedDeltaTime * movementDirection);
        //
        GameObject rightmostSprite = generatedBackgrounds.Last(), leftmostSprite = generatedBackgrounds[0];
        // If rightSprite is on the border of the camera, it is updated to a new sprite generated.
        if (movingFactor < 0) {
            float rightmostSpriteBorder = rightmostSprite.transform.position.x + spriteWidth/2,
                rightCameraBorder = camera.ScreenToWorldPoint(movementDirection * camera.pixelWidth).x;
            if (rightmostSpriteBorder < rightCameraBorder) {
                leftmostSprite.transform.position = rightmostSprite.transform.position + spriteWidth * movementDirection;
                generatedBackgrounds.Remove(leftmostSprite);
                generatedBackgrounds.Add(leftmostSprite);
            }
        } else if (movingFactor > 0) {
            float leftCameraBorder = camera.ScreenToWorldPoint(Vector3.zero).x,
                leftmostSpriteBorder = leftmostSprite.transform.position.x - spriteWidth/2;
            if (leftmostSpriteBorder > leftCameraBorder) {
                rightmostSprite.transform.position = leftmostSprite.transform.position - spriteWidth * movementDirection;
                generatedBackgrounds.Remove(rightmostSprite);
                generatedBackgrounds.Insert(0,rightmostSprite);
            }
        }
    }
}
