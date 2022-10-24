namespace Casting;

[Flags]
public enum BookAttributes : short
{
    IsNothing = 0x0,
    IsEducational = 0x1,
    IsDetective = 0x2,
    IsHumoros = 0x4,
    IsMedical = 0x8,
    IsPolitical = 0x10,
    IsEconomical = 0x20
}