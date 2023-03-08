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
        CalculateGrants_OnAdd(value);
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
            CalculateGrants_OnAdd(value);
        }
    }

    public void RemoveBanknote(uint value)
    {
        if (_banknotes2.ContainsKey(value) && _banknotes2[value] != 0)
        {
            _banknotes2[value]--;
            Count--;
            _total -= value;
        }
        else if (_banknotes2.ContainsKey(value)  && _banknotes2[value] == 0)
        {
            _banknotes2.Remove(value);
        }
        CalculateGrants_OnRemove(value);
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
            }
            else if (_banknotes2.ContainsKey(value)  && _banknotes2[value] == 0)
            {
                _banknotes2.Remove(value);
            }
        }
        CalculateGrants_OnRemove(value);
    }
    
    public bool CanGrant(uint value)
    {
        if (value > _total)
        {
            return false;
        }

        return _granted[(int) value] > 0;
    }

    
    private void CalculateGrants_OnAdd(uint value)
    {
        Array.Resize(ref _granted, (int)(_total + 1));
        _granted[0] = 1;

        for (var i = _granted.Length - 1; i >= 0; i--)
        {
            
            if (_granted[i] > 0)
            {
                _granted[i + value] += _granted[i];
            }
        }
    }

    private void CalculateGrants_OnRemove(uint value)
    {
        Array.Resize(ref _granted, (int)(_total + 1));
        _granted[0] = 1;

        if (_granted.Length > 1)
        {
            for (var i = 0; i < _granted.Length; i++)
            {

                if (_granted[i] > 0 && i + value <= _granted.Length)
                {
                    _granted[i + value] -= _granted[i];
                }
            }
        }
    }
}