using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaStripe : MonoBehaviour
{
    [Header("Stripe info")]
    public float manaAmount = 1.0f;

    public float getMana()
    {
        return manaAmount;
    }

}
