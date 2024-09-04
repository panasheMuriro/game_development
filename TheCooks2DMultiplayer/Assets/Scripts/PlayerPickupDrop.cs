using FishNet.Object;
using FishNet.Connection;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class PlayerPickupDrop : NetworkBehaviour
{
    [SerializeField] private Transform carryPoint;
    private bool hasObjectInHand;
    private GameObject objInHand;
    private MeatSpawner meatSpawner;
    private BunSpawner bunSpawner;
    private PlateSpawner plateSpawner;


    private readonly SyncVar<GameObject> _syncedObjInHand = new SyncVar<GameObject>(new SyncTypeSettings());





    private void Awake()
    {
        bunSpawner = FindObjectOfType<BunSpawner>();
        meatSpawner = FindObjectOfType<MeatSpawner>();
        plateSpawner = FindObjectOfType<PlateSpawner>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        // if (IsOwner)
        // {
        //     bunSpawner.BunPickedUp();
        // }
        bunSpawner = FindObjectOfType<BunSpawner>();
        meatSpawner = FindObjectOfType<MeatSpawner>();
        plateSpawner = FindObjectOfType<PlateSpawner>();

        // bunSpawner = FindObjectOfType<BunsSpawner>();
    }

    public void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                Pickup();

            if (Input.GetKey(KeyCode.Z))
                Drop();

            // cut the meat
            // HandleCutting();

            if (Input.GetKey(KeyCode.X))
            {
                RequestCuttingServerRpc();
            }

        }
    }

    private void Pickup()
    {
        if (hasObjectInHand)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        GameObject cuttingBoard = GetCuttingBoard(colliders);
        GameObject ms = GetMeatSpawner(colliders);
        GameObject bt = GetBunTray(colliders);


        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Plate") || collider.CompareTag("Bun") || collider.CompareTag("Meat"))
            {

                if (collider.CompareTag("Bun"))
                {
                    if (bt != null)
                    {
                        // bunSpawner.BunPickedUp();
                        bunSpawner.BunPickedUp();
                    }

                }

                if (collider.CompareTag("Meat"))
                {

                    if (ms != null)
                    {
                        meatSpawner.MeatPickedUp();
                    }

                    if (cuttingBoard != null)
                    {
                        CuttingBoard cbScript = cuttingBoard.GetComponent<CuttingBoard>();
                        cbScript.SetIsOccupied(false);
                    }

                }


                // Spawn meat
                SetObjectInHandServer(collider.gameObject, carryPoint.position, gameObject);
                objInHand = collider.gameObject;
                hasObjectInHand = true;
                break;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SetObjectInHandServer(GameObject obj, Vector3 position, GameObject player)
    {
        _syncedObjInHand.Value = obj;
        SetObjectInHandObserver(obj, position, player);
    }

    [ObserversRpc]
    void SetObjectInHandObserver(GameObject obj, Vector3 position, GameObject player)
    {
        if (obj.GetComponent<Collider2D>() != null)
            obj.GetComponent<Collider2D>().enabled = false;

        obj.transform.position = position;
        obj.transform.parent = player.transform;

        if (obj.GetComponent<Rigidbody2D>() != null)
        {
            var rb = obj.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }


    // ----- DROP ------

    private void Drop()
    {
        if (!hasObjectInHand)
            return;



        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        GameObject plate = GetPlate(colliders);
        GameObject platform = GetPlatform(colliders);
        GameObject cuttingBoard = GetCuttingBoard(colliders);
        GameObject pan = GetPan(colliders);
        GameObject deliveryZone = GetDeliveryZone(colliders);
        GameObject meat = GetMeat(colliders);
        GameObject bun = GetBun(colliders);

        // Can't drop anything onto the spawners
        if (objInHand.CompareTag("Bun"))
        {
            if (plate != null)
            {
                Plate plateScript = plate.GetComponent<Plate>();
                if (!plateScript.hasBun.Value)
                {
                    Transform bunCenterPoint = plate.transform.Find("BunCenterPoint");
                    if (bunCenterPoint != null)
                    {
                        DropObjectOntoAnotherServer(objInHand, bunCenterPoint.position, plate);
                        // plate.GetComponent<Plate>().hasBun.Value = true;
                        plateScript.SetHasBunServerRpc();
                        hasObjectInHand = false;
                        objInHand.tag = "Untagged";
                        objInHand = null;
                        return;
                    }
                }
            }

            else if (platform != null)
            {
                // TODO: position properly
                DropObjectServer(objInHand);
                hasObjectInHand = false;
                objInHand = null;
            }

        }

        // TODO: Other ingredients

        else if (objInHand.CompareTag("Meat"))
        {
            // only uncooked meat goes into the cutting board
            Meat meatScript = objInHand.GetComponent<Meat>();
            if (!meatScript.isCut.Value)
            {
                if (cuttingBoard != null)
                {
                    CuttingBoard cbScript = cuttingBoard.GetComponent<CuttingBoard>();
                    if (!cbScript.isOccupied.Value)
                    {
                        Transform cuttingBoardCenterPoint = cuttingBoard.transform.Find("CuttingBoardCenterPoint");
                        DropObjectOntoAnotherServer(objInHand, cuttingBoardCenterPoint.position, cuttingBoard);
                        cbScript.SetIsOccupied(true);
                        hasObjectInHand = false;
                        objInHand = null;
                        return;
                    }
                }

                if (plate != null)
                {
                    return;
                }
                else if (pan == null && platform != null)
                {
                    // TODO: position properly
                    DropObjectServer(objInHand);
                    hasObjectInHand = false;
                    objInHand = null;
                }



            }
            else if (meatScript.isCut.Value && !meatScript.isCooked.Value)
            {
                //  can place onto a pan or platform

                if (pan != null)
                {
                    Pan panScript = pan.GetComponent<Pan>();
                    if (!panScript.isCooking.Value)
                    {
                        Debug.Log("We will be cooking");
                        Transform panCenterPoint = pan.transform.Find("PanCenterPoint");
                        if (panCenterPoint != null)
                        {
                            DropObjectOntoAnotherServer(objInHand, panCenterPoint.position, pan);
                            pan.GetComponent<Pan>().StartCooking(meatScript);
                            hasObjectInHand = false;
                            objInHand = null;
                            return;
                        }
                    }
                }
                // else if (platform != null)
                // {
                //     // TODO: position properly
                //     DropObjectServer(objInHand);
                //     hasObjectInHand = false;
                //     objInHand = null;
                // }
            }
            else if (meatScript.isCooked.Value && plate != null)
            {

                Plate plateScript = plate.GetComponent<Plate>();
                if (!plateScript.hasMeat.Value)
                {
                    Transform meatCenterPoint = plate.transform.Find("MeatCenterPoint");
                    if (meatCenterPoint != null)
                    {
                        DropObjectOntoAnotherServer(objInHand, meatCenterPoint.position, plate);
                        plateScript.SetHasMeatServerRpc();
                        hasObjectInHand = false;
                        objInHand.tag = "Untagged";
                        objInHand = null;
                        return;
                    }
                }
                // Otherwise do nothing
            }

            else if (platform != null)
            {
                // TODO: position properly
                DropObjectServer(objInHand);
                hasObjectInHand = false;
                objInHand = null;
            }
        }


        else if (objInHand.CompareTag("Plate"))
        {

            Plate plateScript = objInHand.GetComponent<Plate>();

            // standing infront of the delivery zone with a done plate

            if (deliveryZone != null)
            {
                Debug.Log("Player is here");
                if (plateScript.hasBun.Value && plateScript.hasMeat.Value)
                {
                    objInHand.transform.SetParent(null);
                    ServeOrderServer(objInHand);
                    ScoreManager.instance.IncreaseScore(10);
                    //  Spawn Plate
                    plateSpawner.SpawnPlate();
                    hasObjectInHand = false;
                }
                return;
            }

            if (cuttingBoard || bun || meat || pan)
            {
                return;
            }
            // only drop onto a platform
            else if (platform != null)
            {
                // TODO: position properly
                DropObjectServer(objInHand);
                hasObjectInHand = false;
                objInHand = null;
            }
        }


    }



    [ServerRpc(RequireOwnership = false)]
    void DropObjectOntoAnotherServer(GameObject obj, Vector3 position, GameObject hostObj)
    {
        DropObjectOntoAnotherObserver(obj, position, hostObj);
    }


    [ObserversRpc]
    void DropObjectOntoAnotherObserver(GameObject obj, Vector3 position, GameObject hostObj)
    {
        obj.transform.position = position;
        obj.transform.parent = hostObj.transform;
        if (!hostObj.CompareTag("Plate"))
        {
            if (obj.GetComponent<Collider2D>() != null)
            {
                obj.GetComponent<Collider2D>().enabled = true;
            }
        }
    }


    //  TODO: Get right of this
    [ServerRpc(RequireOwnership = false)]
    void DropObjectServer(GameObject obj)
    {
        DropObjectObserver(obj);
    }

    [ObserversRpc]
    void DropObjectObserver(GameObject obj)
    {
        if (obj.GetComponent<Collider2D>() != null)
            obj.GetComponent<Collider2D>().enabled = true;
        obj.transform.parent = null;
        if (obj.GetComponent<Rigidbody2D>() != null)
        {
            var rb = obj.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void ServeOrderServer(GameObject obj)
    {
        ServeOrderObserver(obj);
    }

    [ObserversRpc]
    void ServeOrderObserver(GameObject obj)
    {
        obj.transform.parent = null;
        Destroy(obj);
    }



    // If there is a plate, and it has a bun already, don't drop
    // else if there is a platform, drop there
    private GameObject GetPlate(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Plate"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private GameObject GetPlatform(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Platform"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }
    private GameObject GetCuttingBoard(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("CuttingBoard"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private GameObject GetMeatSpawner(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("MeatSpawner"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private GameObject GetBunTray(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("BunTray"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private GameObject GetPan(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Pan"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private GameObject GetDeliveryZone(Collider2D[] colliders)
    {

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("DeliveryZone"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private GameObject GetMeat(Collider2D[] colliders)
    {

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Meat"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private GameObject GetBun(Collider2D[] colliders)
    {

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bun"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }



    // TODO: move this into the cutting board script

    // [ServerRpc(RequireOwnership = false)]
    // void HandleCutting()
    // {

    //     Debug.Log("Will cut the meat");
    //     if (Input.GetKey(KeyCode.X))
    //     {
    //         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
    //         GameObject cb = GetCuttingBoard(colliders);
    //         if (cb != null)
    //         {
    //             CuttingBoard cbScript = cb.GetComponent<CuttingBoard>();
    //             if (cbScript.isOccupied.Value)
    //             {
    //                 foreach (Transform child in cb.transform)
    //                 {
    //                     if (child.CompareTag("Meat") && !child.GetComponent<Meat>().isCut.Value)
    //                     {
    //                         // Cut the meat

    //                         Debug.Log("Cut the meat");

    //                         child.GetComponent<Meat>().Cut();

    //                         break;
    //                     }
    //                 }
    //             }
    //         }
    //     }
    // }


    [ServerRpc(RequireOwnership = false)]
    private void RequestCuttingServerRpc()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        GameObject cb = GetCuttingBoard(colliders);
        if (cb != null)
        {
            CuttingBoard cbScript = cb.GetComponent<CuttingBoard>();
            if (cbScript.isOccupied.Value)
            {
                foreach (Transform child in cb.transform)
                {
                    if (child.CompareTag("Meat") && !child.GetComponent<Meat>().isCut.Value)
                    {
                        // Cut the meat
                        child.GetComponent<Meat>().Cut();
                        NotifyCuttingObserversRpc(child.gameObject);
                        break;
                    }
                }
            }
        }
    }

    [ObserversRpc]
    private void NotifyCuttingObserversRpc(GameObject meatObj)
    {
        if (meatObj != null)
        {
            meatObj.GetComponent<Meat>().Cut();
        }
    }
}
