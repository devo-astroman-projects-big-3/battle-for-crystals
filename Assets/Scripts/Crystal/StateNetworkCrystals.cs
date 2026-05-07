using Fusion;
using UnityEngine;
using System;

public class StateNetworkCrystal : NetworkBehaviour
{

    private int[] _allCrystalsData = new int[]{0,0};

    [Networked, OnChangedRender(nameof(OnNCrystalTakenStateChanged))]
    private int NCrystalTaken { get; set; }
    public event Action CrystalsChangedAction;

    public override void Spawned()
    {
        NCrystalTaken = 0;
    }

    public int GetNCrystalsTaken()
    {
        return NCrystalTaken;
    }

    public int[] GetAllCrystalsData()
    {
        return _allCrystalsData;
    }

    public void RequestCrystalTaken(int crystalId)
    {
        if (Object == null)
        {
            Debug.LogWarning("Crystal is not spawned yet.");
            return;
        }

        RPC_CrystalTaken(crystalId);
    }
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_CrystalTaken(int crystalId)
    {
        _allCrystalsData[crystalId] = 1;
        NCrystalTaken++;
    }
    
    private void OnNCrystalTakenStateChanged()
    {
        CrystalsChangedAction?.Invoke();
    }

    
}