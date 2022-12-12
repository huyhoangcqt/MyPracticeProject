using System;   
using System.Collections.Generic;   
using System.Text;   
using UnityEngine;
   
public class Test : MonoBehaviour {
    private void Start() {
    CorporateCustomer cc = new CorporateCustomer(Guid.NewGuid());   
    Debug.Log(cc.ID);   

    SavingsCustomer sc = new SavingsCustomer(Guid.NewGuid());   
    Debug.Log(sc.ID);   
    }

}


public abstract class Customer   

{   
    public Customer(Guid value)
    {   
        this._id = value;  
    }   

    private Guid _id;   

    public Guid ID   
    {   
        get   
        {   
        return this._id;   
        }   
    }   

}   

public class CorporateCustomer : Customer   
{   
    public CorporateCustomer(Guid value) : base (value){
    }
}   

public class SavingsCustomer : Customer   
{   
    public SavingsCustomer(Guid value) : base (value){
    }
}   
    
 