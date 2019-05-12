namespace HuntProfit
{
    public class Hunt
    {
        public int HuntID { get; set; }
        public string Respawn { get; set; }
        public string Dia { get; set; }
        public int  Persones { get; set; }
        public float WasteEK { get; set; }
        public float WasteED { get; set; }
        public float WasteRP { get; set; }
        public float WasteMS { get; set; }
        public float TotalWaste { get; set; }
        public float Loot { get; set; }
        public float Balance { get; set; }
        public float ProfitEach { get; set; }
        public float TransferEK { get; set; }
        public float TransferED { get; set; }
        public float TransferRP { get; set; }
        public float TransferMS { get; set; }
        public string Pagat { get; set; }


        public Hunt(int huntID, string respawn, string dia, int persones, float wasteEK, float wasteED, float wasteRP, float wasteMS, float totalWaste,
                float loot, float balance, float profitEach, float transferEK, float transferED, float transferRP, float transferMS, string pagat)
        {
            HuntID = huntID;
            Respawn = respawn;
            Dia = dia;
            Persones = persones;
            WasteEK = wasteEK;
            WasteED = wasteED;
            WasteRP = wasteRP;
            WasteMS = wasteMS;
            TotalWaste = totalWaste;
            Loot = loot;
            Balance = balance;
            ProfitEach = profitEach;
            TransferEK = transferEK;
            TransferED = transferED;
            TransferRP = transferRP;
            TransferMS = transferMS;
            Pagat = pagat;
        }
    }   
}
