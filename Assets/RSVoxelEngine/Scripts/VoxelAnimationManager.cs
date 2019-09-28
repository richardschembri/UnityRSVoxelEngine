namespace RSToolkit.VoxelEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class VoxelAnimationManager : MonoBehaviour {
        public VoxelAnimator[] PreLoadedAnimations;
        List<VoxelAnimator> DynamicAnimations = new List<VoxelAnimator>();
        
        // Use this for initialization
        void Start () {
            
        }
        
        // Update is called once per frame
        void Update () {
            
        }

        public void Animate(string animationName)
        {
            if (DynamicAnimations.Any(a => a.IsAnimating && a.WaitUntilAnimEnd && !a.IsAnimEnd))
            {
                return;
            }

            var animationObject = GameObject.Find(animationName);
            if (animationObject == null)
            {

                //Implement Null error handling
                animationObject = (GameObject)Instantiate(Resources.Load(animationName));
                
                animationObject.name = animationName;
                animationObject.transform.SetParent(this.transform);
                animationObject.transform.localScale = new Vector3(1, 1, 1);
                animationObject.GetComponent<VoxelAnimator>().SetToInitialPosition();
            }

            var va = animationObject.GetComponent<VoxelAnimator>();

            if (!DynamicAnimations.Contains(va))
            {
                DynamicAnimations.Add(va);
            }

            Animate(va);

            for (int i = 0; i < DynamicAnimations.Count(); i++)
            {
                var da = DynamicAnimations[i];
                if (da != va)
                {
                    DynamicAnimations.Remove(da);
                    GameObject.Destroy(da.gameObject);
                }
            }
        }

        public void Animate(VoxelAnimator animation)
        {
            if(animation == null)
            {
                return;
            }

            if (animation.gameObject.activeSelf)
            {
                if (animation.IsAnimating)
                {
                    return;
                }else 
                {
                    animation.Animate();
                    return;
                }
            }

            if(PreLoadedAnimations.Any(a => a.IsAnimating && a.WaitUntilAnimEnd && !a.IsAnimEnd)){
            return;
            }

            for (int i = 0; i < PreLoadedAnimations.Length; i++)
            {
                PreLoadedAnimations[i].CloseAnimation();
            }

            animation.gameObject.SetActive(true);
            animation.Animate();

    
            for (int i = 0; i < DynamicAnimations.Count(); i++)
            {
                var da = DynamicAnimations[i];
                if (da != animation)
                {
                    DynamicAnimations.Remove(da);
                    GameObject.Destroy(da.gameObject);
                }
            }


        }
    }
}