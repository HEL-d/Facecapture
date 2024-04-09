using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removew : MonoBehaviour
{

    Mesh mesh;
     SkinnedMeshRenderer skinnedMeshRenderer;


    void Start()
    {
      skinnedMeshRenderer = this.GetComponentInChildren<SkinnedMeshRenderer>();
        mesh = skinnedMeshRenderer.sharedMesh;



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {

            for (int i = 0; i < mesh.blendShapeCount; i++)
                skinnedMeshRenderer.SetBlendShapeWeight(i, 0);

            
         /*  skinnedMeshRenderer.SetBlendShapeWeight(0, mesh.GetBlendShapeIndex("mouthOpen"));
          // skinnedMeshRenderer.SetBlendShapeWeight(0, mesh.GetBlendShapeIndex("viseme_PP")); */ 
        }




    }
}
