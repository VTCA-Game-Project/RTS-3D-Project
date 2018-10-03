using EnumCollection;
using Manager;
using UnityEngine;

public class Construct : MonoBehaviour
{

    private ConstructId[] onwed;
    private int hp;

    public ConstructId Id;
    public ConstructId[] Owned
    {
        get { return onwed; }
        protected set { onwed = value; }
    }
    public int Hp
    {
        get { return hp; }
        protected set { hp = value; }
    }
    private void Start()
    {
        Init();
        Build();
    }

    private void Init()
    {
        switch (Id)
        {
            case ConstructId.Yard:
                onwed = new ConstructId[] 
                {
                    ConstructId.Power,
                };
                break;
            case ConstructId.Power:
                onwed = new ConstructId[]
                {
                    ConstructId.Refinery,
                };
                break;
            case ConstructId.Refinery:
                onwed = new ConstructId[]
                {
                    ConstructId.War,
                    ConstructId.Barrack,
                };
                break;
            case ConstructId.War:
                onwed = new ConstructId[]
                {
                    ConstructId.Radar,
                    ConstructId.FarDefender,
                };
                break;
            case ConstructId.Barrack:
                onwed = new ConstructId[]
                {
                    ConstructId.NearDefender,
                };
                break;
            default:
                onwed = new ConstructId[0];
#if UNITY_EDITOR
                Debug.Log("Tower name not found");
#endif
                break;
        }
    }
    private void UnlockConstruct()
    {
        StoredManager.AddConstruct(this);
    }

    // public method
    public void Build()
    {
        UnlockConstruct();
    }

    public void RecieveDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0) hp = 0;
    }

    public void DestroyConstruct()
    {
        StoredManager.RemoveConstruct(this);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            DestroyConstruct();
        }
    }
}
