using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReplacementShader : MonoBehaviour
{
    public Shader replacementShader;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (replacementShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(replacementShader, "RenderType");
        }
    }

    private void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
}
