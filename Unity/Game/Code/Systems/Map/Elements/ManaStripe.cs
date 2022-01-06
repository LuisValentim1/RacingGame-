using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaStripe : MonoBehaviour
{
    [Header("Stripe info")]
    public int manaAmountPerUpdate;
    public int maxManaPossible;

    public int getMana()
    {
        return manaAmountPerUpdate;
    }
}