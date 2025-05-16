namespace ExpenseManager.Models.Main;

public enum AccountColor
{
    Blue, Green, Red, Yellow, Orange, Purple, Pink
}

public static class AccountColorExtensions
{
    public static string GetHex(this AccountColor color)
    {
        return color switch
        {
            AccountColor.Blue => "#0000FF",
            AccountColor.Green => "#008000",
            AccountColor.Red => "#FF0000",
            AccountColor.Yellow => "#FFFF00",
            AccountColor.Orange => "#FFA500",
            AccountColor.Purple => "#800080",
            AccountColor.Pink => "#FFC0CB",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}
