using UnityEngine;
using System.Collections;

public interface IState
{
    void Update(Enemy enemy);
}

public class GroundedState: MonoBehaviour, IState
{

    void IState.Update(Enemy enemy)
    {
        foreach (Collider2D c in frontHits)
        {
            Debug.Log(grounded);
            if (c.tag == "Coin")
            {
                Flip();
                break;
            }
        }
    }
}

public class NotGroundedState : MonoBehaviour, IState
{
    public void Update(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }


    public void FixedUpdate()
    {

    }

}

