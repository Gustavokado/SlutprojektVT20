using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        Move(input);
        
    }
}
