namespace CashpointModel;

using System.Collections.Generic;

public sealed class Cashpoint
{
    private readonly List<uint> _banknotes = new List<uint>();
    private bool[] _granted = { true };

    private uint _total;

    public uint Total
    {
        get
        {
            return _total;
        }
    }

    public void AddBanknote(uint value)
    {
        _banknotes.Add(value);
        _total += value;
    }

    public void RemoveBanknote(uint value)
    {
        if (_banknotes.Remove(value))
        {
            _total -= value;
        }
    }
    
    public bool CanGrant(uint value)
    {
        CalculateGrants();

        if (value > _total)
        {
            return false;
        }

        return _granted[(int)value];
    }

    private void CalculateGrants()
    {
        _granted = new bool[_total + 1];
        _granted[0] = true;

        foreach (var b in _banknotes)
        {
            for (var i = (int)_total; i >= 0; i--)
            {
                if (_granted[i])
                {
                    _granted[i + b] = true;
                }
            }
        }
    }
}