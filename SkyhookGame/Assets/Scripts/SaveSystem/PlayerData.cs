using System.Collections.Generic;

public class PlayerData
{
    //public List<DockData> docksData;
    public SettlementData settlementData;

    //public PlayerData(List<DockData> docksData, SettlementData settlementData)
    //{
    //    //this.docksData = docksData;
    //    this.settlementData = settlementData;
    //}

    public PlayerData(SettlementData settlementData)
    {
        //this.docksData = docksData;
        this.settlementData = settlementData;
    }
}
