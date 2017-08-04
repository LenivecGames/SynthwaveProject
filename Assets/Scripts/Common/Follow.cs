using UnityEngine;
using System.Collections;

[ExecuteInEditMode, DisallowMultipleComponent]
public class Follow : MonoBehaviour
{
    public GameObject Target;

    public Vector3 PositionOffset;

    public bool FreezePositionX, FreezePositionY, FreezePositionZ;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Target != null)
        {
            /*transform.position.Set(
                FreezePositionX ? 0 : Target.transform.position.x , 
                FreezePositionY ? 0 : Target.transform.position.y ,
                FreezePositionZ ? 0 : Target.transform.position.z 
            );*/

            transform.position = new Vector3(
                FreezePositionX ? transform.position.x : Target.transform.position.x + PositionOffset.x,
                FreezePositionY ? transform.position.y : Target.transform.position.y + PositionOffset.y,
                FreezePositionZ ? transform.position.z : Target.transform.position.z + PositionOffset.z);
        }

    }
}
