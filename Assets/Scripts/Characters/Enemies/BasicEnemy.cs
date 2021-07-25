using UnityEngine;
public class BasicEnemy : Enemy
{

    [ContextMenu("Trigger Death")]
    public void Hit()
    {
        Death();
    }
}