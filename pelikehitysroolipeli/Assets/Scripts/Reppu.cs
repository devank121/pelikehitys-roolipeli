using System.Collections.Generic;

public class Reppu
{
    private int maksimiMaara;
    private float maksimiPaino;
    private float maksimiTilavuus;

    private List<Tavara> tavarat = new List<Tavara>();

    public Reppu(int maksimiMaara, float maksimiPaino, float maksimiTilavuus)
    {
        this.maksimiMaara = maksimiMaara;
        this.maksimiPaino = maksimiPaino;
        this.maksimiTilavuus = maksimiTilavuus;
    }

    public int TavaroidenMaara
    {
        get { return tavarat.Count; }
    }

    public float YhteenlasPaino
    {
        get
        {
            float yhteensa = 0f;
            foreach (Tavara t in tavarat)
            {
                yhteensa += t.Paino;
            }
            return yhteensa;
        }
    }

    public float YhteenlaskettuTilavuus
    {
        get
        {
            float yhteensa = 0f;
            foreach (Tavara t in tavarat)
            {
                yhteensa += t.Tilavuus;
            }
            return yhteensa;
        }
    }

    public bool Lisää(Tavara tavara)
    {
        if (TavaroidenMaara + 1 > maksimiMaara)
        {
            return false;
        }
        if (YhteenlasPaino + tavara.Paino > maksimiPaino)
        {
            return false;
        }
        if (YhteenlaskettuTilavuus + tavara.Tilavuus > maksimiTilavuus)
        {
            return false;
        }

        tavarat.Add(tavara);
        return true;
    }

    public Tavara HaeTavara(int index)
    {
        if (index >= 0 && index < tavarat.Count)
        {
            return tavarat[index];
        }
        return null;
    }

    public override string ToString()
    {
        if (tavarat.Count == 0)
        {
            return "Reppu on tyhjä.";
        }

        string sisalto = "Reppussa on seuraavat tavarat: ";

        foreach (Tavara t in tavarat)
        {
            sisalto += t.ToString() + ", ";
        }

        sisalto = sisalto.TrimEnd(',', ' ');

        return sisalto;
    }
}