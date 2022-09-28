using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] int count;
    private int childCountMax;
    private int childCounter;
    [SerializeField] float movementX;
    [SerializeField] float movementZ;
    [SerializeField] float movementSpeed = 2f;

    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI remainingText;
    [SerializeField] GameObject winTextUIObject;

    [SerializeField] GameObject collectableParent;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        childCounter = childCountMax = collectableParent.transform.childCount;
        UpdateRemainingCount();
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(movementX, 0f, movementZ);
        rb.AddForce(move*movementSpeed);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            childCounter--;
            UpdateScoreText();
            UpdateRemainingCount();
        }
    }

    private void UpdateRemainingCount()
    {
        remainingText.text ="Remaining: "+ childCounter + "/" + childCountMax;
        if (childCounter <= 0)
        {
            winTextUIObject.SetActive(true);
        }
    }

    private void UpdateScoreText()
    {
        countText.text = "Count: "+ count.ToString();
    }
}
