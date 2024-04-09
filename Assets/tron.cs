using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tron : MonoBehaviour
{

    public SkinnedMeshRenderer skinnedMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        NormalizeBlendShapeValues();
    }

    void NormalizeBlendShapeValues()
    {
        if (skinnedMeshRenderer == null)
        {
            Debug.LogWarning("SkinnedMeshRenderer not found on GameObject.");
            return;
        }

        int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < blendShapeCount; i++)
        {
            int frameCount = skinnedMeshRenderer.sharedMesh.GetBlendShapeFrameCount(i);
            for (int j = 0; j < frameCount; j++)
            {
                float frameWeight = skinnedMeshRenderer.sharedMesh.GetBlendShapeFrameWeight(i, j);
                skinnedMeshRenderer.SetBlendShapeWeight(i, frameWeight / 100f);
            }
        }

    }
}



