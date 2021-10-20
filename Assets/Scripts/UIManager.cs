using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager is NULL!");
            }
            return _instance;
        }
    }

    public Animator anim;
    public int currentAnim;
    public int CurrentAnim
    {
        get { return currentAnim; }
        set
        {
            currentAnim = value;
            if (currentAnim == 1)
                label.SetActive(true);
            else
                label.SetActive(false);
        }
    }
    public GameObject label;

    private void Awake()
    {
        _instance = this;
        label.SetActive(false);
    }

    public void NextAnim()
    {
        if (CurrentAnim == 2)
            return;
        else
        {
            CurrentAnim++;
            anim.SetInteger("NextAnim", CurrentAnim);
        }
    }

    public void PreviousAnim()
    {
        if (CurrentAnim == 0)
            return;
        else
        {
            CurrentAnim--;
            anim.SetInteger("NextAnim", CurrentAnim);
        }
    }
}
