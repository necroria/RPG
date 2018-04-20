using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInPlay : MonoBehaviour {
    public Transform playerTr;
    public Transform player;
	// Use this for initialization
	void Start () {
        transform.position = playerTr.position;
        transform.rotation = playerTr.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.position = Vector3.Lerp(transform.position, player.position+ playerTr.localPosition,3);
        
    }
    
}
