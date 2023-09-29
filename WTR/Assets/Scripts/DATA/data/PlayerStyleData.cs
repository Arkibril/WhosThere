[System.Serializable]
public class PlayerStyleData
{
    public int headIndex, bodyIndex, bodyColorIndex, hairIndex, hairColorIndex, eyesColorIndex, topIndex, bottomIndex, shoesIndex;
    public bool easterEggs;

    public PlayerStyleData(){
        this.headIndex = 0;
        this.bodyColorIndex = 0;
        this.bodyIndex = 0;
        this.hairIndex = 0;
        this.hairColorIndex = 0;
        this.eyesColorIndex = 0;
        this.topIndex = 0;
        this.bottomIndex = 0;
        this.shoesIndex = 0;
        this.easterEggs = false;
    }
}
