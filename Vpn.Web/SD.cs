﻿namespace Vpn.Web;

public static class SD
{
    public static string ProductAPIBase { get; set; }
    public static string ShopingCartAPIBase { get; set; }

    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}