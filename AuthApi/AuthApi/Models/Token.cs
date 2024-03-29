﻿namespace AuthApi.Models;

public class Token
{
    public int Id { get; set; }
    public string email { get; set; }
    public string myToken { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}