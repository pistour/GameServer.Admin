using System;

namespace GameServer.Admin.Models;

public class AdminLog
{
    public int Id { get; set; } // Unikátní ID záznamu
    public string ActionPath { get; set; } = string.Empty; // Jakou stránku admin navštívil
    public DateTime VisitedAt { get; set; } = DateTime.Now; // Přesný čas návštěvy
}