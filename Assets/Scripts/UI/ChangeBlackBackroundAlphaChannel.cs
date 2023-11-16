using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBlackBackroundAlphaChannel : MonoBehaviour
{
   [SerializeField] private SpriteRenderer blackBackroundForBeamer;
   [SerializeField] private GameObject mainMenu;

   private void Update()
   {
      if (mainMenu.activeSelf)
      {
         blackBackroundForBeamer.color = new Color(blackBackroundForBeamer.color.r, blackBackroundForBeamer.color.g,
            blackBackroundForBeamer.color.b, 0);
      }
   }
}
