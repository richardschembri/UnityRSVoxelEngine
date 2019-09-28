using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSToolkit.VoxelEngine;

public class TikiAnimator : VoxelAnimationManager {
    private VoxelAnimationManager vam;
    public bool EnableTestControls = false; 
    // Use this for initialization
    void Start () {
        vam = this.gameObject.GetComponent<VoxelAnimationManager>();

        var lstAnimations = new List<VoxelAnimator>();
        
        vam.PreLoadedAnimations = lstAnimations.ToArray();
    }

    void AddToAnimationList(List<VoxelAnimator> lstAnimations, VoxelAnimator anim)
    {
        if(anim != null)
        {
            lstAnimations.Add(anim);
        }
    }

    public void Animate_Idle(){
        vam.Animate("TikiStand");
    }
    public void Animate_Walk(){
        vam.Animate("TikiWalk");
    }

    public void Animate_Jump(){
        vam.Animate("TikiJump");
    }

    public void Animate_InAir(){
        vam.Animate("TikiInAir");
    }

    public void Animate_Land(){
        vam.Animate("TikiLand");
    }
	
    public void Animate_PickUp(){
        vam.Animate("TikiPickUp");
    }

    public void Animate_CarryWalk(){
        vam.Animate("TikiCarryWalk");
    }
	// Update is called once per frame
	void Update () {
        if(!EnableTestControls){
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Animate_Walk();
        }
        else if (Input.GetKey(KeyCode.Space))
        { 
            Animate_Jump();
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            Animate_InAir();
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            Animate_Land();
        }
        else if(Input.GetKey(KeyCode.RightArrow)){
            Animate_PickUp();
        }
        else if(Input.GetKey(KeyCode.S)){
            Animate_CarryWalk();
        }
        else 
        {
            Animate_Idle();
        }
	}
}
