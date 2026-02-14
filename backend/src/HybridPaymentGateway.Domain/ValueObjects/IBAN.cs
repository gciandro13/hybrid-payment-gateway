using System.Text.RegularExpressions;

namespace HybridPaymentGateway.Domain.ValueObjects;

/// <summary>
/// Value object representing an International Bank Account Number (IBAN)
/// </summary>
public class IBAN : IEquatable<IBAN>
{
    public string Value { get; }
    public string CountryCode { get; }

    // Basic IBAN regex (simplified)
    private static readonly Regex IbanRegex = new(
        @"^[A-Z]{2}[0-9]{2}[A-Z0-9]{1,30}$",
        RegexOptions.Compiled);

    public IBAN(string iban)
    {
        if (string.IsNullOrWhiteSpace(iban))
            throw new ArgumentException("IBAN cannot be empty", nameof(iban));

        // Remove spaces and convert to uppercase
        var cleanIban = iban.Replace(" ", "").ToUpperInvariant();

        if (!IsValidIban(cleanIban))
            throw new ArgumentException("Invalid IBAN format", nameof(iban));

        Value = cleanIban;
        CountryCode = cleanIban.Substring(0, 2);
    }

    private static bool IsValidIban(string iban)
    {
        if (!IbanRegex.IsMatch(iban))
            return false;

        // Basic length validation per country (simplified)
        var countryCode = iban.Substring(0, 2);
        var expectedLength = GetExpectedLength(countryCode);
        
        if (expectedLength.HasValue && iban.Length != expectedLength.Value)
            return false;

        // TODO: Implement full MOD-97 checksum validation
        return true;
    }

    private static int? GetExpectedLength(string countryCode)
    {
        // Common IBAN lengths
        return countryCode switch
        {
            "IT" => 27,
            "DE" => 22,
            "FR" => 27,
            "ES" => 24,
            "GB" => 22,
            "NL" => 18,
            "BE" => 16,
            "CH" => 21,
            _ => null // Unknown country, skip length check
        };
    }

    public string GetFormatted()
    {
        // Format in groups of 4 characters
        return Regex.Replace(Value, ".{4}", "$0 ").TrimEnd();
    }

    public bool Equals(IBAN? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is IBAN iban && Equals(iban);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return GetFormatted();
    }

    public static bool operator ==(IBAN? left, IBAN? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(IBAN? left, IBAN? right)
    {
        return !(left == right);
    }

    public static implicit operator string(IBAN iban) => iban.Value;
}
