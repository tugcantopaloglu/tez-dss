using System;

namespace DonerSermaye.Models.Helper { 
    public static class LayoutHelper
{
    public static string GetPath(string yetki){ 
    switch(yetki)
        {
            case "1": return "~/Views/Shared/bolumbaskan.cshtml";
            case "2": return "~/Views/Shared/Index1.cshtml";
            case "3": return "~/Views/Shared/doner.cshtml";
            case "4": return "~/Views/Shared/Ekmek.cshtml";
            case "5": return "~/Views/Shared/Dekan.cshtml";
            case "6": return "~/Views/Shared/Sekreter.cshtml";
            default:return null;
        }
    }
}
}