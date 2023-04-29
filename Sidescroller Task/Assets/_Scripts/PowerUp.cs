public abstract class PowerUp
{
    private int _charges;

    protected PowerUp()
    {
        _charges = 1;
    }
    
    /// <summary>
    /// Invoke this before power up logic
    /// </summary>
    public virtual bool OnUse()
    {
        if (_charges <= 0) return false;

        _charges--;

        return true;
    }

    public void GainCharge()
    {
        _charges++;
    }

    public int GetCharges()
    {
        return _charges;
    }
}