using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public bool OnGoal = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            this.OnGoal = true;
        }
    }

    private void OnTriggerEnterExits(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            this.OnGoal = false;
        }
    }
}
