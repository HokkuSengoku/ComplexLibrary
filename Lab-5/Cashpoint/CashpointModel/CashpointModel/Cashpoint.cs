namespace CashpointModel;

using System.Collections.Generic;

public sealed class Cashpoint
{
    private Dictionary<uint, byte> _banknotes2 = new Dictionary<uint, byte>();
    private uint[] _granted = { 1 };

    private uint _total;

    public uint Count { get; set; }
    public uint Total
    {
        get
        {
            return _total;
        }
    }

    public void AddBanknote(uint value)
    {
        if (!_banknotes2.ContainsKey(value))
        {
            _banknotes2[value] = 1;
        }
        else
        {
            _banknotes2[value]++;
        }
        Count++;
        _total += value;
        CalculateGrants();
    }

    public void AddBanknote(uint value, int count)
    {
        for (var i = 0; i < count; i++)
        {
            if (!_banknotes2.ContainsKey(value))
            {
                _banknotes2[value] = 1;
            }
            else
            {
                _banknotes2[value]++;
            }
            Count++;
            _total += value;
            CalculateGrants();
        }
    }

    public void RemoveBanknote(uint value)
    {
        if (_banknotes2.ContainsKey(value) && _banknotes2[value] != 0)
        {
            _banknotes2[value]--;
            Count--;
            _total -= value;
            CalculateGrants();
        }
        else if (_banknotes2.ContainsKey(value)  && _banknotes2[value] == 0)
        {
            _banknotes2.Remove(value);
        }
    }

    public void RemoveBanknote(uint value, int count)
    {
        for (var i = 0; i < count; i++)
        {
            if (_banknotes2.ContainsKey(value) && _banknotes2[value] != 0)
            {
                _banknotes2[value]--;
                Count--;
                _total -= value;
                CalculateGrants();
            }
            else if (_banknotes2.ContainsKey(value)  && _banknotes2[value] == 0)
            {
                _banknotes2.Remove(value);
            }
        }
    }
    
    public bool CanGrant(uint value)
    {
        if (value > _total)
        {
            return false;
        }

        return _granted[(int) value] > 0;
    }

    
    private void CalculateGrants()
    {
        _granted = new uint[_total + 1];
        _granted[0] = 1;

        foreach (var b in _banknotes2)
        {
            for (var i = (int)_total; i >= 0; i--)
            {
                if (_granted[i] == 1 && _total != 0 && i + b.Key <= _total)
                {
                    _granted[i + b.Key] = 1;
                }
            }
        }
    }
}