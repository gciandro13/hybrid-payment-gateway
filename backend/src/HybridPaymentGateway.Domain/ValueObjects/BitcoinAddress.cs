using System.Text.RegularExpressions;

namespace HybridPaymentGateway.Domain.ValueObjects;

/// <summary>
/// Value object representing a Bitcoin address
/// </summary>
public class BitcoinAddress : IEquatable<BitcoinAddress>
{
    public string Value { get; }

    // Simplified regex for Bitcoin addresses (Legacy, SegWit, Bech32)
    private static readonly Regex BitcoinAddressRegex = new(
        @"^(bc1|[13])[a-zA-HJ-NP-Z0-9]{25,62}$",
        RegexOptions.Compiled);

    public BitcoinAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Bitcoin address cannot be empty", nameof(address));

        if (!IsValidBitcoinAddress(address))
            throw new ArgumentException("Invalid Bitcoin address format", nameof(address));

        Value = address;
    }

    private static bool IsValidBitcoinAddress(string address)
    {
        // Basic validation - in production, use a proper Bitcoin library
        return BitcoinAddressRegex.IsMatch(address);
    }

    public bool IsLegacyAddress() => Value.StartsWith("1") || Value.StartsWith("3");
    public bool IsSegWitAddress() => Value.StartsWith("bc1");

    public bool Equals(BitcoinAddress? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is BitcoinAddress address && Equals(address);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(BitcoinAddress? left, BitcoinAddress? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(BitcoinAddress? left, BitcoinAddress? right)
    {
        return !(left == right);
    }

    public static implicit operator string(BitcoinAddress address) => address.Value;
}
