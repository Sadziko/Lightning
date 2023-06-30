using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Lightning lightningSpell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var spell = Instantiate(lightningSpell, transform.position, Quaternion.identity).GetComponent<Spell>();
            spell.OnCast(transform.position);
        }
    }
}
