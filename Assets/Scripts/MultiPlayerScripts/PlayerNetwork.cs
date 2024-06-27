using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData {
            x = 5
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyUp(KeyCode.T))
        {
            TestServerRpc();
            //randomNumber.Value = new MyCustomData
            //{
            //    x = Random.Range(0, 100), 
            //    message = "A"
            //};
        }
        Vector3 moveDir = new Vector3(0,0,0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        randomNumber.OnValueChanged += (MyCustomData prev, MyCustomData changed) => { 
            Debug.Log(OwnerClientId +"; "+ prev.x + " ; "+changed.x + " ; " + prev.message+" ; " + changed.message);
            Debug.Log(OwnerClientId +"; "+randomNumber.Value.x);

        };
    }

    [ServerRpc]
    public void TestServerRpc()
    {
        Debug.Log("Test ServerRPC "+OwnerClientId);
    }

}
    public struct MyCustomData : INetworkSerializable
    {
       public int x;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref x);
            serializer.SerializeValue(ref message);
        }
    }
